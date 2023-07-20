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
    public class CommonRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public List<CountryModel> GetAllCountries()
        {
            try
            {
                connection();
                con.Open();
                IList<CountryModel> countries = con.Query<CountryModel>("Country_FetchAll", commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return countries.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public List<IndustryModel> GetIndustries(int? companyId)
        {
            try
            {
                connection();
                con.Open();
                IList<IndustryModel> industries = con.Query<IndustryModel>("Industry_FetchAll", new { CompanyId = companyId ,page = 0, size = 1000, sortby = "Industry", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return industries.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public List<RatingModel> GetRating(int? companyId)
        {
            try
            {
                connection();
                con.Open();
                IList<RatingModel> ratings = con.Query<RatingModel>("Rating_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "Rating", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return ratings.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public List<SourceModel> GetSources(int? companyId)
        {
            try
            {
                connection();
                con.Open();
                IList<SourceModel> sources = con.Query<SourceModel>("Source_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "Source", sortOrder = "Asc", searchText = "", sendAll=1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return sources.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public List<AccountTypeModel> GetAccountTypes(int? companyId)
        {
            try
            {
                connection();
                con.Open();
                IList<AccountTypeModel> accountTypes = con.Query<AccountTypeModel>("Account_Type_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "AccountType", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return accountTypes.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public List<AccountModel> GetAccountList(int? companyId)
        {
            try
            {
                connection();
                con.Open();
                IList<AccountModel> accountTypes = con.Query<AccountModel>("Account_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "CreatedOn", sortOrder = "Desc", searchText = "", totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return accountTypes.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        //public List<SaleStageModel> GetSaleStage(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<SaleStageModel> SaleStage = con.Query<SaleStageModel>("Sale_Stage_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "SaleStage", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return SaleStage.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}
        //public List<ContactModel> GetContact(int? accountId)
        //{
        //    try
        //    {

        //        connection();
        //        con.Open();
        //        var sortColumn = "FirstName";
        //        sortColumn = sortColumn == "AccountName" ? "A.Name" : sortColumn == "Owner" ? "U.FirstName" : "C." + sortColumn;
        //        IList<ContactModel> Contact = con.Query<ContactModel>("Contact_FetchAll", new { AccountId = accountId, page = 0, size = 1000, sortby = sortColumn, sortOrder = "Asc", searchText = "", totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();
        //        return Contact.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        //public List<ServiceModel> GetService(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<ServiceModel> services = con.Query<ServiceModel>("Service_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "ServiceName", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return services.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        //public List<LeadModel> GetLead(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<LeadModel> lead = con.Query<LeadModel>("Lead_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "LeadId", sortOrder = "Asc", searchText = "", totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return lead.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        //public List<ActivityTypeModel> GetType(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<ActivityTypeModel> type = con.Query<ActivityTypeModel>("Activity_Type_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "AccountTypeId", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return type.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        //public List<ActivityStatusModel> GetStatus(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<ActivityStatusModel> status = con.Query<ActivityStatusModel>("Activity_Status_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "ActivityStatusId", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return status.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        //public List<LeadStatusModel> GetLeadStatus(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<LeadStatusModel> status = con.Query<LeadStatusModel>("LeadStatus_FetchAll", new { CompanyId = companyId, page = 0, size = 1000, sortby = "LeadStatusId", sortOrder = "Asc", searchText = "", sendAll = 1, totalrow = 0 }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return status.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}


        public List<CompanyModel> GetAllCompany()
        {
            try
            {
                connection();
                con.Open();
                IList<CompanyModel> status = con.Query<CompanyModel>("Company_FetchAll", commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return status.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }


        public CompanyModel GetCompanyByCompId(int? companyId)
        {
            try
            {
                connection();
                con.Open();
               CompanyModel company = con.Query<CompanyModel>("Company_FetchByCompId", new { CompanyId = companyId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                con.Close();

                return company;
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        //public List<CompanyCurrencyModel> GetAllCompanyCurrency()
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<CompanyCurrencyModel> countriesCurrencies = con.Query<CompanyCurrencyModel>("CompanyCurrency_FetchAll", commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();
        //        return countriesCurrencies.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        //public List<CountryModel> GetCurrencyByCompnyId(int? companyId)
        //{
        //    try
        //    {
        //        connection();
        //        con.Open();
        //        IList<CountryModel> Currency = con.Query<CountryModel>("CompanyCurrency_FetchAllByCompId", new { CompanyId = companyId }, commandType: CommandType.StoredProcedure).ToList();
        //        con.Close();

        //        return Currency.ToList();
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}
        //public SettingModel GetSettingByCompanyId(int CompanyId)
        //{
        //    try
        //    {
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("CompanyId", CompanyId);
        //        connection();
        //        con.Open();
        //        SettingModel SettingList = con.Query<SettingModel>("Setting_Fetch", new { CompanyId = CompanyId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        //        con.Close();
        //        return SettingList;
        //    }
        //    catch (Exception exe)
        //    {
        //        throw;
        //    }
        //}

        public List<UserTypeViewModel> GetUserType()
        {
            try
            {
                connection();
                con.Open();
                IList<UserTypeViewModel> sources = con.Query<UserTypeViewModel>("UserType_Fetch", new { }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return sources.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

      
    }
}
