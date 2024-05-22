using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class MedicinaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Dosificacion { get; set; }
        public string Presentacion { get; set; }
        public string Indicaciones {  get; set; }
        public string Foto{ get;set; }

    }
}
