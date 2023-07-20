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
   public class PaymentRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertPayment(PaymentModel Payment)
        {
            int newPaymentId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@PaymentId", Payment.PaymentId);
                _params.Add("@OrderId", Payment.OrderId);
                _params.Add("@PaymentDate", Payment.PaymentDate);
                _params.Add("@PaymentAmount", Payment.PaymentAmount);
                _params.Add("@PaymnetModeId", Payment.PaymnetModeId);
                _params.Add("@PaymentDetail", Payment.PaymentDetail);
                _params.Add("@CreatedBy", Payment.CreatedBy);
                _params.Add("@CreatedOn", Payment.CreatedOn);
                _params.Add("@NewPaymentId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Payment_Upsert", _params, commandType: CommandType.StoredProcedure);
                newPaymentId = _params.Get<int>("NewPaymentId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newPaymentId;

        }

        public bool DeletePayment(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@PaymentId", Id);
                connection();
                con.Open();
                con.Execute("Payment_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PaymentModel GetPaymentById(int PaymentId)
        {
            try
            {
                connection();
                con.Open();
                PaymentModel PaymentList = con.Query<PaymentModel>("Payment_Fetch", new { PaymentId = PaymentId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return PaymentList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<PaymentModel> GetAllPayment(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<PaymentModel> PaymentList = con.Query<PaymentModel>("Payment_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (PaymentList.Count() > 0)
                    totalRows = PaymentList.FirstOrDefault().TotalRows;
                con.Close();

                return PaymentList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<PaymentModeModel> GetPaymentMode()
        {
            try
            {
                connection();
                con.Open();
                IList<PaymentModeModel> PaymentModeList = con.Query<PaymentModeModel>("PaymentMode_FetchAll", commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return PaymentModeList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

    }
}
