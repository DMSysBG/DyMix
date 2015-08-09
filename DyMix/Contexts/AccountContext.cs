using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DyMix.Models;
using DMSys.Systems;

namespace DyMix.Contexts
{
    public class AccountContext : DMSys.Data.MSSQLUtility
    {
        public AccountContext()
            : base(xConfig.ConnectionString)
        { }

        public AccountModel Login(string userName, string passWord)
        {
            AccountModel model = null;
            string commandText =
@"SELECT a.ID
       , a.A_EMAIL
       , a.FIRST_NAME
       , a.LAST_NAME
       , a.LANGUAGE_CODE
 FROM S_ACCOUNT a
 WHERE a.A_USERNAME = " + SQLString(userName) + @"
   AND a.A_PASSWORD = " + SQLStringMD5(passWord) + @"
   AND a.IS_ACTIVE = 1 ";

            using( DataTable dtAccount = FillDataTable(commandText))
            {
                // Има открит логин
                if (dtAccount.Rows.Count == 1)
                {
                    DataRow drAccount = dtAccount.Rows[0];
                    model = new AccountModel()
                    {
                        ActionId = TryParse.ToInt32(drAccount["ID"]),
                        FirstName = TryParse.ToString(drAccount["FIRST_NAME"]),
                        LastName = TryParse.ToString(drAccount["LAST_NAME"]),
                        LanguageCode = TryParse.ToString(drAccount["LANGUAGE_CODE"])
                    };
                    // Отбелязва логването
                    commandText =
@"UPDATE S_ACCOUNT
    SET LOGIN_SESSION_ID = " + SQLString(xSession.SessionId) + @"
      , LOGIN_LAST_DATE = GETDATE()
 WHERE ID = " + SQLInt(model.ActionId);

                    ExecuteNonQuery(commandText);
                }
            }
            return model;
        }
    }
}