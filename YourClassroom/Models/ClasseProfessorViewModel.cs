using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YourClassroom.Models
{
    public class ClasseProfessorViewModel
    {
        public List<Classes> Classes { get; set; }
        public List<ApplicationUser> Professores { get; set; }
    }
}