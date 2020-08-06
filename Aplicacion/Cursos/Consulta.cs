using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<Curso>>
        {

        }

        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            private readonly CursosOnlineContext context;

            public Manejador(CursosOnlineContext context)
            {
                this.context = context;
            }
            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                var cursos = await context.Curso.ToListAsync();
                return cursos;
            }
        }
    }
}