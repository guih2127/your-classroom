using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourClassroom.Models;
using YourClassroom.Services;
using Microsoft.AspNet.Identity;

namespace YourClassroom.Controllers
{
    public class ClassesController : Controller
    {
        private static ClasseService _classeService = new ClasseService();
        private static RoleService _roleService = new RoleService();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_roleService.GetUserRoleById(User.Identity.GetUserId()) == "Professor")
                {
                    ViewBag.Classes = _classeService.ObterClassesPorIDProfessor(User.Identity.GetUserId());
                }
            }
            return View();
        }
    }
}