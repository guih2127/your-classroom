using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourClassroom.Services;
using YourClassroom.Models;

namespace YourClassroom.Controllers
{
    public class HomeController : Controller
    {
        private static RoleService _roleService = new RoleService();
        private static UserService _usuarioService = new UserService();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser usuario = _usuarioService.GetUserById(User.Identity.GetUserId());
                ViewBag.NomeCompleto = usuario.FirstName + " " + usuario.LastName;
                ViewBag.UserRole = _roleService.GetUserRoleById(User.Identity.GetUserId());
            }
            return View();
        }

        public HtmlString AlertaNotificacao(string mensagem)
        {
            if (mensagem.Contains("Sucesso"))
            {
                HtmlString notification = new HtmlString("<div class='alert alert-success' role='alert'>" + mensagem + "</div>");
                return notification;

            }
            else if (mensagem.Contains("Erro"))
            {
                HtmlString notification = new HtmlString("<div class='alert alert-danger' role='alert'>" + mensagem + "</div>");
                return notification;
            }
            else
            {
                HtmlString notification = new HtmlString("<div class='alert alert-primary' role='alert'>" + mensagem + "</div>");
                return notification;
            }
        }
    }
}