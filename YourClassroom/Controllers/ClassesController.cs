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
        private static CursoService _cursoService = new CursoService();

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

                ViewBag.Mensagem = TempData["Mensagem"];
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Cursos = _cursoService.ObterTodosIdsCurso();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Classes classe)
        {
            classe.ID_Professor = User.Identity.GetUserId();
            TempData["Mensagem"] = _classeService.Inserir(classe);

            return RedirectToAction("Index");
        }
    }
}