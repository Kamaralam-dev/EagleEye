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
   public class OrderRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertOrder(OrderModel Order)
        {
            int newOrderId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@OrderId", Order.OrderId);
                _params.Add("@DonorId", Order.DonorId);
                _params.Add("@PrimaryReceiverId", Order.PrimaryReceiverId);
                _params.Add("@SecondaryReceiverId", Order.SecondaryReceiverId);
                _params.Add("@Orderdate", Order.OrderDate);
                _params.Add("@OrderAmount", Order.OrderAmount);
                _params.Add("@OrderTitle", Order.OrderTitle);
                _params.Add("@DonorAddress1", Order.DonorAddress1);
                _params.Add("@DonorAddress2", Order.DonorAddress2);
                _params.Add("@DonorCity", Order.DonorCity);
                _params.Add("@DonorState", Order.DonorState);
                _params.Add("@DonorZip", Order.DonorZip);
                _params.Add("@DonorCountry", Order.DonorCountry);
                _params.Add("@ReceiverAddress1", Order.ReceiverAddress1);
                _params.Add("@ReceiverAddress2", Order.ReceiverAddress2);
                _params.Add("@ReceiverCity", Order.ReceiverCity);
                _params.Add("@ReceiverState", Order.ReceiverState);
                _params.Add("@ReceiverZip", Order.ReceiverZip);
                _params.Add("@ReceiverCountry", Order.ReceiverCountry);
                _params.Add("@CreatedBy", Order.CreatedBy);
                _params.Add("@CreatedOn", Order.CreatedOn);
                _params.Add("@NewOrderId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Order_Upsert", _params, commandType: CommandType.StoredProcedure);
                newOrderId = _params.Get<int>("NewOrderId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newOrderId;

        }

        public bool DeleteOrder(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@OrderId", Id);
                connection();
                con.Open();
                con.Execute("Order_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public OrderModel GetOrderById(int OrderId)
        {
            try
            {
                connection();
                con.Open();
                OrderModel OrderList = con.Query<OrderModel>("Order_Fetch", new { OrderId = OrderId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return OrderList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<OrderModel> GetAllOrder(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<OrderModel> OrderList = con.Query<OrderModel>("Order_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (OrderList.Count() > 0)
                    totalRows = OrderList.FirstOrDefault().TotalRows;
                con.Close();

                return OrderList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }


    }
}
