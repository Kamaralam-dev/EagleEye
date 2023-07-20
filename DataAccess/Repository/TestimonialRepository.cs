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
   public class TestimonialRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertTestimonial(TestimonialModel Testimonial)
        {
            int newTestimonialId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters(Testimonial);
                _params.Add("@NewTestimonialId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Testimonial_Upsert", _params, commandType: CommandType.StoredProcedure);
                newTestimonialId = _params.Get<int>("NewTestimonialId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newTestimonialId;
        }

        public bool DeleteTestimonial(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TestimonialId", Id);
                connection();
                con.Open();
                con.Execute("Testimonial_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TestimonialModel GetTestimonialById(int TestimonialId)
        {
            try
            {
                connection();
                con.Open();
                TestimonialModel TestimonialList = con.Query<TestimonialModel>("Testimonial_Fetch", new { TestimonialId = TestimonialId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return TestimonialList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<TestimonialModel> GetAllTestimonial(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<TestimonialModel> TestimonialList = con.Query<TestimonialModel>("Testimonial_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (TestimonialList.Count() > 0)
                    totalRows = TestimonialList.FirstOrDefault().TotalRows;

                con.Close();

                return TestimonialList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

    }
}
