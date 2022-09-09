using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Api_Rest.Models;
using WebApiRest.Data;
using WebApiRest.Models;

namespace Web_Api_Rest.Controllers
{
    [RoutePrefix("api/profesor")]
    public class ProfesorController : ApiController
    {
        // GET api
        [Route("ListarProfesores")]
        public List<Profesor> Get()
        {
            return ProfesorData.Listar();
        }

        // GET api
        [Route("ObtenerProfesor")]
        public Profesor Get(string nombre_profesor)
        {
            return ProfesorData.Obtener(nombre_profesor);
        }

        // POST api
        [Route("RegistrarProfesor")]
        public string Post([FromBody] Profesor2 oProfesor)
        {
            return ProfesorData.Registrar(oProfesor);
        }

        // PUT api
        [Route("EditarProfesor")]
        public string Put([FromBody] Profesor2 oProfesor)
        {
            return ProfesorData.Modificar(oProfesor);
        }

        // DELETE api
        [Route("EliminarProfesor")]
        public string Delete(string nombre_profesor)
        {
            return ProfesorData.Eliminar(nombre_profesor);
        }
    }
}