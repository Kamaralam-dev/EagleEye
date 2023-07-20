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
using DataAccess.ViewModels;

namespace DataAccess.Repository
{
    public class AccountCustomFieldRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }
        public void UpsertAccountCustomField(AccountCustomFieldModel AccountCustomField)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("Account_CustomField_Upsert", AccountCustomField, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AccountCustomFieldModel> GetAllAccountCustomFields(int companyId, int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<AccountCustomFieldModel> AccountCustomFieldList = con.Query<AccountCustomFieldModel>("Account_CustomField_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                con.Close();

                return AccountCustomFieldList.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public bool SetOrderAccountCustomField(int id, int newSortOrder)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountCustomFieldId", id);
                param.Add("@NewOrder", newSortOrder);
                connection();
                con.Open();
                con.Execute("Account_CustomField_UpdateSortOrder", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAccountCustomField(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AccountCustomFieldId", Id);
                connection();
                con.Open();
                con.Execute("Account_CustomField_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AccountCustomFieldModel GetAccountCustomFieldById(int AccountCustomFieldId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("AccountCustomFieldId", AccountCustomFieldId);
                connection();
                con.Open();
                AccountCustomFieldModel CustomFieldList = con.Query<AccountCustomFieldModel>("Account_CustomField_Fetch", new { AccountCustomFieldId = AccountCustomFieldId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return CustomFieldList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<AccountCustomFieldViewModel> GetAccountCustomFields(int companyId, int accountId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("CompanyId", companyId);
                param.Add("AccountId", accountId);
                connection();
                con.Open();
                List<AccountCustomFieldViewModel> CustomFields = con.Query<AccountCustomFieldViewModel>("Account_CustomField_FetchAccount", new { CompanyId = companyId, AccountId = accountId }, commandType: System.Data.CommandType.StoredProcedure).ToList();
                con.Close();
                return CustomFields;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public void UpsertCustomFieldValues(AccountCustomFieldValueModel accountCustomFieldValueModel)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("Account_CustomFieldValue_Upsert", accountCustomFieldValueModel, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
