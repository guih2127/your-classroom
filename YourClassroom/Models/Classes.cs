//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YourClassroom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Classes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Classes()
        {
            this.SolicitacoesEntradaClasse = new HashSet<SolicitacoesEntradaClasse>();
            this.RLClassesAlunos = new HashSet<RLClassesAlunos>();
        }
    
        public string Materia { get; set; }
        public string ID_Professor { get; set; }
        public int Id { get; set; }
        public int Curso_Id { get; set; }
        public int Periodo { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitacoesEntradaClasse> SolicitacoesEntradaClasse { get; set; }
        public virtual Curso Curso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RLClassesAlunos> RLClassesAlunos { get; set; }
    }
}
