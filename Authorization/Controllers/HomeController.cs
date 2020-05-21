using Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Authorization.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated) return Redirect("/Home/Login");
            return View();
        }

        [HttpGet]
        public ActionResult Login(string message)
        {
            switch (message)
            {
                case null:
                    break;
                case "OK":
                    ViewBag.Message = "Регистрация прошла успешно!";
                    break;
                case "WrongPassword":
                    ViewBag.Message = "Неверный пароль!";
                    break;
                case "UnknownLogin":
                    ViewBag.Message = "Такой логин не существует!";
                    break;
                default:
                    ViewBag.Message = "Не удалось авторизоваться!";
                    break;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            var result = Credentials.Auth(login, password);
            if (result == CredResult.OK)
            {
                FormsAuthentication.SetAuthCookie(login, false);
                return Redirect("/Home/Index");
            }
            else
                return Redirect($"/Home/Login?message={result:G}");
        }

        [HttpGet]
        public ActionResult Register(string message)
        {
            switch (message)
            {
                case null:
                    break;
                case "LoginAlreadyExists":
                    ViewBag.Message = "Такой логин уже существует!";
                    break;
                default:
                    ViewBag.Message = "Регистрация не удалась";
                    break;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(string login, string password)
        {
            var result = Credentials.Register(login, password);
            if (result == CredResult.OK)
                return Redirect($"/Home/Login?message={result:G}");
            else
                return Redirect($"/Home/Register?message={result:G}");
        }
    }
}