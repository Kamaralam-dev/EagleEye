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
   public class TeamRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertTeam(TeamModel Team)
        {
            int newTeamId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(Team);
                _params.Add("@NewTeamId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Team_Upsert", _params, commandType: CommandType.StoredProcedure);
                newTeamId = _params.Get<int>("NewTeamId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newTeamId;            

        }

        public bool DeleteTeam(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TeamId", Id);
                connection();
                con.Open();
                con.Execute("Team_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TeamModel GetTeamById(int TeamId)
        {
            try
            {
                connection();
                con.Open();
                TeamModel AccountList = con.Query<TeamModel>("Team_Fetch", new { TeamId = TeamId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return AccountList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<TeamModel> GetAllTeam(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<TeamModel> teamList = con.Query<TeamModel>("Team_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (teamList.Count() > 0)
                    totalRows = teamList.FirstOrDefault().TotalRows;
                con.Close();

                return teamList.ToList();
            }
            catch (Exception exe)
            {
                 throw exe;
            }
        }

        public bool UpdateTeamOrderNo(int teamId,int orderNo)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TeamId", teamId);
                param.Add("@OrderNo", orderNo);
                connection();
                con.Open();
                con.Execute("Team_UpdateOrderNo", param, commandType: CommandType.StoredProcedure);
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
