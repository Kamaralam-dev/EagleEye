using DataAccess.ApiHelper;
using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using DataAccess;

namespace Prototype.API.Controllers
{

    public class UserController : ApiController
    {
        #region Declaration
        UserRepository _userRepo = new UserRepository();
        #endregion

        [HttpGet]
        [Route("api/User/GetAllUsers")]
        public ResponseModel GetAllUsers(int companyId, int page, int size, string sortBy, string sortOrder, string searchText)
        {
            int totRows = 0;
            try
            {
                var users = _userRepo.GetAllUsers(page, size, sortBy, sortOrder, searchText.NullToString(), out totRows);
                if (users != null)
                    return new ResponseModel { IsSuccess = true, Message = users.Count + " records count", Data = users, outParam = totRows };
                else
                    return new ResponseModel { IsSuccess = false, Message = "some error occurred,please try again!", Data = users, outParam = totRows };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, Message = ex.Message, Data = null, outParam = totRows };
            }
        }

        [HttpPost]
        [Route("api/User/AuthenticateUser")]
        public ResponseModel AuthenticateUser([FromBody] UserModel model)
        {
            try
            {
                var user = _userRepo.AuthenticateUser(model);
                if (user != null)
                    return new ResponseModel { IsSuccess = true, Message = "Login successfully.", Data = user };
                else
                    return new ResponseModel { IsSuccess = false, Message = "EmailId or password is wrong.", Data = user };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, Message = ex.Message, Data = null };
            }
        }

        [HttpGet]
        [Route("api/User/GetUserById")]
        public ResponseModel GetUserById(int userId)
        {
            try
            {
                var user = _userRepo.GetUserById(userId);
                if (user != null)
                    return new ResponseModel { IsSuccess = true, Message = "1 record found", Data = user };
                else
                    return new ResponseModel { IsSuccess = false, Message = "no record found", Data = user };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, Message = ex.Message, Data = null };
            }
        }

        [HttpGet]
        [Route("api/User/Delete")]
        public ResponseModel Delete(int UserId)
        {
            try
            {
                var DeleteUser = _userRepo.DeleteUser(UserId);
                if (DeleteUser != false)
                    return new ResponseModel { IsSuccess = true, Message = "User deleted successfully.", Data = null };
                else
                    return new ResponseModel { IsSuccess = false, Message = "Some error occurred,please try again!", Data = null };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, Message = ex.Message, Data = null };
            }
        }

        [HttpPost]
        [Route("api/User/Create")]
        public ResponseModel Create([FromBody] UserModel model)
        {
            try
            {

                if (_userRepo.UpsertUser(model) == 1)
                    return new ResponseModel { IsSuccess = false, Message = "Email already exists!", Data = null };
                else
                    return new ResponseModel { IsSuccess = true, Message = "Some error occurred,please try again!", Data = null };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, Message = ex.Message, Data = null };
            }
        }

        [HttpGet]
        [Route("api/User/CheckEmailAvailability")]
        public ResponseModel CheckEmailAvailability(string email)
        {
            try
            {
                var IsExistEmail = _userRepo.IsExistEmail(email);
                if (IsExistEmail != false)
                    return new ResponseModel { IsSuccess = true, Message = " ", Data = null };
                else
                    return new ResponseModel { IsSuccess = false, Message = "Some error occurred,please try again!", Data = null };
            }
            catch (Exception ex)
            {
                return new ResponseModel { IsSuccess = false, Message = ex.Message, Data = null };
            }
        }
    }
}



