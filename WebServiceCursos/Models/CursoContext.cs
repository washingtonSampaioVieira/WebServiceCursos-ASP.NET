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
        }

        public DbSet<Curso> Cursos { get; set; }

    }
}