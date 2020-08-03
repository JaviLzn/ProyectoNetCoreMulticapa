namespace Dominio
{
    public class Precio
    {
        public int PrecioId { get; set; }
        public decimal PrecioACtual { get; set; }
        public decimal Promocion { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}