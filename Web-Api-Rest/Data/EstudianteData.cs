using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiRest.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace WebApiRest.Data
{
    public class EstudianteData
    {
        public static string Registrar(Estudiante oestudiante)
        {
            int Estudiantes_Aula = ValidarAula(oestudiante);
            int Validacion_Aula = 0;
            string mensaje = null;
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
              if (Estudiantes_Aula >= 30) {
                    Validacion_Aula = 1;
              }
              else{
                    //Registro de datos
                    DateTime dateTime = Convert.ToDateTime(oestudiante.Fecha_Inscripcion);
                    oestudiante.Fecha_Inscripcion = dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    SqlCommand cmd = new SqlCommand("SpEstudiantesInsertar", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", oestudiante.Nombre);
                    cmd.Parameters.AddWithValue("@Grado", oestudiante.Grado);
                    cmd.Parameters.AddWithValue("@Aula", oestudiante.Aula);
                    cmd.Parameters.AddWithValue("@Fecha_Inscripcion", oestudiante.Fecha_Inscripcion);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Validacion_Aula = 2;   
                    }
                    catch (Exception ex)
                    {
                        Validacion_Aula = 0;
                    }
                    finally { connection.Close(); } 
              }
                switch (Validacion_Aula) 
                { case 0: mensaje = "El estudiante no pudo ser registrado, compruebe los datos e intente nuevamente";  
                  break;

                  case 1: mensaje = "El aula en la que intenta matricular a este estudiante esta en el limite de 30 estudiantes";
                  break;

                  case 2: mensaje = "El estudiante fue registrado exitosamente";
                  break;
                }
                return mensaje;
            }
        } 

        public static string Modificar(Estudiante oestudiante)
        {
            int Estudiantes_Aula = ValidarAula(oestudiante);
            int Validacion_Aula = 0;
            string mensaje = null;
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                if (Estudiantes_Aula >= 30)
                {
                    Validacion_Aula = 1;
                }
                else
                {
                    DateTime dateTime = Convert.ToDateTime(oestudiante.Fecha_Inscripcion);
                    oestudiante.Fecha_Inscripcion = dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    SqlCommand cmd = new SqlCommand("SpEstudiantesActualizar", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Estudiante", oestudiante.ID_Estudiante);
                    cmd.Parameters.AddWithValue("@Nombre", oestudiante.Nombre);
                    cmd.Parameters.AddWithValue("@Grado", oestudiante.Grado);
                    cmd.Parameters.AddWithValue("@Aula", oestudiante.Aula);
                    cmd.Parameters.AddWithValue("@Fecha_Inscripcion", oestudiante.Fecha_Inscripcion);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Validacion_Aula = 2;
                    }
                    catch (Exception ex)
                    {
                        Validacion_Aula = 0;
                    }
                    finally { connection.Close(); }
                }
                switch (Validacion_Aula)
                {
                    case 0:
                        mensaje = "Hubo un problema al modificar los datos del estudiante, compruebe los datos e intente nuevamente";
                        break;

                    case 1:
                        mensaje = "El aula en la que intenta matricular a este estudiante esta en el limite de 30 estudiantes";
                        break;

                    case 2:
                        mensaje = "Los datos del estudiante han sido modificados exitosamente";
                        break;
                }
                return mensaje;
            }
        }

        public static List<Estudiante> Listar()
        {
            List<Estudiante> oListaEstudiante = new List<Estudiante>();
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd = new SqlCommand("SpEstudiantesListar", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaEstudiante.Add(new Estudiante()
                            {
                                ID_Estudiante = Convert.ToInt32(dr["ID_Estudiante"]),
                                Nombre = dr["Nombre"].ToString(),
                                Grado = dr["Grado"].ToString(),
                                Aula = dr["Aula"].ToString(),
                                Fecha_Inscripcion = dr["Fecha_Inscripcion"].ToString()
                            });
                        }
                    }
                    return oListaEstudiante;
                }
                catch (Exception ex)
                {
                    return oListaEstudiante;
                }
                finally { connection.Close(); }
            }
        }
        public static Estudiante Obtener(string nombre_estudiante)
        {
            Estudiante oestudiante = new Estudiante();
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd = new SqlCommand("SpEstudiantesObtener", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre_estudiante);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oestudiante = new Estudiante()
                            {
                                ID_Estudiante = Convert.ToInt32(dr["ID_Estudiante"]),
                                Nombre = dr["Nombre"].ToString(),
                                Grado = dr["Grado"].ToString(),
                                Aula = dr["Aula"].ToString(),
                                Fecha_Inscripcion = dr["Fecha_Inscripcion"].ToString()
                            };
                        }
                    }

                    return oestudiante;
                }
                catch (Exception ex)
                {
                    return oestudiante;
                }
                finally { connection.Close(); }
            }
        }

        public static string Eliminar(string Nombre_Estudiante)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd = new SqlCommand("SpEstudiantesEliminar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", Nombre_Estudiante);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "El estudiante ha sido eliminado exitosamente";
                }
                catch (Exception ex)
                {
                    return "Hubo un error, compruebe que el ID del estudiante este en la base de datos e intente de nuevo";
                }
                finally { connection.Close(); }
            }
        }

        public static int ValidarAula(Estudiante oestudiante)
        {
            int Estudiantes_Aula = 0;
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd2 = new SqlCommand("SpAulaObtener", connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Aula", oestudiante.Aula);
                try
                {
                    connection.Open();
                    cmd2.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd2.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Estudiantes_Aula++;
                        }
                    }

                    return Estudiantes_Aula;
                }
                catch (Exception ex)
                {
                    return 30;
                }
                finally { connection.Close(); }
            }
        }
    }
}