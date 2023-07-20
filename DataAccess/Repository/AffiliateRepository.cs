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
   public class AffiliateRepository
    {
        public SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConn"].ToString();
            con = new SqlConnection(constr);
        }

        public int UpsertAffiliate(AffiliateModel AffiliateModel)
        {
            int newAffiliateId = 0;
            try
            {
                connection();
                con.Open();

                DynamicParameters _params = new DynamicParameters();
                _params.Add("@AffiliateId", AffiliateModel.AffiliateId);
                _params.Add("@AffiliateTypeId", AffiliateModel.AffiliateTypeId);
                _params.Add("@FirstName", AffiliateModel.FirstName);
                _params.Add("@LastName", AffiliateModel.LastName);
                _params.Add("@Company", AffiliateModel.Company);
                _params.Add("@Address1", AffiliateModel.Address1);
                _params.Add("@Address2", AffiliateModel.Address2);
                _params.Add("@City", AffiliateModel.City);
                _params.Add("@State", AffiliateModel.State);
                _params.Add("@Zip", AffiliateModel.Zip);
                _params.Add("@Country", AffiliateModel.Country);
                _params.Add("@Phone", AffiliateModel.Phone);
                _params.Add("@Email", AffiliateModel.Email);
                _params.Add("@ServiceId", AffiliateModel.ServiceId);
                _params.Add("@ServiceDetails", AffiliateModel.ServiceDetails);
                _params.Add("@IsActive", AffiliateModel.IsActive);
                _params.Add("@CreatedBy", AffiliateModel.CreatedBy);
                _params.Add("@CreatedOn", AffiliateModel.CreatedOn);
                _params.Add("@ImageUrl", AffiliateModel.ImageUrl);
                _params.Add("@NewAffiliateId", DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("Affiliate_Upsert", _params, commandType: CommandType.StoredProcedure);
                newAffiliateId = _params.Get<int>("NewAffiliateId");
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newAffiliateId;
        }

        public bool DeleteAffiliate(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AffiliatedId", Id);
                connection();
                con.Open();
                con.Execute("Affiliate_Delete", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AffiliateModel GetAffiliateById(int AffiliateId)
        {
            try
            {
                connection();
                con.Open();
                AffiliateModel AffiliateModelList = con.Query<AffiliateModel>("Affiliate_Fetch", new { AffiliateId = AffiliateId }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                con.Close();
                return AffiliateModelList;
            }
            catch (Exception exe)
            {
                throw;
            }

        }

        public List<AffiliateModel> GetAllAffiliate(int page, int size, string sortBy, string sortOrder, string searchText, out int totalRows)
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

                IList<AffiliateModel> AffiliateList = con.Query<AffiliateModel>("Affiliate_FetchAll", param, commandType: CommandType.StoredProcedure).ToList();
                totalRows = param.Get<int>("totalRow");
                if (AffiliateList.Count() > 0)
                    totalRows = AffiliateList.FirstOrDefault().TotalRows;

                con.Close();

                return AffiliateList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }

        public List<AffiliateTypeModel> GetAllAffiliateType()
        {
            try
            {
                connection();
                con.Open();
                IList<AffiliateTypeModel> AffiliateTypeList = con.Query<AffiliateTypeModel>("AffiliateType_FetchAll", commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return AffiliateTypeList.ToList();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
    }
}
