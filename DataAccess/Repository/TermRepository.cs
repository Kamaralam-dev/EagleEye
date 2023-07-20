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
   public class TermRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public void UpsertTerm(TermModel Term)
        {
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@TermText", Term.TermText);
                _params.Add("@UpdatedBy", Term.LastUpdatedBy);
                con.Execute("Term_Upsert", _params, commandType: CommandType.StoredProcedure);
                
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TermModel GetTerm()
        {
            try
            {
                connection();
                con.Open();
                TermModel TermTxt = con.Query<TermModel>("Term_Fetch", commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return TermTxt;
            }
            catch (Exception exe)
            {
                throw;
            }

        }
    }
}
