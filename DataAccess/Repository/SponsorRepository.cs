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
   public class SponsorRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertSponsor(SponsorModel Sponsor)
        {
            int newSponsorId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(Sponsor);
                _params.Add("@NewId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Sponsor_Upsert", _params, commandType: CommandType.StoredProcedure);
                newSponsorId = _params.Get<int>("NewId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newSponsorId;            

        }

        public bool DeleteSponsor(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                connection();
                con.Open();
                con.Execute("Sponsor_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SponsorModel GetSponsorById(int SponsorId)
        {
            try
            {
                connection();
                con.Open();
                SponsorModel AccountList = con.Query<SponsorModel>("Sponsor_Fetch", new {Id = SponsorId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return AccountList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<SponsorModel> GetAllSponsor(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<SponsorModel> SponsorList = con.Query<SponsorModel>("Sponsor_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (SponsorList.Count() > 0)
                    totalRows = SponsorList.FirstOrDefault().TotalRows;
                con.Close();

                return SponsorList.ToList();
            }
            catch (Exception exe)
            {
                 throw exe;
            }
        }

        public bool UpdateSponsorOrderNo(int SponsorId,int orderNo)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", SponsorId);
                param.Add("@SortOrder", orderNo);
                connection();
                con.Open();
                con.Execute("Sponsor_UpdateOrderNo", param, commandType: CommandType.StoredProcedure);
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
