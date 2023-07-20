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
   public class ContactUsRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertContactUs(ContactUsModel ContactUsModel)
        {
            int newContactUsId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@ContactUsId", ContactUsModel.ContactUsId);
                _params.Add("@CompanyName", ContactUsModel.CompanyName);
                _params.Add("@ContactName", ContactUsModel.ContactName);
                _params.Add("@Title", ContactUsModel.Title);
                _params.Add("@Address", ContactUsModel.Address);
                _params.Add("@City", ContactUsModel.City);
                _params.Add("@State", ContactUsModel.State);
                _params.Add("@Zip", ContactUsModel.Zip);
                _params.Add("@Country", ContactUsModel.Country);
                _params.Add("@Phone", ContactUsModel.Phone);
                _params.Add("@Email", ContactUsModel.Email);
                _params.Add("@Query", ContactUsModel.Query);
                _params.Add("@Subject", ContactUsModel.Subject);
                _params.Add("@NewContactUsId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("ContactUs_Upsert", _params, commandType: CommandType.StoredProcedure);
                newContactUsId = _params.Get<int>("NewContactUsId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newContactUsId;
        }

        public bool DeleteContactUs(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ContactUsId", Id);
                connection();
                con.Open();
                con.Execute("ContactUs_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ContactUsModel GetContactUsById(int ContactUsId)
        {
            try
            {
                connection();
                con.Open();
                ContactUsModel ContactUsModelList = con.Query<ContactUsModel>("ContactUs_Fetch", new { ContactUsId = ContactUsId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return ContactUsModelList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<ContactUsModel> GetAllContactUs(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("page", page);
                param.Add("size", size);
                param.Add("sortby", sortBy);
                param.Add("sortOrder", sortOrder);
                param.Add("searchText", searchText);
                param.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection();
                con.Open();

                IList<ContactUsModel> ContactUsList = con.Query<ContactUsModel>("ContactUs_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (ContactUsList.Count() > 0)
                    totalRows = ContactUsList.FirstOrDefault().TotalRows;
                con.Close();

                return ContactUsList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
