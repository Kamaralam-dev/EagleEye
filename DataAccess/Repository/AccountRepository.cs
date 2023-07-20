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
   public class AccountRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertAccount(AccountModel account)
        {
            int newAccountId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(account);
                _params.Add("@NewAccountId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Account_Upsert", _params, commandType: CommandType.StoredProcedure);
                newAccountId = _params.Get<int>("NewAccountId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newAccountId;

        }
        public List<AccountModel> GetAllAccounts(int companyId, int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("companyId", companyId);
                param.Add("page", page);
                param.Add("size", size);
                param.Add("sortby", sortBy);
                param.Add("sortOrder", sortOrder);
                param.Add("searchText", searchText);
                param.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection();
                con.Open();

                IList<AccountModel> AccountList = con.Query<AccountModel>("Account_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                con.Close();

                return AccountList.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }
        public bool DeleteAccount(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountId", Id);
                connection();
                con.Open();
                con.Execute("Account_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AccountModel GetAccountById(int AccountId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("AccountId", AccountId);
                connection();
                con.Open();
                AccountModel AccountList = con.Query<AccountModel>("Account_Fetch", new { AccountId = AccountId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return AccountList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

    }
}
