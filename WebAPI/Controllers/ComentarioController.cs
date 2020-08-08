using System;
using System.Threading.Tasks;
using Aplicacion.Comentarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ComentarioController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CrearComentario (Nuevo.CrearComentario data){
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarComentario (Guid id){
            return await Mediator.Send(new Eliminar.Ejecuta{Id = id});
        }
    }
}