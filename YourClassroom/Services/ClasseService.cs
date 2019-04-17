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
    }
}