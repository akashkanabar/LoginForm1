using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginForm.Controllers
{
    public class LoginController : Controller
    {

        LoginFormEntities db = new LoginFormEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User U)
        {
            var user = db.Users.Where(model => model.UserName == U.UserName && model.PassWord == U.PassWord).FirstOrDefault();
            if(user != null)
            {
                //Session["UserId"]= U.Id.ToString();
                //Session["UserName"]= U.UserName.ToString();
                TempData["LoginSuccessMessage"] = "<script>alert('LogIn Successfully')</script>";
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('UserName Or Password Incorrect')</script>";
                return View();
            }
            //return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(User U)
        {
            var user = db.Users.Where(model => model.UserName == U.UserName && model.Email == U.Email).FirstOrDefault();
            if (user != null)
            {
                return RedirectToAction("ShowData");
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Invalid UserName Or Email')</script>";
                return View();
            }
            //return View(User);
        }

        public ActionResult ShowData()
        {
            var Details = db.Users.ToList();
            return View(Details);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User U)
        {
            if(ModelState.IsValid)
            {
                db.Users.Add(U);
                int a = db.SaveChanges();
                if(a > 0)
                {
                    ViewBag.InsertMessage = "<script>alert('Registered Successfully')</script>";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert('Registration Failed')</script>";
                }
            }

            return RedirectToAction("Index", "Login");
        }
    }
}