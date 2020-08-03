namespace Dominio
{
    public class CursoInstructor
    {
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}