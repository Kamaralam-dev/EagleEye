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
   public class CategoryRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertCategory(CategoryModel model)
        {
            int newCategoryId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(model);
                _params.Add("@NewCategoryId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Category_Upsert", _params, commandType: CommandType.StoredProcedure);
                newCategoryId = _params.Get<int>("NewCategoryId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newCategoryId;
        }

        public bool DeleteCategory(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CategoryId", Id);
                connection();
                con.Open();
                con.Execute("Category_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CategoryModel GetCategoryById(int CategoryId)
        {
            try
            {
                connection();
                con.Open();
                CategoryModel CategoryList = con.Query<CategoryModel>("Category_Fetch", new { CategoryId = CategoryId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return CategoryList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<CategoryModel> GetAllCategory()
        {
            try
            {               
                connection();
                con.Open();
                IList<CategoryModel> CategoryList = con.Query<CategoryModel>("Category_FetchAll",  commandType: CommandType.StoredProcedure).ToList();             
                con.Close();

                return CategoryList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
