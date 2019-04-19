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

        public Classes ObterClassePorId(int id)
        {
            Classes classe = context.Classes.Where(c => c.Id == id).First();
            return classe;
        }

        public string Inserir(Classes classe)
        {
            using (var context = new YourClassroomEntities())
            {
                try
                {
                    context.Classes.Add(classe);
                    context.SaveChanges();
                    return "Classe criada com sucesso. Comece a já a divulgá-la para seus alunos!";
                }
                catch (Exception e)
                {
                    return "Ocorreu um erro!" + e.Message;
                }
            }
        }

        public string Editar(int id, Classes classe)
        {
            using (var context = new YourClassroomEntities())
            {
                try
                {
                    Classes classeAtual = ObterClassePorId(id);
                    classeAtual.Materia = classe.Materia;
                    classeAtual.Periodo = classe.Periodo;
                    classeAtual.Curso_Id = classe.Curso_Id;
                    context.SaveChanges();
                    return "Classe editada com sucesso!";
                }
                catch (Exception e)
                {
                    return "Ocorreu um erro!" + e.Message;
                }
            }
        }
    }
}