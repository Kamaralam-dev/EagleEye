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
   public class FaqRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertFaq(FaqModel FaqModel)
        {
            int newFaqId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(FaqModel);
                _params.Add("@NewFaqId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Faq_Upsert", _params, commandType: CommandType.StoredProcedure);
                newFaqId = _params.Get<int>("NewFaqId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newFaqId;
        }

        public bool DeleteFaq(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@FaqId", Id);
                connection();
                con.Open();
                con.Execute("Faq_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FaqModel GetFaqById(int FaqId)
        {
            try
            {
                connection();
                con.Open();
                FaqModel FaqList = con.Query<FaqModel>("Faq_Fetch", new { FaqId = FaqId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return FaqList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<FaqModel> GetAllFaq(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<FaqModel> FaqList = con.Query<FaqModel>("Faq_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (FaqList.Count() > 0)
                    totalRows = FaqList.FirstOrDefault().TotalRows;

                con.Close();

                return FaqList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
