using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Registros
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public float IMC { get; set; }
    }
}
