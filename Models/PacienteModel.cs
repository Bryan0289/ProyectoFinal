using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        
        public DateTime FechaNac{ get; set; }
        public string Peso { get; set; }
        public string Altura { get; set; }
        public string Detalle { get; set; }

        public string Foto { get; set; }
        public List<TriajeModel> Triajes { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public int Edad
        {
            get
            {
                // Calcula la edad en base a la fecha de nacimiento
                var edad = DateTime.Today.Year - FechaNac.Year;
                
                return edad;
            }
        }
    }
}
