using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRest.Data;
using WebApiRest.Models;

namespace Web_Api_Rest.Controllers
{
    [RoutePrefix("api/estudiante")]
    public class EstudianteController : ApiController
    {
        // GET api
        [Route("ListarEstudiante")]
        public List<Estudiante> Get()
        {
            return EstudianteData.Listar();
        }

        // GET api
        [Route("ObtenerEstudiante")]
        public Estudiante Get(string nombre_estudiante)
        {
            return EstudianteData.Obtener(nombre_estudiante);
        }

        // POST api
        [Route("RegistrarEstudiante")]
        public string Post([FromBody] Estudiante oEstudiante)
        {
            return EstudianteData.Registrar(oEstudiante);
        }

        // PUT api
        [Route("EditarEstudiante")]
        public string Put([FromBody] Estudiante oEstudiante)
        {
            return EstudianteData.Modificar(oEstudiante);
        }

        // DELETE api
        [Route("EliminarEstudiante")]
        public string Delete(string nombre_estudiante)
        {
            return EstudianteData.Eliminar(nombre_estudiante);
        }
    }
}