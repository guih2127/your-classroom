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
    
    public partial class Table
    {
        public int Id { get; set; }
        public string ID_Aluno { get; set; }
        public Nullable<int> ID_Atividade { get; set; }
        public Nullable<double> Nota { get; set; }
        public byte[] Arquivo { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Atividade Atividade { get; set; }
    }
}
