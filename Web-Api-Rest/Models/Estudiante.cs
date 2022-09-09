using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiRest.Models
{
    public class Estudiante
    {
        public int ID_Estudiante { get; set; }
        public string Nombre { get; set; }
        public string Grado { get; set; }
        public string Aula { get; set; }
        public string Fecha_Inscripcion { get; set; }
    }
}