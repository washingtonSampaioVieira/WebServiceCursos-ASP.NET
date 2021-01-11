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

        public IHttpActionResult GetCurso(int id)
        {
            if (id < 0)
                return BadRequest("O Id deve ser maior que 0");

            var curso = db.Cursos.Find(id);

            if (curso == null)
                return NotFound();

            return Ok(curso);
        }

        public IHttpActionResult PutCurso(int id, Curso curso)
        {
            // Para atualizar um registro tambem é nescessario validar o obj
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != curso.Id)
                return BadRequest("O id informado deve ser igual ao id informado no corpo da requisição");

            // Verificando se existe um curso com o ID informado
            if (db.Cursos.Count(c => c.Id == id) == 0)
                return BadRequest("O Id informado é divergente do informado no corpo da requisição.");

            // Gerando instrução de atualização no banco
            db.Entry(curso).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult DeleteCurso(int id)
        {
            if (id < 0)
                return BadRequest("O Id deve ser maior que 0.");

            var curso = db.Cursos.Find(id);
            if (curso == null)
                return NotFound();

            db.Cursos.Remove(curso);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult GetCursos(int pagina = 1, int tamanhoPagina = 10)
        {
            if (pagina <= 0 || tamanhoPagina <= 0)
                return BadRequest("Os parametros pagina e tamanhoPagina devem ser maiores que zero.");

            if (tamanhoPagina > 10)
                return BadRequest("O tamanho maximo de pagina permitido e 10.");

            int totalPaginas = (int)Math.Ceiling(db.Cursos.Count() / Convert.ToDecimal(tamanhoPagina));

            if (pagina > totalPaginas)
                return BadRequest("A pagina solicitada nao existe.");

            System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-TotalPages", totalPaginas.ToString());

            if (pagina > 1)
                System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-PreviousPage",
                    Url.Link("DefaultApi", new { pagina = pagina - 1, tamanhoPagina = tamanhoPagina }));

            if (pagina < totalPaginas)
                System.Web.HttpContext.Current.Response.AddHeader("X-Pagination-NextPage",
                    Url.Link("DefaultApi", new { pagina = pagina + 1, tamanhoPagina = tamanhoPagina }));

            var cursos = db.Cursos.OrderBy(c => c.DataPublicacao).Skip(tamanhoPagina * (pagina - 1)).Take(tamanhoPagina);

            return Ok(cursos);
        
        }


    }
}