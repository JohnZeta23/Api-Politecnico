using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api_Rest.Models
{
    public class Profesor2
    {
        public int ID_Profesor { get; set; }
        public string Nombre { get; set; }
        public string Materia { get; set; }
        public string TurnoLunes { get; set; }
        public string TurnoMartes { get; set; }
        public string TurnoMiercoles { get; set; }
        public string TurnoJueves { get; set; }
        public string TurnoViernes { get; set; }
        public string Fecha_Matriculacion { get; set; }
    }
}