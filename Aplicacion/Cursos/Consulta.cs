using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

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
                                             .Include(x => x.InstructoresLink)
                                             .ThenInclude(x => x.Instructor)
                                             .ToListAsync();

                var cursoDTO = mapper.Map<List<Curso>, List<CursoDTO>>(cursos);
                return cursoDTO;
            }
        }
    }
}