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
   public class StoryRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertStory(StoryModel StoryModel)
        {
            int newStoryId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(StoryModel);
                _params.Add("@NewStoryId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Story_Upsert", _params, commandType: CommandType.StoredProcedure);
                newStoryId = _params.Get<int>("NewStoryId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newStoryId;
        }

        public bool DeleteStory(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@StoryId", Id);
                connection();
                con.Open();
                con.Execute("Story_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public StoryModel GetStoryById(int StoryId)
        {
            try
            {
                connection();
                con.Open();
                StoryModel StoryModelList = con.Query<StoryModel>("Story_Fetch", new { StoryId = StoryId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return StoryModelList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<StoryModel> GetAllStory(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<StoryModel> StoryList = con.Query<StoryModel>("Story_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (StoryList.Count() > 0)
                    totalRows = StoryList.FirstOrDefault().TotalRows;
                con.Close();

                return StoryList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
