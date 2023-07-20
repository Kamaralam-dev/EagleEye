using DataAccess.Models;
using DataAccess.Repository;
using EagleEye.CommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.ApiHelper;
using System.IO;
using DataAccess.ViewModels;
using System.Configuration;

namespace EagleEye.Controllers
{

    public class UsersController : Controller
    {
        #region Declaration
        UserRepository _userRepo = new UserRepository();
        #endregion

        #region Authentication  

        public ActionResult Login()
        {
            //login lading page
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(UserModel model)
        {
            try
            {
                //login validate
                var user = _userRepo.AuthenticateUser(model);
                if (user != null)
                {
                    Utility.SetCookie("LoggedUserId", user.UserId.ToString());
                    Utility.SetCookie("LoggedUserEmailId", user.Email);
                    Utility.SetCookie("LoggedUserName", user.UserName);
                    Utility.SetCookie("LoggedUserTypeId", user.UserTypeId.NullToString());
                    Utility.SetCookie("LoggedUserType", user.UserTypeId == 1 ? "Admin" : "User");

                    return new ActionState { Message = "Congratulations!", Data = "Login successfully.", Success = true, Type = ActionState.SuccessType, OptionalValue = user.UserTypeId.NullToString() }.ToActionResult(HttpStatusCode.OK);
                }
                else
                    return new ActionState { Message = "Failed!", Data = "Email or password is wrong.", Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ActionState { Message = "Failed!", Data = ex.Message, Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.OK);
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Utility.RemoveCookie("LoggedUserId");
            Utility.RemoveCookie("LoggedUserEmailId");
            Utility.RemoveCookie("LoggedUserTypeId");
            Utility.RemoveCookie("LoggedUserType");
            return RedirectToAction("Login");
        }      

        public ActionResult CheckEmailAvailability(string email)
        {           
                if (_userRepo.IsExistEmail(email))
                {
                    return new ActionState { Message = "Email already exists!", Data = "1", Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.OK);
                }
                else
                    return new ActionState { Message = "", Data = "0", Success = true, Type = ActionState.SuccessType }.ToActionResult(HttpStatusCode.OK);
           

        }
        #endregion

        #region ManageUsers      
        [AuthenticateUser]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUsers(int filter)
        {
            int totRows = 0;          
            var users = _userRepo.GetAllUsers(Utility.StartIndex(), Utility.PageSize(), Utility.SortBy(), Utility.SortDesc(), Utility.FilterText(), out totRows);           
            return Json(new { recordsFiltered = totRows, recordsTotal = totRows, data = users }, JsonRequestBehavior.AllowGet);
        }

        [AuthenticateUser]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return View(new UserModel());


            UserModel user = new UserModel();
            user = _userRepo.GetUserById(id.Value);
            if (user == null)
                return View(new UserModel());

            return View(user);

        }

        [AuthenticateUser]
        public ActionResult Profile(int? id)
        {
            if (id == null)
                return View(new UserModel());


            UserModel user = new UserModel();
            user = _userRepo.GetUserById(id.Value);
            if (user == null)
                return View(new UserModel());

            return View(user);

        }

        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            int loggedUser = Utility.GetCookie("LoggedUserId").ToIntOrZero();
            int newUserId = 0;
            if (ModelState.IsValid)
            {

                try
                {
                    UserModel user = new UserModel();

                    if (model.UserId > 0)
                        user = _userRepo.GetUserById(model.UserId);


                    if (_userRepo.UpsertUser(model, out newUserId) == 1)
                        return new ActionState { Message = "Failed!", Data = "Email already exists!", Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.BadRequest);
                    else
                    {                       
                        return new ActionState { Message = "Done!", Data = "User " + (model.UserId > 0 ? "updated" : "added") + " successfully.", Success = true, OptionalValue = newUserId.ToString(), Type = ActionState.SuccessType }.ToActionResult(HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    return new ActionState { Message = "Failed!", Data = ex.Message, Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.BadRequest);
                }

            }
            else
            {
                return new ActionState { Message = "Failed!", Data = ModelState.Values.SelectMany(v => v.Errors), Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _userRepo.DeleteUser(id);
                return new ActionState { Message = "Done!", Data = "User deleted successfully.", Success = true, Type = ActionState.SuccessType }.ToActionResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ActionState { Message = "Failed!", Data = ex.Message, Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.BadRequest);
            }

        }
        #endregion

        [HttpPost]
        public ActionResult UpdatePassword(int userId, string password)
        {
            try
            {
                _userRepo.UpdatePassword(userId, password);
                return new ActionState { Message = "Done!", Data = "Password changed successfully.", Success = true, Type = ActionState.SuccessType }.ToActionResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ActionState { Message = "Failed!", Data = ex.Message, Success = false, Type = ActionState.ErrorType }.ToActionResult(HttpStatusCode.BadRequest);
            }

        }      

        //public string RegistrationEmail(string toEmail, string username, string Password)
        //{
        //    string outMessage;
        //    string body = "Dear " + username + ",<br/>";
        //    body = body + "Your registration is successfully apporved and below is the login information.";
        //    body = body + "<br/><br/>";
        //    body = body + "Username -<b>" + toEmail + "</b><br/>";
        //    body = body + "Password -<b>" + Password + "</b><br/>";
        //    body = body + "<a href='" + ConfigurationManager.AppSettings["WebUrl"].ToString() + "' style='text-decoration:underline;'>Click here to login</a>";
        //    body = body + "<br/><br/>";
        //    body = body + "Regards<br/>";
        //    body = body + "EagleEye Team";
        //    Utility.SendEmail(toEmail, "EagleEye- Your registration is approved.", body, out outMessage);
        //    return outMessage;
        //}
    }
}



