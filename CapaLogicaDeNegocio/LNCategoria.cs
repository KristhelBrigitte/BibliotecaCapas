using System;
using System.Collections.Generic;
using System.Text;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class LNCategoria
    {

        string conexion;

        #region Constructores

        public LNCategoria()
        {
            conexion = string.Empty;
        }
        public LNCategoria(string cad)
        {
            conexion = cad;
        }
        #endregion

        #region Metodos
        public bool claveCategoriaExiste(string clav)
        {
            bool result = false;
            ADCategoria adCategoria = new ADCategoria(conexion);
            try
            {
                result = adCategoria.claveCategoriaExiste(clav);
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
