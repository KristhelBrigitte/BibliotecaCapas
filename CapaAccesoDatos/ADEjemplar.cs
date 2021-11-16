using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaEntidades;


namespace CapaAccesoDatos
{
    public class ADEjemplar
    {
        string cadConexion;

        public ADEjemplar()
        {
            cadConexion = string.Empty;
        
        }

        public ADEjemplar(string cad)//Preguntar
        {
            cadConexion = cad;
   
        }


        public DataSet listarTodos(string condicion)
        {
            DataSet setEjemplares = new DataSet();
            string sentencia = "Select claveEjemplar, claveLibro,claveCondicion,claveEstado,edicion,claveEditorial,numeroPaginas " +
                "from Ejemplar";

            if (!string.IsNullOrEmpty(condicion)) //Util para filtrar
                sentencia = string.Format("{0} where {1}", sentencia, condicion);

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlDataAdapter adaptador;

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(setEjemplares);
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
            return setEjemplares;
        }

        public EEjemplar buscar(string condicion)
        {
            EEjemplar eje = new EEjemplar();
            string sentencia = "Select claveEjemplar,claveLibro,claveCondicion,claveEstado,claveEditorial,numeroPaginas from Ejemplar";

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
                    dato.Read();
                    eje.ClaveEjemplar = dato.GetString(0);
                    eje.Libro.ClaveLibro = dato.GetString(1);
                    eje.Condicion.ClaveCondicion = dato.GetString(2);
                    eje.Estado.ClaveEstado = dato.GetString(3);
                    eje.Editorial.ClaveEditorial = dato.GetString(4);
                    eje.NumeroPaginas = dato.GetInt32(5);
                }

                conexion.Close();
            }

            catch (Exception)
            {

                throw new Exception("No se ha encontrado el ejemplar");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return eje;

        }

        public bool claveEjemplarExiste(string clave)
        {
            bool result = false; object ObEscalar;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();

            comando.CommandText = "select 1 from Ejemplar Where claveEjemplar=@claveEjemplar";
            comando.Parameters.AddWithValue("@claveEjemplar", clave);
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


    }
}
