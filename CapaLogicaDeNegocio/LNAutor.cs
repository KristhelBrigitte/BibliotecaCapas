using System;
using System.Collections.Generic;
using System.Text;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class LNAutor
    {

        string conexion;

        #region Constructores

        public LNAutor()   
        {
            conexion = string.Empty;
        }
        public LNAutor(string cad)
        {
            conexion = cad;
        }
        #endregion

        #region Metodos
        public bool claveAutorExiste(string clav)
        {
            bool result = false;
            ADAutor adAutor = new ADAutor(conexion);
            try
            {
                result = adAutor.claveAutorExiste(clav);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion


    }
}
