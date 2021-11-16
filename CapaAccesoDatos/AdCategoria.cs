using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
//using CapaEntidades;

namespace CapaAccesoDatos
{
    public class ADCategoria
    {
        string cadConexion;

        public ADCategoria()
        {
            cadConexion = string.Empty;
        }

        public ADCategoria(string conexion)
        {
            cadConexion = conexion;
        }

        public bool claveCategoriaExiste(string clav)
        {
            bool result = false; object ObEscalar;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();

            comando.CommandText = "select 1 from Categoria Where claveCategoria=@claveCat";
            comando.Parameters.AddWithValue("@claveCat", clav);
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
