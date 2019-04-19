using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YourClassroom.Models;
using System.Data.Entity;

namespace YourClassroom.Services
{
    public class CursoService
    {
        YourClassroomEntities context;

        public CursoService()
        {
            context = new YourClassroomEntities();
        }

        public List<Curso> ObterTodosIdsCurso()
        {
            return context.Curso.ToList();
        }

        public Curso ObterCursoPorId(int id)
        {
            return context.Curso.Where(c => c.Id == id).First();
        }
    }
}