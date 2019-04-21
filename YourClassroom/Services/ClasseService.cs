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
                    return "Sucesso! Classe criada com sucesso. Comece a já a divulgá-la para seus alunos.";
                }
                catch (Exception e)
                {
                    return "Erro! Ocorreu um erro." + e.Message;
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
                    return "Sucesso! Classe editada com sucesso.";
                }
                catch (Exception e)
                {
                    return "Erro! Ocorreu um erro." + e.Message;
                }
            }
        }

        public List<Classes> Pesquisar(FiltroClassesViewModel filtro)
        {
            List<Classes> classes = context.Classes.ToList();

            if (filtro.NomeProfessor != null)
            {
                List<AspNetUsers> professores = context.AspNetUsers.Where(p => (p.FirstName + " " + p.LastName).Contains(filtro.NomeProfessor)).ToList();
                classes = (from c in classes
                           join p in professores on c.ID_Professor equals p.Id
                           select c).ToList();
            }
            if (filtro.NomeMateria != null)
            {
                classes = classes.Where(c => c.Materia == filtro.NomeMateria).ToList();
            }
            if (filtro.ClasseId != null)
            {
                classes = classes.Where(c => c.Id == filtro.ClasseId).ToList();
            }

            return classes;
        }

        public string SolicitarEntrada(int classeId, string alunoId)
        {
            using (var context = new YourClassroomEntities())
            {
                try
                {
                    var solicitacaoExistente = context.SolicitacoesEntradaClasse.Where(s => s.Id_Aluno == alunoId && s.Id_Classe == classeId).FirstOrDefault();

                    if (solicitacaoExistente != null)
                    {
                        return "Erro! Você já solicitou sua entrada nessa classe! Aguarde a resposta do professor.";
                    }

                    SolicitacoesEntradaClasse solicitacao = new SolicitacoesEntradaClasse { Id_Aluno = alunoId, Id_Classe = classeId };
                    context.SolicitacoesEntradaClasse.Add(solicitacao);
                    context.SaveChanges();

                    return "Sucesso! Solicitação enviada com sucesso. Aguarde a aprovação do professor.";
                }
                catch (Exception e)
                {
                    return "Erro! Ocorreu um erro. " + e.Message;
                }
            }
        }
    }
}