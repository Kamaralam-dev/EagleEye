using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataAccess.Repository
{
   public class AccountPhoneRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }
     
        public List<AccountPhoneModel> GetAccountPhoneById(int accountId)
        {
            try
            {
                connection();
                con.Open();
                IList<AccountPhoneModel> accountPhoneList = con.Query<AccountPhoneModel>("Account_Phone_Fetch", new { AccountId=accountId }, commandType: CommandType.StoredProcedure).ToList();              
                con.Close();
                accountPhoneList = accountPhoneList.Count == 0 ? new List<AccountPhoneModel>() { new AccountPhoneModel() } : accountPhoneList;
                return accountPhoneList.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public void UpsertAccountPhone(AccountPhoneModel accountPhone)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("Account_Phone_Upsert", accountPhone, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAccountPhone(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountPhoneId", Id);
                connection();
                con.Open();
                con.Execute("Account_Phone_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
