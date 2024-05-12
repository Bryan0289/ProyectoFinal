using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Recordatorio
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public PacienteModel Paciente { get; set; }
        public int IdMedicamento { get; set; }
        public MedicinaModel Medicina { get; set; }
        public DateTime FechaInicio { get; set; }
        public TimeSpan Frecuencia { get; set; }
        public int DuracionDias { get; set; }
        public bool Activo { get; set; } = true;

        public DateTime FechaFin => CalcularFechaFin();


        private DateTime CalcularFechaFin()
        {
            DateTime fechaFin = FechaInicio;
            fechaFin = fechaFin.AddDays(DuracionDias);
            for (int i = 1; i <= DuracionDias; i++)
            {
                fechaFin = fechaFin.Add(Frecuencia);
            }
            return fechaFin;
        }

    }
}
