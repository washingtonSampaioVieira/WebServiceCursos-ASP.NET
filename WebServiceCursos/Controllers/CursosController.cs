using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServiceCursos.Models;

namespace WebServiceCursos.Controllers
{
    public class CursosController : ApiController
    {
        private CursoContext db = new CursoContext();

        // Por padrão quando o parametro do metodo é complexo o Net vai pegar ele no corpo da req
        // Se o parametro for simples como uma string vai ser esperado na URL
        // Nome do metodo leva o verbo da requisição (GET, POST...)
        public IHttpActionResult PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Cursos.Add(curso);
            db.SaveChanges();

            // Retono = ROTA GET do curso e o Curso no body da
            return CreatedAtRoute("DefaultApi", new { id = curso.Id }, curso);
        }

    }
}