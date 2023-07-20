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
   public class ItemRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertItem(ItemModel Item)
        {
            int newItemId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@ItemId", Item.ItemId);
                _params.Add("@ItemNumber", Item.ItemNumber);
                _params.Add("@ItemName", Item.ItemName);
                _params.Add("@DonerId", Item.DonerId);
                _params.Add("@CategoryId", Item.CategoryId);
                _params.Add("@SubCategoryId", Item.SubCategoryId);
                _params.Add("@PurchaseDate", Item.PurchaseDate);
                _params.Add("@Size", Item.Size);
                _params.Add("@Color", Item.Color);
                _params.Add("@Condition", Item.Condition);
                _params.Add("@WarrentyPeriod", Item.WarrentyPeriod);
                _params.Add("@MarketValue", Item.MarketValue);
                _params.Add("@Quantity", Item.Quantity);
                _params.Add("@Address1", Item.Address1);
                _params.Add("@Address2", Item.Address2);
                _params.Add("@City", Item.City);
                _params.Add("@State", Item.State);
                _params.Add("@Zip", Item.Zip);
                _params.Add("@Country", Item.Country);
                _params.Add("@ContactPerson", Item.ContactPerson);
                _params.Add("@Phone", Item.Phone);
                _params.Add("@Email", Item.Email);
                _params.Add("@CreatedBy", Item.CreatedBy);
                _params.Add("@CreatedOn", Item.CreatedOn);
                _params.Add("@ImageUrl", Item.ImageUrl);
                _params.Add("@Description", Item.Description);
                _params.Add("@ClaimByDate", Item.ClaimByDate);
                _params.Add("@ItemType", Item.ItemType);
                _params.Add("@NewItemId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Item_Upsert", _params, commandType: CommandType.StoredProcedure);
                newItemId = _params.Get<int>("NewItemId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newItemId;

        }

        public bool DeleteItem(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ItemId", Id);
                connection();
                con.Open();
                con.Execute("Item_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ItemModel GetItemById(int ItemId)
        {
            try
            {
                connection();
                con.Open();
                ItemModel ItemList = con.Query<ItemModel>("Item_Fetch", new { ItemId = ItemId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return ItemList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<ItemModel> GetAllItem(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<ItemModel> ItemList = con.Query<ItemModel>("Item_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (ItemList.Count() > 0)
                    totalRows = ItemList.FirstOrDefault().TotalRows;

                con.Close();

                return ItemList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<UserModel> GetDonerByUserType(string UserType)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@UserTypeAbv", UserType);
                connection();
                con.Open();
                IList<UserModel> UserTypeList = con.Query<UserModel>("User_Fetch_ByUserType", param, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return UserTypeList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<CategoryModel> GetCategory()
        {
            try
            {
                connection();
                con.Open();
                IList<CategoryModel> CategoryList = con.Query<CategoryModel>("Category_Fetch_Category", commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return CategoryList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<CategoryModel> GetSubCategory(int CategoryId)
        {
            try
            {
                connection();
                con.Open();
                IList<CategoryModel> CategoryList = con.Query<CategoryModel>("Category_Fetch_SubCategory", new { CategoryId = CategoryId }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return CategoryList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<OrderDetailViewModel> GetOrderItemByDonor(int orderId,int donorId)
        {
            try
            {
                connection();
                con.Open();
                IList<OrderDetailViewModel> orderItems = con.Query<OrderDetailViewModel>("Item_FetchOrder", new { orderId = orderId, donorId= donorId }, commandType: System.Data.CommandType.StoredProcedure).ToList();
                con.Close();
                return orderItems.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public int UpsertOrderItem(OrderDetailModel Items)
        {
            int newItemId = 0;
            try
            {
                connection();
                con.Open();

                con.Execute("Upsert_OrderItem", Items, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newItemId;

        }

        public int UpdateItemReceiver(int itemId,int? primaryRecevierId,int? secondaryRecevierId)
        {
            int result = 0;
            try
            {
                connection();
                con.Open();

                con.Execute("Update_ItemReceiver", new { ItemId =itemId, PrimaryRecevierId=primaryRecevierId , SecondryRecevierId=secondaryRecevierId }, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                result = 1;
            }
            return result;

        }

        public List<ItemModel> GetAllItemByDonor(int donorId, int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
        {
            //API missing -remove this commnet when creaing API
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("DonorId", donorId);//donor will check here with donor/primary and secondary receiver
                param.Add("page", page);
                param.Add("size", size);
                param.Add("sortby", sortBy);
                param.Add("sortOrder", sortOrder);
                param.Add("searchText", searchText);
                param.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection();
                con.Open();

                IList<ItemModel> ItemList = con.Query<ItemModel>("Item_FetchAllDonor", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                con.Close();

                return ItemList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public int InsertRequestItem(RequestItemModel Item,out int outFlag)
        {
            int newItemId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@RequestId", Item.RequestId);
                _params.Add("@ItemId", Item.ItemId);
                _params.Add("@ReceiverId", Item.ReceiverId);
                _params.Add("@RequestDate", Item.RequestDate);
                _params.Add("@RequestType", Item.RequestType);
                _params.Add("@RequestNote", "Name : " + Item.ClaimByName + ", Phone : " + Item.ClaimByPhone + ", email : " + Item.ClaimByEmail);
                _params.Add("@outFlag", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("RequestItem_Upsert", _params, commandType: CommandType.StoredProcedure);
                outFlag = _params.Get<int>("outFlag");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newItemId;

        }

        public List<RequestItemViewModel> GetRequestItems(int userId,string userType,int page,int size,string sortBy,string sortOrder,string searchText, out int totalRows)
        {            
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("UserId", userId);
                param.Add("UserType", userType.ToUpper());
                param.Add("page", page);
                param.Add("size", size);
                param.Add("sortby", sortBy);
                param.Add("sortOrder", sortOrder);
                param.Add("searchText", searchText);
                param.Add("totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection();
                con.Open();

                IList<RequestItemViewModel> ItemList = con.Query<RequestItemViewModel>("RequestItem_Fetch", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (ItemList.Count() > 0)
                    totalRows = ItemList.FirstOrDefault().TotalRows;

                con.Close();

                return ItemList.ToList();               
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public void InsertRequestComments(RequestCommentModel model)
        {
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@CommentId", model.CommentId);
                _params.Add("@Comments", model.Comments);
                _params.Add("@CommentDate", model.CommentDate);
                _params.Add("@RequestId", model.RequestId);
                _params.Add("@CreatedBy", model.CreatedBy);
                con.Execute("RequestComment_Upsert", _params, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<dynamic> GetItemComment(int requestId)
        {
            try
            {
                connection();
                con.Open();
                IList<dynamic> commentList = con.Query<dynamic>("RequestComment_Fetch", new { RequestId = requestId }, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return commentList.ToList();
            }
            catch (Exception exe)
            {
                throw;
            }
        }

        public List<ItemModel> GetCities(string state)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("State", state);
                connection();
                con.Open();
                IList<ItemModel> cities = con.Query<ItemModel>("Item_FetchCities", param, commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return cities.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<ItemModel> GetStates()
        {
            try
            {
                connection();
                con.Open();
                IList<ItemModel> States = con.Query<ItemModel>("Item_FetchStates", commandType: CommandType.StoredProcedure).ToList();
                con.Close();

                return States.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<ItemModel> GetFilteredItems(int? categoryId,string city, string state)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("CategoryId", categoryId);
                param.Add("City", city);
                param.Add("State", state);
                connection();
                con.Open();
                IList<ItemModel> ItemList = con.Query<ItemModel>("Item_FetchByFilter", param, commandType: CommandType.StoredProcedure).ToList();
                
                con.Close();

                return ItemList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
