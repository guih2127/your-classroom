using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourClassroom.Models;

namespace YourClassroom.Services
{
    public class SolicitacoesService
    {
        YourClassroomEntities context;
        ClasseService _classeService = new ClasseService();

        public SolicitacoesService()
        {
            context = new YourClassroomEntities();
        }

        public SolicitacoesEntradaClasse ObterPorId(int id)
        {
            SolicitacoesEntradaClasse solicitacao = context.SolicitacoesEntradaClasse.Where(s => s.Id == id).First();
            return solicitacao;
        }

        public void ApagarSolicitacao(int id)
        {
            SolicitacoesEntradaClasse solicitacao = context.SolicitacoesEntradaClasse.Where(s => s.Id == id).First();
            context.SolicitacoesEntradaClasse.Remove(solicitacao);
            context.SaveChanges();
        }

        public string AceitarSolicitacaoAlunoClasse(SolicitacoesEntradaClasse solicitacao)
        {
            using (var context = new YourClassroomEntities())
            {
                try
                {
                    _classeService.InserirRLClasseAluno(solicitacao.Id_Aluno, solicitacao.Id_Classe);
                    ApagarSolicitacao(solicitacao.Id);

                    return "Sucesso! Aluno aceito com sucesso.";
                }
                catch (Exception e)
                {
                    return "Erro! Ocorreu um problema. " + e.Message;
                }
            }
        }

        public string RecusarSolicitacaoAlunoClasse(SolicitacoesEntradaClasse solicitacao)
        {
            using (var context = new YourClassroomEntities())
            {
                try
                {
                    ApagarSolicitacao(solicitacao.Id);
                    return "Sucesso! Solicitação recusada com sucesso.";
                }
                catch (Exception e)
                {
                    return "Erro! Ocorreu um problema. " + e.Message;
                }
            }
        }
    }
}