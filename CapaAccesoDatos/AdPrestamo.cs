using System;
using System.Collections.Generic;
using System.Text;
using CapaEntidades;
using System.Data.SqlClient;
using System.Data;

namespace CapaAccesoDatos
{
    public class AdPrestamo
    {
        #region Atributos
        private string cadConexion;
        #endregion

        #region Constructores

        public AdPrestamo()
        {
            cadConexion = string.Empty;
        }

        public AdPrestamo(string cad)
        {
            cadConexion = cad;
        }
        #endregion

        public int insertar(EPrestamo pre)
        {
            int result = -1;
            string sentencia = "insert into Prestamo(clavePrestamo,claveEjemplar," +
            "claveUsuario,fechaPrestamo,fechaDevolucion)" +
            " values (@clavePrestamo,@clavEjemplar,@claveUsuario,@fechaPre,@fechaDev)" //Con arrobas, al no interpolar
            ;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@clavePrestamo", pre.ClavePrestamo);
            comando.Parameters.AddWithValue("@clavEjemplar", pre.Ejemplar.ClaveEjemplar);
            comando.Parameters.AddWithValue("@claveUsuario", pre.Usuario.ClaveUsuario);
            comando.Parameters.AddWithValue("@fechaPre", pre.FechaPrestamo);
            comando.Parameters.AddWithValue("@fechaDev", pre.FechaDevolucion);


            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();//ERROR AQUI
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw new Exception("Error!");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;
        }

        public bool clavePrestamoExiste(string clave)
        {
            bool result = false; object ObEscalar;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();

            comando.CommandText = "select 1 from Prestamo Where clavePrestamo=@clavePres";
            comando.Parameters.AddWithValue("@clavePres", clave);
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                ObEscalar = comando.ExecuteScalar();
                if (ObEscalar != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception)
            {
                throw new Exception("Error de conexión");
            }
            finally
            {
                comando.Dispose();
                conexion.Dispose();
            }
            return result;

        }

        public DataSet listarTodos(string condicion)
        {
            DataSet setPrestamos = new DataSet();
            string sentencia = "Select clavePrestamo,claveEjemplar,claveUsuario,fechaPrestamo,fechaDevolucion from Prestamo";

            if (!string.IsNullOrEmpty(condicion))
                sentencia = string.Format("{0} where {1}", sentencia, condicion);

            SqlConnection conexion = new SqlConnection(cadConexion);// adapter para llenar dataset y dataview, y llevarlos a la base
            SqlDataAdapter adaptador;// no se instancia de una vez, solo donde se usa  PARA EL DATASET NO SE USA COMANDO

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(setPrestamos);
                adaptador.Dispose();
            }
            catch (Exception)
            {

                throw new Exception("");
            }
            finally
            {
                conexion.Dispose();
            }
            return setPrestamos;
        }

        public int eliminar(EPrestamo presta)
        {
            string sentencia = "Delete from Prestamo where clavePrestamo=@clavePrestamo";
            int result = -1;
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@ClavePrestamo", presta.ClavePrestamo);
            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                result = -1;
                throw;
            }
            return result;
        }

        public EPrestamo buscar(string condicion)
        {
            EPrestamo  presta = new EPrestamo();
            string sentencia = "Select clavePrestamo,claveEjemplar,claveUsuario,fechaPrestamo,fechaDevolucion from Prestamo";

            sentencia = $"{sentencia} where {condicion}";

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            SqlDataReader dato;

            try
            {
                conexion.Open();
                dato = comando.ExecuteReader();
                if (dato.HasRows)
                {
                    dato.Read();//cambia de registro es como el fetch
                    presta.ClavePrestamo = dato.GetString(0);
                    presta.Ejemplar.ClaveEjemplar = dato.GetString(1);
                    presta.Usuario.ClaveUsuario = dato.GetString(2);
                    presta.FechaPrestamo = dato.GetDateTime(3);
                    presta.FechaDevolucion = dato.GetDateTime(4);
                }

                conexion.Close();
            }

            catch (Exception)
            {

                throw new Exception("No se ha encontrado el libro");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return presta;

        }

          public int modificar(EPrestamo prestamo, string claveVieja = "")
        {
            int result = -1;
            string sentencia;
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();

          
            if (string.IsNullOrEmpty(claveVieja))
            {
                sentencia = "Update Prestamo set claveEjemplar=@claveEjem,claveUsuario=@clav, fechaPrestamo=@fePres,fechaDevolucion=@claveDe where clavePrestamo=@clavePre";
            }
            else
            {
                sentencia = $"Update Prestamo set claveEjemplar=@claveEjem,claveUsuario=@clav,fechaPrestamo=@fePres,fechaDevolucion=@claveDe, clavePrestamo=@clavePre where clavePrestamo='{claveVieja}'";
            }

            comando.Connection = conexion;
            comando.CommandText = sentencia;
            comando.Parameters.AddWithValue("@claveEjem",prestamo.Ejemplar.ClaveEjemplar);
            comando.Parameters.AddWithValue("@clav",prestamo.Usuario.ClaveUsuario);
            comando.Parameters.AddWithValue("@fePres", prestamo.FechaPrestamo);
            comando.Parameters.AddWithValue("@claveDe", prestamo.FechaDevolucion);
            comando.Parameters.AddWithValue("@clavePre",prestamo.ClavePrestamo);
        
            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {

                throw new Exception("Error actualizando");
            }
            finally
            {
                comando.Dispose();
                conexion.Dispose();
            }
            return result;
        }


    }
}    

