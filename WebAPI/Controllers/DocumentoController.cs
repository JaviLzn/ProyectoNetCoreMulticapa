using Aplicacion.Documentos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class DocumentoController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> GuardarArchivo(SubirArchivo.Ejecuta data)
        {
            return await Mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArchivoGenerico>> ObtenerDocumento(Guid id)
        {
            return await Mediator.Send(new ObtenerArchivo.Ejecuta { Id = id });
        }
    }
}