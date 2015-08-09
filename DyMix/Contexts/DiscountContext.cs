using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DyMix.Models;
using DMSys.Systems;

namespace DyMix.Contexts
{
    public class DiscountContext : DMSys.Data.MSSQLUtility
    {
        public DiscountContext()
            : base(xConfig.ConnectionString)
        { }

        public List<DiscountModel> GetDiscounts()
        {
            List<DiscountModel> model = new List<DiscountModel>();
            string commandText =
@"SELECT d.ID AS DISCOUNT_ID
       , d.D_NAME
       , d.VALID_FROM_DATE
       , d.VALID_TO_DATE
       , d.DISCOUNT_KIND_ID
       , dk.DK_NAME
       , d.D_VALUE
 FROM DISCOUNT d
 LEFT JOIN DISCOUNT_KIND dk ON dk.ID = d.DISCOUNT_KIND_ID
 ORDER BY d.D_NAME ";

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new DiscountModel()
                    {
                        DiscountId = TryParse.ToInt32(drDiscount["DISCOUNT_ID"]),
                        Name = TryParse.ToString(drDiscount["D_NAME"]),
                        DiscountKindId = TryParse.ToInt32(drDiscount["DISCOUNT_KIND_ID"]),
                        DiscountKindName = TryParse.ToString(drDiscount["DK_NAME"]),
                        ValidFrom = TryParse.ToDateTime(drDiscount["VALID_FROM_DATE"]),
                        ValidTo = TryParse.ToDateTime(drDiscount["VALID_TO_DATE"]),
                        Value = TryParse.ToDecimal(drDiscount["D_VALUE"]),
                    });
                }
            }
            return model;
        }

        public List<DiscountGroupModel> GetDiscountGroups()
        {
            List<DiscountGroupModel> model = new List<DiscountGroupModel>();
            string commandText =
@"SELECT dg.ID AS DISCOUNT_GROUP_ID
       , dg.DG_NAME
 FROM DISCOUNT_GROUP dg
 ORDER BY dg.DG_NAME ";

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new DiscountGroupModel()
                    {
                        DiscountGroupId = TryParse.ToInt32(drDiscount["DISCOUNT_GROUP_ID"]),
                        Name = TryParse.ToString(drDiscount["DG_NAME"]),
                    });
                }
            }
            return model;
        }

        /// <summary>
        /// Филтриран списък отстъки
        /// </summary>
        public List<ListItemModel> GetFilterList(string term)
        {
            List<ListItemModel> model = new List<ListItemModel>();
            string filter = ((term == "*") ? "'%'" : String.Format("'%{0}%'", term));
            string commandText =
@"SELECT TOP 20 d.ID
       , d.D_NAME
 FROM DISCOUNT d
 WHERE d.D_NAME LIKE " + filter + @"
 ORDER BY d.D_NAME ";

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new ListItemModel()
                    {
                        id = TryParse.ToString(drDiscount["ID"]),
                        label = TryParse.ToString(drDiscount["D_NAME"]),
                        abbrev = ""
                    });
                }
            }
            return model;
        }

        /// <summary>
        /// Списък отстъки
        /// </summary>
        public List<ListItemModel> GetList(string discountList)
        {
            List<ListItemModel> model = new List<ListItemModel>();
            string sqlDiscountList = discountList.Trim(',');
            if (String.IsNullOrWhiteSpace(sqlDiscountList))
            { return model; }

            string commandText =
@"SELECT d.ID, d.D_NAME
 FROM DISCOUNT d
 WHERE d.ID IN (" + sqlDiscountList + @")
 ORDER BY d.D_NAME ";

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new ListItemModel()
                    {
                        id = TryParse.ToString(drDiscount["ID"]),
                        label = TryParse.ToString(drDiscount["D_NAME"]),
                        abbrev = ""
                    });
                }
            }
            return model;
        }

        public DiscountModel GetDiscount(int id)
        {
            DiscountModel model = null;
            string commandText =
@"SELECT d.ID AS DISCOUNT_ID
       , d.D_NAME
       , d.VALID_FROM_DATE
       , d.VALID_TO_DATE
       , d.DISCOUNT_KIND_ID
       , dk.DK_NAME
       , d.D_VALUE
 FROM DISCOUNT d
 LEFT JOIN DISCOUNT_KIND dk ON dk.ID = d.DISCOUNT_KIND_ID
 WHERE d.ID = " + SQLInt(id);

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                if( dtDiscounts.Rows.Count > 0)
                {
                    DataRow drDiscount = dtDiscounts.Rows[0];
                    model = new DiscountModel()
                    {
                        DiscountId = TryParse.ToInt32(drDiscount["DISCOUNT_ID"]),
                        Name = TryParse.ToString(drDiscount["D_NAME"]),
                        DiscountKindId = TryParse.ToInt32(drDiscount["DISCOUNT_KIND_ID"]),
                        DiscountKindName = TryParse.ToString(drDiscount["DK_NAME"]),
                        ValidFrom = TryParse.ToDateTime(drDiscount["VALID_FROM_DATE"]),
                        ValidTo = TryParse.ToDateTime(drDiscount["VALID_TO_DATE"]),
                        Value = TryParse.ToDecimal(drDiscount["D_VALUE"]),
                    };
                }
            }
            return model;
        }

        public DiscountGroupModel GetDiscountGroup(int id)
        {
            DiscountGroupModel model = null;
            string commandText =
@"SELECT dg.ID AS DISCOUNT_GROUP_ID
       , dg.DG_NAME
       , (SELECT ',' + cast(sgi.DISCOUNT_ID AS nvarchar(MAX)) AS [text()]
          FROM DISCOUNT_GROUP_ITEM sgi
          WHERE sgi.DISCOUNT_GROUP_ID = dg.ID
          For XML PATH ('')
         ) + ',' AS DISCOUNT_LIST
 FROM DISCOUNT_GROUP dg
 WHERE dg.ID = " + SQLInt(id);

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                if( dtDiscounts.Rows.Count > 0)
                {
                    DataRow drDiscount = dtDiscounts.Rows[0];
                    model = new DiscountGroupModel()
                    {
                        DiscountGroupId = TryParse.ToInt32(drDiscount["DISCOUNT_GROUP_ID"]),
                        Name = TryParse.ToString(drDiscount["DG_NAME"]),
                        DiscountList = TryParse.ToString(drDiscount["DISCOUNT_LIST"])
                    };
                }
            }
            return model;
        }

        public List<DiscountKindModel> GetDiscountKinds()
        {
            List<DiscountKindModel> model = new List<DiscountKindModel>();
            string commandText =
@"SELECT dk.ID
       , dk.DK_NAME
 FROM DISCOUNT_KIND dk
 ORDER BY dk.DK_NAME ";

            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new DiscountKindModel()
                    {
                        DiscountKindId = TryParse.ToInt32(drDiscount["ID"]),
                        Name = TryParse.ToString(drDiscount["DK_NAME"])
                    });
                }
            }
            return model;
        }

        public void Add(DiscountModel model)
        {
            string commandText =
@"INSERT INTO DISCOUNT
  ( D_NAME, VALID_FROM_DATE, VALID_TO_DATE, DISCOUNT_KIND_ID, D_VALUE )
 VALUES (" + SQLString(model.Name) + 
 ", " + SQLDateTime(model.ValidFrom) + 
 ", " + SQLDateTime(model.ValidTo) + 
 ", " + SQLInt(model.DiscountKindId) +
 ", " + SQLDecimal(model.Value) + ")";

            ExecuteNonQuery(commandText);
        }

        public int Add(DiscountGroupModel model)
        {
            string commandText =
@"INSERT INTO DISCOUNT_GROUP
  ( DG_NAME )
 VALUES (" + SQLString(model.Name) + @")
           
SELECT @@IDENTITY";

            model.DiscountGroupId = TryParse.ToInt32(ExecuteScalar(commandText));
            return model.DiscountGroupId;
        }

        public void Edit(DiscountModel model)
        { 
             string commandText =
@"UPDATE DISCOUNT
     SET D_NAME = " + SQLString(model.Name) + @"
       , VALID_FROM_DATE = " + SQLDateTime(model.ValidFrom) + @"
       , VALID_TO_DATE = " + SQLDateTime(model.ValidTo) + @"
       , DISCOUNT_KIND_ID = " + SQLInt(model.DiscountKindId) + @"
       , D_VALUE = " + SQLDecimal(model.Value) + @"
 WHERE ID = " + SQLInt(model.DiscountId);

             ExecuteNonQuery(commandText);
        }

        public void Edit(DiscountGroupModel model)
        {
            string commandText =
@"UPDATE DISCOUNT_GROUP
     SET DG_NAME = " + SQLString(model.Name) + @"
 WHERE ID = " + SQLInt(model.DiscountGroupId);

            ExecuteNonQuery(commandText);
        }

        public void SetGroupItems(int groupId, string discountList)
        {
            // Добавя новите
            string commandText = String.Format(
@"INSERT INTO DISCOUNT_GROUP_ITEM
  (DISCOUNT_GROUP_ID, DISCOUNT_ID)
  SELECT {0} AS DISCOUNT_GROUP_ID
	   , d.ID AS DISCOUNT_ID
  FROM DISCOUNT d
  LEFT JOIN DISCOUNT_GROUP_ITEM dgi
    ON dgi.DISCOUNT_GROUP_ID = {0}
   AND dgi.DISCOUNT_ID = d.ID
  WHERE d.ID IN ({1})
  AND dgi.DISCOUNT_GROUP_ID IS NULL", groupId, discountList);

            ExecuteNonQuery(commandText);

            // Премахва изтритите
            commandText =  String.Format(
@"DELETE FROM DISCOUNT_GROUP_ITEM
  WHERE DISCOUNT_GROUP_ID = {0}
    AND DISCOUNT_ID NOT IN ({1}) ", groupId, discountList);

            ExecuteNonQuery(commandText);
        }
    }
}