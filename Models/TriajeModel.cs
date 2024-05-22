using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class TriajeModel
    {
        public int Id { get; set; }
        public string NivelInsu { get; set; }
        public int PresionArt { get; set; }
        public DateTime Fecha { get; set; }
        public int PacienteId { get; set; }
        public PacienteModel Paciente { get; set; }

        public string FechaHum{
            get{
                return Fecha.Humanize();
            }
        }
    }
}
