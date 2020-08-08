using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Paginacion;

namespace Aplicacion.Cursos
{
    public class PaginacionCurso
    {
        public class Ejecuta : IRequest<PaginacionModel>
        {
            public string Titulo { get; set; }
            public int NumeroPagina { get; set; }
            public int ElementosPagina { get; set; }
        }

        public class ValidarEjecuta : AbstractValidator<Ejecuta>
        {
            public ValidarEjecuta()
            {
                RuleFor(x => x.NumeroPagina).NotEmpty().GreaterThan(0);
                RuleFor(x => x.ElementosPagina).NotEmpty().GreaterThan(0).LessThanOrEqualTo(100);
            }
        }

        public class ManejarEjecuta : IRequestHandler<Ejecuta, PaginacionModel>
        {
            private readonly IPaginacion paginacion;

            public ManejarEjecuta(IPaginacion paginacion)
            {
                this.paginacion = paginacion;
            }

            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var storedProcedure = "usp_Curso_Paginacion";
                var ordenamiento = "Titulo";
                var parametrosFiltro = new Dictionary<string, object>();
                parametrosFiltro.Add("NombreCurso", request.Titulo);
                return await paginacion.devolverPaginacion(storedProcedure, request.NumeroPagina, request.ElementosPagina, parametrosFiltro, ordenamiento);
            }
        }
    }
}