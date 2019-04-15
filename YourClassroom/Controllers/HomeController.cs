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

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserRole = _roleService.GetUserRoleById(User.Identity.GetUserId());
            }
            return View();
        }
    }
}