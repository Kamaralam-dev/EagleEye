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
   public class SettingRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertSetting(SettingModel SettingModel)
        {
            int newSettingId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(SettingModel);              
                con.Execute("Setting_Upsert", _params, commandType: CommandType.StoredProcedure);             
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newSettingId;
        }

        public bool DeleteSetting(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@SettingId", Id);
                connection();
                con.Open();
                con.Execute("Setting_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SettingModel GetSettingById(int SettingId)
        {
            try
            {
                connection();
                con.Open();
                SettingModel SettingList = con.Query<SettingModel>("Setting_Fetch", new { SettingId = SettingId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return SettingList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        

        public List<SettingModel> GetAllSetting(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<SettingModel> SettingList = con.Query<SettingModel>("Setting_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (SettingList.Count() > 0)
                    totalRows = SettingList.FirstOrDefault().TotalRows;

                con.Close();

                return SettingList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
