using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebServiceCursos.Models
{
    public class CursoContext : DbContext
    {
        public CursoContext() : base("CursoLocal")
        {
            Database.Log = d => System.Diagnostics.Debug.WriteLine(d);
        }

        public DbSet<Curso> Cursos { get; set; }

    }
}