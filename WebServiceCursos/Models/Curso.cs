using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServiceCursos.Models
{
    [Table("Cursos")]
    public class Curso
    {   
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do curso deve ser preenchido.")]
        [MaxLength(100, ErrorMessage ="O título do curso só pode conter até 100 caracteres.")]
        public string Titilo { get; set; }

        [Required(ErrorMessage ="A URL do curso deve ser preenchida.")]
        [Url(ErrorMessage ="A URL deve conter um endereço válido")]
        public string URL { get; set; }

        [Required(ErrorMessage ="O canal deve ser preenchido.")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Canal Canal { set; get; }

        [Required(ErrorMessage ="A data da publicação do curso deve ser preenchida.")]
        public DateTime DataPublicacao { get; set; }

        [Required(ErrorMessage = "A carga horária do curso deve ser preenchida.")]
        [Range(1, Int32.MaxValue, ErrorMessage ="A carga hoária deve ser pelo menos 1h.")]
        public int CargaHoraria { get; set; }

    }
}