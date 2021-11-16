using System;
using System.Collections.Generic;
using System.Text;
using CapaEntidades;
using System.Data.SqlClient;
using System.Data;


namespace CapaAccesoDatos
{
    public class ADAutor
    {
        string cadConexion;

        public ADAutor()
        {
            cadConexion = string.Empty;
        }

        public ADAutor(string conexion)
        {
            cadConexion = conexion;
        }

        public bool claveAutorExiste(string clav)
        {
            bool result = false;  object ObEscalar;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();

            comando.CommandText = "select 1 from Autor Where claveAutor=@claveAutor";
            comando.Parameters.AddWithValue("@claveAutor", clav);
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
            catch(Exception)
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
