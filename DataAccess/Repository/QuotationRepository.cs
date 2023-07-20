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
   public class QuotationRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertQuotation(QuotationModel QuotationModel)
        {
            int newQuotationId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(QuotationModel);
                _params.Add("@NewQuotationId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Quotation_Upsert", _params, commandType: CommandType.StoredProcedure);
                newQuotationId = _params.Get<int>("NewQuotationId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newQuotationId;
        }

        public bool DeleteQuotation(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@QuotationId", Id);
                connection();
                con.Open();
                con.Execute("Quotation_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public QuotationModel GetQuotationById(int QuotationId)
        {
            try
            {
                connection();
                con.Open();
                QuotationModel QuotationModelList = con.Query<QuotationModel>("Quotation_Fetch", new { QuotationId = QuotationId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return QuotationModelList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<QuotationModel> GetAllQuotation(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<QuotationModel> QuotationList = con.Query<QuotationModel>("Quotation_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (QuotationList.Count() > 0)
                    totalRows = QuotationList.FirstOrDefault().TotalRows;
                con.Close();

                return QuotationList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
