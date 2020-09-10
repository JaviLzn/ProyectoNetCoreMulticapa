using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDTO>>
        {

        }

        public class Manejador : IRequestHandler<ListaCursos, List<CursoDTO>>
        {
            private readonly CursosOnlineContext context;
            private readonly IMapper mapper;

            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            public async Task<List<CursoDTO>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso
                    .Include(x => x.Precios)
                    .Include(y => y.Comentarios)
                    .Include(x => x.InstructoresLink)
                        .ThenInclude(x => x.Instructor)
                    .ToListAsync();

                var cursoDTO = mapper.Map<List<CursoDTO>>(cursos);
                return cursoDTO;
            }
        }
    }
}