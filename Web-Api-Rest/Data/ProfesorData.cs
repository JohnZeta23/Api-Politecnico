using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiRest.Models;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Web_Api_Rest.Models;

namespace WebApiRest.Data
{
    public class ProfesorData
    {
        public static string Registrar(Profesor2 oProfesor)
        {
            int turnoDisponible = ValidarTurnos(oProfesor);
            int Validacion_Turno = 0;
            string mensaje = null;
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                if (turnoDisponible > 0)
                {
                    Validacion_Turno = 1;
                }
                else
                {
                    //Registro de datos
                    DateTime dateTime = Convert.ToDateTime(oProfesor.Fecha_Matriculacion);
                    oProfesor.Fecha_Matriculacion = dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    SqlCommand cmd = new SqlCommand("SpProfesoresInsertar", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                    cmd.Parameters.AddWithValue("@Materia", oProfesor.Materia);
                    cmd.Parameters.AddWithValue("@TurnoLunes", oProfesor.TurnoLunes);
                    cmd.Parameters.AddWithValue("@TurnoMartes", oProfesor.TurnoMartes);
                    cmd.Parameters.AddWithValue("@TurnoMiercoles", oProfesor.TurnoMiercoles);
                    cmd.Parameters.AddWithValue("@TurnoJueves", oProfesor.TurnoJueves);
                    cmd.Parameters.AddWithValue("@TurnoViernes", oProfesor.TurnoViernes);
                    cmd.Parameters.AddWithValue("@Fecha_Matriculacion", oProfesor.Fecha_Matriculacion);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Validacion_Turno = 2;
                    }
                    catch (Exception ex)
                    {
                        Validacion_Turno = 0;
                    }
                    finally { connection.Close(); }
                }

                switch (Validacion_Turno)
                {
                    case 0:
                        mensaje = "Ocurrió un problema al registrar el profesor, compruebe los datos e intente de nuevo";
                        break;

                    case 1:
                        mensaje = "Parece que uno de los turnos esta ocupado, compruebe que los turnos esten libres e intente de nuevo";
                        break;

                    case 2:
                        mensaje = "El profesor fue registrado exitosamente";
                        break;
                }
                return mensaje;
            }

        }

        public static string Modificar(Profesor2 oProfesor)
        {
            int turnoDisponible = ValidarTurnos(oProfesor);
            int Validacion_Turno = 0;
            string mensaje = null;
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            { 
                if (turnoDisponible > 0)
                {
                    Validacion_Turno = 1;
                }
                else
                {
                    DateTime dateTime = Convert.ToDateTime(oProfesor.Fecha_Matriculacion);
                    oProfesor.Fecha_Matriculacion = dateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                    SqlCommand cmd = new SqlCommand("SpProfesoresActualizar", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Profesor", oProfesor.ID_Profesor);
                    cmd.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                    cmd.Parameters.AddWithValue("@Materia", oProfesor.Materia);
                    cmd.Parameters.AddWithValue("@TurnoLunes", oProfesor.TurnoLunes);
                    cmd.Parameters.AddWithValue("@TurnoMartes", oProfesor.TurnoMartes);
                    cmd.Parameters.AddWithValue("@TurnoMiercoles", oProfesor.TurnoMiercoles);
                    cmd.Parameters.AddWithValue("@TurnoJueves", oProfesor.TurnoJueves);
                    cmd.Parameters.AddWithValue("@TurnoViernes", oProfesor.TurnoViernes);
                    cmd.Parameters.AddWithValue("@Fecha_Matriculacion", oProfesor.Fecha_Matriculacion);
                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Validacion_Turno = 2;
                    }
                    catch (Exception ex)
                    {
                        Validacion_Turno = 0;
                    }
                    finally { connection.Close(); }
                }
                switch (Validacion_Turno)
                {
                    case 0:
                        mensaje = "Ocurrió un problema al editar los datos del profesor, compruebe la información e intente de nuevo";
                        break;

                    case 1:
                        mensaje = "Parece que uno de los turnos esta ocupado, compruebe que los turnos esten libres e intente de nuevo";
                        break;

                    case 2:
                        mensaje = "Los datos del profesor han sido editados exitosamente";
                        break;
                }
                return mensaje;
            }
        }

        public static List<Profesor> Listar()
        {
            List<Profesor> oListaProfesor = new List<Profesor>();
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd = new SqlCommand("SpProfesoresListar", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        string Fecha = null;
                        while (dr.Read())
                        {
                            if (dr["Fecha_Salida"].ToString().Trim() == "") { 
                                Fecha = "AUN ACTIVO"; 
                            } 
                            else { 
                                Fecha = dr["Fecha_Salida"].ToString(); 
                            }
                            oListaProfesor.Add(new Profesor()
                            {
                                ID_Profesor = Convert.ToInt32(dr["ID_Profesor"]),
                                Nombre = dr["Nombre"].ToString(),
                                Materia = dr["Materia"].ToString(),
                                TurnoLunes = dr["TurnoLunes"].ToString(),
                                TurnoMartes = dr["TurnoMartes"].ToString(),
                                TurnoMiercoles = dr["TurnoMiercoles"].ToString(),
                                TurnoJueves = dr["TurnoJueves"].ToString(),
                                TurnoViernes = dr["TurnoViernes"].ToString(),
                                Fecha_Matriculacion = dr["Fecha_Matriculacion"].ToString(),
                                Fecha_Salida = Fecha,
                                Estatus = dr["Estatus"].ToString()
                            });
                        }
                    }
                    return oListaProfesor;
                }
                catch (Exception ex)
                {
                    return oListaProfesor;
                }
                finally { connection.Close(); }

            }
        }

        public static Profesor Obtener(string Nombre_Profesor)
        {
            Profesor oProfesor = new Profesor();
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd = new SqlCommand("SpProfesoresObtener", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", Nombre_Profesor);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        string Fecha = null;
                        while (dr.Read())
                        {
                            if (dr["Fecha_Salida"].ToString().Trim() == ""){
                                Fecha = "AUN ACTIVO";
                            }
                            else{
                                Fecha = dr["Fecha_Salida"].ToString();
                            }
                            oProfesor = new Profesor()
                            {
                                ID_Profesor = Convert.ToInt32(dr["ID_Profesor"]),
                                Nombre = dr["Nombre"].ToString(),
                                Materia = dr["Materia"].ToString(),
                                TurnoLunes = dr["TurnoLunes"].ToString(),
                                TurnoMartes = dr["TurnoMartes"].ToString(),
                                TurnoMiercoles = dr["TurnoMiercoles"].ToString(),
                                TurnoJueves = dr["TurnoJueves"].ToString(),
                                TurnoViernes = dr["TurnoViernes"].ToString(),
                                Fecha_Matriculacion = dr["Fecha_Matriculacion"].ToString(),
                                Fecha_Salida = Fecha,
                                Estatus = dr["Estatus"].ToString()
                            };
                        }
                    }

                    return oProfesor;
                }
                catch (Exception ex)
                {
                    return oProfesor;
                }
                finally { connection.Close(); }
            }
        }

        public static string Eliminar(string Nombre_Profesor)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd = new SqlCommand("SpProfesoresEliminar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", Nombre_Profesor);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return "El profesor ha sido inactivado exitosamente";
                }
                catch (Exception ex)
                {
                    return "Ocurrió un problema al tratar de inactivar al profesor, compruebe el nombre e intente de nuevo";
                }
                finally { connection.Close(); }
            }
        }

        public static int ValidarTurnos(Profesor2 oProfesor) 
        {
            int turnoDisponible = 0;
            using (SqlConnection connection = new SqlConnection(Conexion.ConexionString))
            {
                SqlCommand cmd2 = new SqlCommand("SpTurnoLunesObtener", connection);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                cmd2.Parameters.AddWithValue("@TurnoLunes", oProfesor.TurnoLunes);
                SqlCommand cmd3 = new SqlCommand("SpTurnoMartesObtener", connection);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                cmd3.Parameters.AddWithValue("@TurnoMartes", oProfesor.TurnoMartes);
                SqlCommand cmd4 = new SqlCommand("SpTurnoMiercolesObtener", connection);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                cmd4.Parameters.AddWithValue("@TurnoMiercoles", oProfesor.TurnoMiercoles);
                SqlCommand cmd5 = new SqlCommand("SpTurnoJuevesObtener", connection);
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                cmd5.Parameters.AddWithValue("@TurnoJueves", oProfesor.TurnoJueves);
                SqlCommand cmd6 = new SqlCommand("SpTurnoViernesObtener", connection);
                cmd6.CommandType = CommandType.StoredProcedure;
                cmd6.Parameters.AddWithValue("@Nombre", oProfesor.Nombre);
                cmd6.Parameters.AddWithValue("@TurnoViernes", oProfesor.TurnoViernes);
                try
                {
                    connection.Open();
                    cmd2.ExecuteNonQuery();

                    using (SqlDataReader dr = cmd2.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            turnoDisponible++;
                        }
                    }

                    using (SqlDataReader dr = cmd3.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            turnoDisponible++;
                        }
                    }

                    using (SqlDataReader dr = cmd4.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            turnoDisponible++;
                        }
                    }

                    using (SqlDataReader dr = cmd5.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            turnoDisponible++;
                        }
                    }

                    using (SqlDataReader dr = cmd6.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            turnoDisponible++;
                        }
                    }

                    return turnoDisponible;
                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally { connection.Close(); }
            }
        }
    }
}