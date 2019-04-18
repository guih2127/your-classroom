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
        private static UserService _userService = new UserService();

        public ActionResult Index()
        {
            ClasseProfessorViewModel model = new ClasseProfessorViewModel();

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                var userRole = _roleService.GetUserRoleById(userId);
                ViewBag.UserRole = userRole;
                List<Classes> classes = userRole == "Professor" ? _classeService.ObterClassesPorIDProfessor(userId) : _classeService.ObterClassesPorIDAluno(userId);
                List<ApplicationUser> professores = new List<ApplicationUser>();

                foreach (var classe in classes)
                {
                    ApplicationUser professor = _userService.GetUserById(classe.ID_Professor);
                    professores.Add(professor);
                }

                model.Classes = classes;
                model.Professores = professores;
            }
            return View(model);
        }
    }
}