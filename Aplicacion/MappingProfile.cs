using System.Linq;
using Aplicacion.Cursos;
using AutoMapper;
using Dominio;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDTO>()
             .ForMember(dest => dest.Instructores, 
                        src => src.MapFrom(z => z.InstructoresLink.Select(a => a.Instructor).ToList()));
            CreateMap<CursoInstructor, CursoInstructorDTO>();
            CreateMap<Instructor, InstructorDTO>();
        }
    }
}