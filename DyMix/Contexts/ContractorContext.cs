using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DyMix.Models;
using DMSys.Systems;

namespace DyMix.Contexts
{
    public class ContractorContext : DMSys.Data.MSSQLUtility
    {
        public ContractorContext()
            : base(xConfig.ConnectionString)
        { }

        public List<ContractorModel> GetContractors()
        {
            List<ContractorModel> model = new List<ContractorModel>();
            string commandText =
@"SELECT cnt.ID AS CONTRACTOR_ID
       , cnt.CONTRACTOR_TYPE_ID
       , cnt.C_NAME
       , cnt.ID_CONTRACTOR
       , cnt.S_TOWN_ID
       , cnt.C_ADDRESS
       , ISNULL(c_cmp.ACCOUNTABLE_PERSON, '') AS ACCOUNTABLE_PERSON
 FROM CONTRACTOR cnt
 LEFT JOIN CONTRACTOR_COMPANY c_cmp ON c_cmp.CONTRACTOR_ID = cnt.ID
 ORDER BY cnt.C_NAME ";

            using (DataTable dtContractors = FillDataTable(commandText))
            {
                foreach (DataRow drContractor in dtContractors.Rows)
                {
                    model.Add(new ContractorModel()
                    {
                        ContractorId = TryParse.ToInt32(drContractor["CONTRACTOR_ID"]),
                        Name = TryParse.ToString(drContractor["C_NAME"]),
                        ContractorTypeId = TryParse.ToInt32(drContractor["CONTRACTOR_TYPE_ID"]),
                        IdContractor = TryParse.ToString(drContractor["ID_CONTRACTOR"]),
                        TownId = TryParse.ToInt32(drContractor["S_TOWN_ID"]),
                        Address = TryParse.ToString(drContractor["C_ADDRESS"]),
                        AccountablePerson = TryParse.ToString(drContractor["ACCOUNTABLE_PERSON"])
                    });
                }
            }
            return model;
        }

        public ContractorModel GetContractor(int id)
        {
            ContractorModel model = null;
            string commandText =
@"SELECT cnt.ID AS CONTRACTOR_ID
       , cnt.CONTRACTOR_TYPE_ID
       , cnt.C_NAME
       , cnt.ID_CONTRACTOR
       , cnt.S_TOWN_ID
       , cnt.C_ADDRESS
       , ISNULL(c_cmp.ACCOUNTABLE_PERSON, '') AS ACCOUNTABLE_PERSON
 FROM CONTRACTOR cnt
 LEFT JOIN CONTRACTOR_COMPANY c_cmp ON c_cmp.CONTRACTOR_ID = cnt.ID
 WHERE cnt.ID = " + SQLInt(id);

            using (DataTable dtContractors = FillDataTable(commandText))
            {
                if( dtContractors.Rows.Count > 0)
                {
                    DataRow drContractor = dtContractors.Rows[0];
                    model = new ContractorModel()
                    {
                        ContractorId = TryParse.ToInt32(drContractor["CONTRACTOR_ID"]),
                        Name = TryParse.ToString(drContractor["C_NAME"]),
                        ContractorTypeId = TryParse.ToInt32(drContractor["CONTRACTOR_TYPE_ID"]),
                        IdContractor = TryParse.ToString(drContractor["ID_CONTRACTOR"]),
                        TownId = TryParse.ToInt32(drContractor["S_TOWN_ID"]),
                        Address = TryParse.ToString(drContractor["C_ADDRESS"]),
                        AccountablePerson = TryParse.ToString(drContractor["ACCOUNTABLE_PERSON"])
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// Добавя контрагент
        /// </summary>
        public void Add(ContractorModel model)
        {
            // Добавя контрагент
            string commandText =
@"INSERT INTO CONTRACTOR
  ( CONTRACTOR_TYPE_ID, C_NAME, ID_CONTRACTOR, S_TOWN_ID, C_ADDRESS )
 VALUES (" + SQLInt(model.ContractorTypeId) +
 ", " + SQLString(model.Name) +
 ", " + SQLString(model.IdContractor) +
 ", " + SQLInt(model.TownId) +
 ", " + SQLString(model.Address) + @")
 
SELECT @@IDENTITY";

            model.ContractorId = TryParse.ToInt32(ExecuteScalar(commandText));

            // Записва разширена информация за контаргента 
            SaveExtension(model);
        }

        /// <summary>
        /// Редактира контрагент
        /// </summary>
        public void Edit(ContractorModel model)
        {
            // Променя данните за контрагента
            string commandText =
@"UPDATE CONTRACTOR
     SET C_NAME = " +  SQLString(model.Name) + @"
       , ID_CONTRACTOR = " + SQLString(model.IdContractor) + @"
       , S_TOWN_ID = " + SQLInt(model.TownId) + @"
       , C_ADDRESS = " + SQLString(model.Address) + @"
 WHERE ID = " + SQLInt(model.ContractorId);

            ExecuteNonQuery(commandText);

            // Записва разширена информация за контаргента 
            SaveExtension(model);
        }

        /// <summary>
        /// Записва разширена информация за контаргента
        /// </summary>
        private void SaveExtension(ContractorModel model)
        {
            if (model.ContractorId == 0)
            { return; }
            // Ако е юридическо лице
            if (model.ContractorTypeId == 2)
            {
                string commandText = String.Format(
@"UPDATE CONTRACTOR_COMPANY
     SET ACCOUNTABLE_PERSON = {1}
  WHERE CONTRACTOR_ID = {0}
  IF @@ROWCOUNT = 0
  BEGIN
    INSERT INTO CONTRACTOR_COMPANY
    (CONTRACTOR_ID, ACCOUNTABLE_PERSON)
    VALUES ({0}, {1})
  END ", SQLInt(model.ContractorId), SQLString(model.AccountablePerson));

                ExecuteNonQuery(commandText);
            }
        }

        /// <summary>
        /// Списък контрагенти
        /// </summary>
        public List<ListItemModel> GetList(string term)
        {
            List<ListItemModel> model = new List<ListItemModel>();
            string termValue = (String.IsNullOrWhiteSpace(term) || term.Equals("*")) ? "" : term;

            string commandText =
@"SELECT TOP 15 c.ID, c.C_NAME
 FROM CONTRACTOR c
 WHERE c.C_NAME LIKE N'%" + termValue + @"%'
 ORDER BY c.C_NAME ";

            using (DataTable dtItems = FillDataTable(commandText))
            {
                foreach (DataRow drItem in dtItems.Rows)
                {
                    model.Add(new ListItemModel()
                    {
                        id = TryParse.ToString(drItem["ID"]),
                        label = TryParse.ToString(drItem["C_NAME"]),
                        abbrev = ""
                    });
                }
            }
            return model;
        }
    }
}