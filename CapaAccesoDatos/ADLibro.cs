using CapaEntidades;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace CapaAccesoDatos
{
    public class ADLibro
    {
        #region Atributos
        private string cadConexion;
        private string mensaje;
        public string Mensaje { get; }
        #endregion

        #region Constructores
        public ADLibro()
        {
            cadConexion = string.Empty;
            mensaje = string.Empty;
        }

        public ADLibro(string cad)//Preguntar
        {
            cadConexion = cad;
            mensaje = string.Empty;
        }
        #endregion

        #region Metodos
        public bool libroRepetido(ELibro libro)
        {
            bool result = false;
            string sentencia;
            sentencia = $"Select 1 From Libro Where titulo='{libro.Titulo}' and claveAutor ='{libro.ClaveAutor}'";
            SqlCommand comandoSql = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            SqlDataReader datos;

            comandoSql.Connection = conexionSQL;
            comandoSql.CommandText = sentencia;

            try
            {
                conexionSQL.Open();
                datos = comandoSql.ExecuteReader();
                if (datos.HasRows) //result=datos.HasRows
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                conexionSQL.Close();

            }
            catch (Exception)
            {
                throw new Exception("Se ha producido un error realizando la consulta");
            }
            finally
            {
                comandoSql.Dispose();
                conexionSQL.Dispose();
            }
            return result;
        }

        public bool claveLibroRepetida(string clave)
        {
            bool result = false;
            object ObEscalar;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexion = new SqlConnection(cadConexion);
            //SqlDataReader datos;

            comando.CommandText = "select 1 from Libro Where claveLibro=@claveLibro";
            comando.Parameters.AddWithValue("@claveLibro", clave);
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

                conexion.Close();


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

        public DataSet listarTodos()
        {
            throw new NotImplementedException();
        }

        //READER LEER DEVUELVE DATOS

        public int insertar(ELibro libro)
        {
            int result = -1;
            string sentencia = "insert into Libro(claveLibro,titulo," +
            "claveAutor,claveCategoria)" +
            " values(@claveLibro,@titulo,@claveAutor,@claveCategoria)" //Con arrobas, al no interpolar
            ;

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comando.Parameters.AddWithValue("@titulo", libro.Titulo);
            comando.Parameters.AddWithValue("@claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("@claveCategoria",
            libro.Categoria.ClaveCategoria);

            try
            {
                conexion.Open();
                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw new Exception("Error");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return result;

        }

        public DataSet listarTodos(string condicion)
        {
            DataSet setLibros = new DataSet();
            string sentencia = "Select claveLibro,titulo,claveAutor,ClaveCategoria from Libro";

            if (!string.IsNullOrEmpty(condicion))
                sentencia = string.Format("{0} where {1}", sentencia, condicion);

            SqlConnection conexion = new SqlConnection(cadConexion);// adapter para llenar dataset y dataview, y llevarlos a la base
            SqlDataAdapter adaptador;// no se instancia de una vez, solo donde se usa  PARA EL DATASET NO SE USA COMANDO

            try
            {
                adaptador = new SqlDataAdapter(sentencia, conexion);
                adaptador.Fill(setLibros);
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

            //sentencia=${sentencia}where{condicion};
            return setLibros;
        }
        public ELibro buscarRegistro(string condicion)
        {
            ELibro libro = new ELibro();
            string sentencia = "Select claveLibro,titulo,claveAutor,claveCategoria from Libro";

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
                    libro.ClaveLibro = dato.GetString(0);
                    libro.Titulo = dato.GetString(1);
                    libro.ClaveAutor = dato.GetString(2);
                    libro.Categoria.ClaveCategoria = dato.GetString(3);
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
            return libro;

        }


        public int eliminar(ELibro libro)
        {
            string sentencia = "Delete from Libro where claveLibro=@claveLibro";
            int result = -1;
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);

            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);

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

        public string eliminarProcedure(ELibro libro) {
            string sentencia = "EliminarLibro"; //Procedimiento almacenado

            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);


            comando.Parameters.AddWithValue("clave", libro.ClaveLibro).Direction = ParameterDirection.Input; //Si retorna o ocupamos mandar un valor
            //PARAMETRO DE SALIDA

            comando.CommandType = CommandType.StoredProcedure;//Definir tipo

            comando.Parameters.Add("@msj", SqlDbType.VarChar, 100).Direction = ParameterDirection.InputOutput;


            // comando.Connection = conexion;
            //comando.CommandText = sentencia;

            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                mensaje = comando.Parameters["@msj"].Value.ToString(); //En comando se almacena lo que se guarda y LOS PARAMETROS DEBEN SER IGUALES A LOS DE SQL.
                conexion.Close();
            }
            catch (Exception)
            {

                throw new Exception("Se ha presentado un error con el Procedimiento Almacenado");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return mensaje;

        }

        public int modificar(ELibro libro, string claveVieja = "")
        {
            int result = -1;
            string sentencia;
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();


            if (string.IsNullOrEmpty(claveVieja))
            {
                sentencia = "Update libro set titulo=@titulo, claveAutor=@claveAutor,claveCategoria=@claveCategoria where claveLibro=@claveLibro";
            }
            else
            {
                sentencia = $"Update libro set titulo=@titulo, claveAutor=@claveAutor,claveCategoria=@claveCategoria, claveLibro=@claveLibro where claveLibro='{claveVieja}'";
            }

            comando.Connection = conexion;
            comando.CommandText = sentencia;
            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comando.Parameters.AddWithValue("@titulo", libro.Titulo);
            comando.Parameters.AddWithValue("@claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("@claveCategoria", libro.Categoria.ClaveCategoria);

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
        #endregion

        public List<ELibro> listLibros(string condicion) {
            List<ELibro> listarLibros = new List<ELibro>();
            DataTable tabla = new DataTable();
            string sentencia;
            SqlDataAdapter adaptador;

            sentencia = "Select claveLibro, titulo. claveAutor,claveCategoria from Libro";

            if (!string.IsNullOrEmpty(condicion))
            {
                sentencia = string.Format("{0} where {1} ", sentencia, condicion);
            }

            try
            {
                adaptador = new SqlDataAdapter(sentencia, cadConexion);
                adaptador.Fill(tabla);

                //LinQ
                listarLibros = (from DataRow registro in tabla.Rows
                                select new ELibro()
                                {
                                    ClaveLibro = registro[0].ToString(),
                                    Titulo = registro[1].ToString(),
                                    ClaveAutor = registro[2].ToString(),
                                    Categoria = new ECategoria()
                                    {
                                        ClaveCategoria = registro[3].ToString()
                                    }

                                }).ToList();//namespace
            }
            catch (Exception)
            {

                throw;
            }

            return listarLibros;
        
        }
            
}

}



