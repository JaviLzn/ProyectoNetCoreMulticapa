using Aplicacion.ManejadorError;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDTO>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDTO>
        {
            private readonly CursosOnlineContext context;
            private readonly IMapper mapper;

            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<CursoDTO> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await context.Curso
                    .Include(x => x.Precios)
                    .Include(y => y.Comentarios)
                    .Include(x => x.InstructoresLink)
                        .ThenInclude(x => x.Instructor)
                    .FirstOrDefaultAsync(x => x.CursoId == request.Id);

                if (curso is null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = $"No se encontró el curso {request.Id}" });
                }

                CursoDTO cursoDTO = mapper.Map<CursoDTO>(curso);

                return cursoDTO;
            }
        }


    }
}