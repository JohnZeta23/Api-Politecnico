using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class Profesor
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
        public string Fecha_Salida { get; set; }
        public string Estatus { get; set; }
    }
}