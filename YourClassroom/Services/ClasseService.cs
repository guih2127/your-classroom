using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourClassroom.Models;
using System.Data.Entity;

namespace YourClassroom.Services
{
    public class ClasseService
    {
        YourClassroomEntities context;

        public ClasseService()
        {
            context = new YourClassroomEntities();
        }

        public List<Classes> ObterClassesPorIDProfessor(string idProfessor)
        {
            List<Classes> classes = context.Classes.Where(c => c.ID_Professor == idProfessor).ToList();
            return classes;
        }

        public List<Classes> ObterClassesPorIDAluno(string idAluno)
        {
            List<Classes> classes =
                (from classe in context.Classes
                join rl in context.RLClassesAlunos on classe.Id equals rl.ID_Classe
                where rl.ID_Aluno == idAluno
                select classe).ToList();

            return classes;
        }

        public string Inserir(Classes classe)
        {
            try
            {
                context.Classes.Add(classe);
                context.SaveChanges();
                return "Classe inserida com sucesso!";
            }
            catch (Exception e)
            {
                return "Ocorreu um erro!" + e.Message;
            }
        }
    }
}