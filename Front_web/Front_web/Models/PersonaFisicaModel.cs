using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_web.Models
{
    public class PersonaFisicaModel
    {
        public int IdPersonaFisica { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Nullable<DateTime> FechaActualizacion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int UsuarioAgrega { get; set; }
        public bool Activo { get; set; }
    }
}
