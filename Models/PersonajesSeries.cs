using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPracticaPersonajes.Models
{
    public class PersonajesSeries
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Serie { get; set; }
    }
}
