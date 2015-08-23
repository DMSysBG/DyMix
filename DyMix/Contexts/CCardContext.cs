using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DyMix.Models;
using System.Data;
using DMSys.Systems;

namespace DyMix.Contexts
{
    public class CCardContext: DMSys.Data.MSSQLUtility
    {
        public CCardContext()
            : base(xConfig.ConnectionString)
        { }

        public List<CCardModel> GetCards()
        {
            List<CCardModel> model = new List<CCardModel>();
            string commandText =
@"SELECT cc.ID
       , cc.C_NUMBER
       , cc.VALID_FROM_DATE
       , cc.VALID_TO_DATE
       , cc.IS_BLOCKED
       , cc.CONTRACTOR_ID
 FROM C_CARD cc
 ORDER BY cc.C_NUMBER ";

            using (DataTable dtCCards = FillDataTable(commandText))
            {
                foreach (DataRow drCCard in dtCCards.Rows)
                {
                    model.Add(new CCardModel()
                    {
                        CardId = TryParse.ToInt32(drCCard["ID"]),
                        CNumber = TryParse.ToString(drCCard["C_NUMBER"]),
                        ValidFrom = TryParse.ToDateTime(drCCard["VALID_FROM_DATE"]),
                        ValidTo = TryParse.ToDateTime(drCCard["VALID_TO_DATE"]),
                        IsBlocked = (TryParse.ToInt32(drCCard["IS_BLOCKED"]) == 1),
                        ContractorId = TryParse.ToInt32(drCCard["CONTRACTOR_ID"])
                    });
                }
            }
            return model;
        }

        public CCardModel GetCard(int id)
        {
            CCardModel model = null;
            string commandText =
@"SELECT cc.ID
       , cc.C_NUMBER
       , cc.VALID_FROM_DATE
       , cc.VALID_TO_DATE
       , cc.IS_BLOCKED
       , c.ID AS CONTRACTOR_ID
       , c.C_NAME AS CONTRACTOR_NAME
       , cc.DISCOUNT_GROUP_ID
       , cc.OWNER_NAME
       , cc.CAR_NUMBER
       , cc.IS_INVOICE
       , cc.IS_MANUAL_INPUT
       , cc.C_COMMENT
       , cc.S_PAYMENT_TYPE_ID
       , cc.C_PASSWORD
 FROM C_CARD cc
 LEFT JOIN CONTRACTOR c ON c.ID = cc.CONTRACTOR_ID
 WHERE cc.ID = " + SQLInt(id);

            using (DataTable dtCCards = FillDataTable(commandText))
            {
                foreach (DataRow drCCard in dtCCards.Rows)
                {
                    model = new CCardModel()
                    {
                        CardId = TryParse.ToInt32(drCCard["ID"]),
                        CNumber = TryParse.ToString(drCCard["C_NUMBER"]),
                        ValidFrom = TryParse.ToDateTime(drCCard["VALID_FROM_DATE"]),
                        ValidTo = TryParse.ToDateTime(drCCard["VALID_TO_DATE"]),
                        IsBlocked = (TryParse.ToInt32(drCCard["IS_BLOCKED"]) == 1),
                        ContractorId = TryParse.ToInt32(drCCard["CONTRACTOR_ID"]),
                        ContractorName = TryParse.ToString(drCCard["CONTRACTOR_NAME"]),
                        DiscountGroupId = TryParse.ToInt32(drCCard["DISCOUNT_GROUP_ID"], -1),
                        OwnerName = TryParse.ToString(drCCard["OWNER_NAME"]),
                        CarNumber = TryParse.ToString(drCCard["CAR_NUMBER"]),
                        IsInvoice = (TryParse.ToInt32(drCCard["IS_INVOICE"]) == 1),
                        IsManualInput = (TryParse.ToInt32(drCCard["IS_MANUAL_INPUT"]) == 1),
                        Password = TryParse.ToString(drCCard["C_PASSWORD"]),
                        Comment = TryParse.ToString(drCCard["C_COMMENT"]),
                        PaymentTypeId = TryParse.ToInt32(drCCard["S_PAYMENT_TYPE_ID"])
                    };
                }
            }
            return model;
        }
        
        /// <summary>
        /// Добавя карта
        /// </summary>
        public void Add(CCardModel model)
        {
            string commandText =
@"INSERT INTO C_CARD
  ( C_NUMBER, VALID_FROM_DATE, VALID_TO_DATE, IS_BLOCKED, CONTRACTOR_ID, DISCOUNT_GROUP_ID, OWNER_NAME
  , CAR_NUMBER, IS_INVOICE, IS_MANUAL_INPUT, C_PASSWORD, C_COMMENT, S_PAYMENT_TYPE_ID )
 VALUES (" + SQLString(model.CNumber) +
 ", " + SQLDateTime(model.ValidFrom) +
 ", " + SQLDateTime(model.ValidTo) +
 ", " + SQLInt(model.IsBlocked) +
 ", " + SQLInt(model.ContractorId) +
 ", " + SQLInt(model.DiscountGroupId) +
 ", " + SQLString(model.OwnerName) +
 ", " + SQLString(model.CarNumber) +
 ", " + SQLInt(model.IsInvoice) +
 ", " + SQLInt(model.IsManualInput) +
 ", " + SQLString(model.Password) +
 ", " + SQLString(model.Comment) +
 ", " + SQLInt(model.PaymentTypeId) +
@")
 
SELECT @@IDENTITY";

            model.CardId = TryParse.ToInt32(ExecuteScalar(commandText));
        }

        /// <summary>
        /// Редактира карта
        /// </summary>
        public void Edit(CCardModel model)
        {
            string commandText =
@"UPDATE C_CARD
     SET C_NUMBER = " + SQLString(model.CNumber) + @"
       , VALID_FROM_DATE = " + SQLDateTime(model.ValidFrom) + @"
       , VALID_TO_DATE = " + SQLDateTime(model.ValidTo) + @"
       , IS_BLOCKED = " + SQLInt(model.IsBlocked) + @"
       , CONTRACTOR_ID = " + SQLInt(model.ContractorId) + @"
       , DISCOUNT_GROUP_ID = " + SQLInt(model.DiscountGroupId) + @"
       , OWNER_NAME = " + SQLString(model.OwnerName) + @"
       , CAR_NUMBER = " + SQLString(model.CarNumber) + @"
       , IS_INVOICE = " + SQLInt(model.IsInvoice) + @"
       , IS_MANUAL_INPUT = " + SQLInt(model.IsManualInput) + @"
       , C_PASSWORD = " + SQLString(model.Password) + @"
       , C_COMMENT = " + SQLString(model.Comment) + @"
       , S_PAYMENT_TYPE_ID = " + SQLInt(model.PaymentTypeId) + @"
 WHERE ID = " + SQLInt(model.CardId);

            ExecuteNonQuery(commandText);
        }
    }
}