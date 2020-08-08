using System.Collections.Generic;
using System;
namespace Dominio
{
    public class Curso
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Precio Precios { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<CursoInstructor> InstructoresLink { get; set; }
    }
}