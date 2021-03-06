﻿using System;
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
        private static HomeController _home = new HomeController();
        private static SolicitacoesService _solicitacoesService = new SolicitacoesService();

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
                    classe.Curso = _cursoService.ObterCursoPorId(classe.Curso_Id);
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

        [Authorize]
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            var userRole = _roleService.GetUserRoleById(userId);
            if (_roleService.GetUserRoleById(userId) == "Professor")
            {
                ViewBag.Cursos = _cursoService.ObterTodosIdsCurso();
                return View();
            }
            else
            {
                TempData["Mensagem"] = _home.AlertaNotificacao("Infelizmente um usuário com perfil de Aluno não pode criar classes.");
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Classes classe)
        {
            classe.ID_Professor = User.Identity.GetUserId();
            TempData["Mensagem"] = _home.AlertaNotificacao(_classeService.Inserir(classe));

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            string userId = User.Identity.GetUserId();
            var userRole = _roleService.GetUserRoleById(userId);
            Classes classe = _classeService.ObterClassePorId(id);

            if (classe.ID_Professor == userId)
            {
                ViewBag.Cursos = _cursoService.ObterTodosIdsCurso();
                return View(classe);
            }
            else
            {
                TempData["Mensagem"] = _home.AlertaNotificacao("Você não tem permissão para acessar essa página!");
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Classes classe)
        {
            TempData["Mensagem"] = _home.AlertaNotificacao(_classeService.Editar(id, classe));
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(FiltroClassesViewModel filtro)
        {
            string userId = User.Identity.GetUserId();
            var userRole = _roleService.GetUserRoleById(userId);

            List<Classes> classes = _classeService.Pesquisar(filtro);
            List<ApplicationUser> professores = new List<ApplicationUser>();
            foreach(var classe in classes)
            {
                var professor = _userService.GetUserById(classe.ID_Professor);
                classe.AspNetUsers.FirstName = professor.FirstName;
                classe.AspNetUsers.LastName = professor.LastName;
                professores.Add(professor);
            }
            return View(classes);
        }

        public ActionResult SolicitarEntrada(int classeId)
        {
            string userId = User.Identity.GetUserId();
            TempData["Mensagem"] = _home.AlertaNotificacao(_classeService.SolicitarEntrada(classeId, userId));

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Solicitacoes()
        {
            string userId = User.Identity.GetUserId();
            List<SolicitacoesEntradaClasse> solicitacoes = _classeService.TodasSolicitacoesPorProfessor(userId);
            ViewBag.Mensagem = TempData["Mensagem"];

            return View(solicitacoes);
        }

        [Authorize(Roles="Professor")]
        public ActionResult Aceitar(int idSolicitacao)
        {
            SolicitacoesEntradaClasse solicitacao = _solicitacoesService.ObterPorId(idSolicitacao);
            TempData["Mensagem"] = _home.AlertaNotificacao(_solicitacoesService.AceitarSolicitacaoAlunoClasse(solicitacao));
            return RedirectToAction("Solicitacoes");
        }

        [Authorize(Roles="Professor")]
        public ActionResult Recusar(int idSolicitacao)
        {
            SolicitacoesEntradaClasse solicitacao = _solicitacoesService.ObterPorId(idSolicitacao);
            TempData["Mensagem"] = _home.AlertaNotificacao(_solicitacoesService.RecusarSolicitacaoAlunoClasse(solicitacao));
            return RedirectToAction("Solicitacoes");
        }

        [Authorize]
        public ActionResult Detalhes(int id)
        {
            Classes classe = _classeService.ObterClassePorId(id);
            return View(classe);
        }
    }
}