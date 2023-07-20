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
    public class UserRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertUser(UserModel user,out int newUserId)
        {
            int result = 0;
            try
            {
                connection();
                con.Open();
                DynamicParameters _params = new DynamicParameters(user);
                _params.Add("@EmailExist", DbType.Int32, direction: ParameterDirection.Output);
                _params.Add("@NewUserId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("User_Upsert", _params, commandType: CommandType.StoredProcedure);

                result = _params.Get<int>("EmailExist");
                newUserId = _params.Get<int>("NewUserId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }

        public List<UserModel> GetAllUsers(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<UserModel> userList = con.Query<UserModel>("User_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                con.Close();

                return userList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }


        public bool DeleteUser(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", Id);
                connection();
                con.Open();
                con.Execute("User_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetUserById(int userId)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("UserId", userId);
                connection();
                con.Open();
                UserModel userList = con.Query<UserModel>("User_Fetch", new { UserId = userId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return userList;
            }
            catch (Exception exe)
            {
                throw exe;
            }

        }

        public UserModel AuthenticateUser(UserModel user)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Email", user.Email);
                param.Add("Password", user.Password);
                connection();
                con.Open();
                UserModel userList = con.Query<UserModel>("User_Authenticate", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                con.Close();

                return userList;
            }
            catch (Exception exe)
            {
                throw exe;
            }

        }

        public bool IsExistEmail(string email)
        {
            bool result = false;
            try
            {
                connection();
                con.Open();
                DynamicParameters _params = new DynamicParameters();
                _params.Add("Email", email);
                _params.Add("IsExist", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("User_CheckEmailAvailability", _params, commandType: CommandType.StoredProcedure);
                result = _params.Get<int>("IsExist") == 1;
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdatePassword(int userId,string password)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserId", userId);
                param.Add("@Password", password);
                connection();
                con.Open();
                con.Execute("User_UpdatePassword", param, commandType: CommandType.StoredProcedure);
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
