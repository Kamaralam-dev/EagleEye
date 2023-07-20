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
    public class AccountSocialRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }


        public List<AccountSocialModel> GetAccountSocialById(int accountId)
        {
            try
            {
                connection();
                con.Open();
                IList<AccountSocialModel> accountSocialList = con.Query<AccountSocialModel>("Account_Social_Fetch", new { AccountId = accountId }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                accountSocialList = accountSocialList.Count == 0 ? new List<AccountSocialModel>() { new AccountSocialModel() } : accountSocialList;
                return accountSocialList.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public void UpsertAccountSocial(AccountSocialModel accountSocial)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("Account_Social_Upsert", accountSocial, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAccountSocial(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountSocialId", Id);
                connection();
                con.Open();
                con.Execute("Account_Social_Delete", param, commandType: CommandType.StoredProcedure);
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
