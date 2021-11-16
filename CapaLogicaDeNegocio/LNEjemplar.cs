using System;
using System.Collections.Generic;
using System.Text;
using CapaEntidades;
using CapaAccesoDatos;
using System.Data;

namespace CapaLogicaDeNegocio
{
    public class LNEjemplar
    {
        private string cadConexion;

        #region Constructores
        public LNEjemplar()
        {
    
            cadConexion = string.Empty;
        }
        public LNEjemplar(string cadena)
        {
         
            cadConexion = cadena;
        }
        #endregion


        public DataSet listarTodos(string condicion)
        {
            DataSet setEjemplares;
            ADEjemplar adEje = new ADEjemplar(cadConexion);
            try
            {
                setEjemplares = adEje.listarTodos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return setEjemplares;

        }

        public EEjemplar buscar(string condicion)
        {
            ADEjemplar ade=new ADEjemplar(cadConexion);
            EEjemplar eje;
            try
            {
                eje= ade.buscar(condicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eje;
        }

        public bool claveEjemplarExiste(string clav)
        {
            bool result = false;
            ADEjemplar adEjem = new ADEjemplar(cadConexion);
            try
            {
                result = adEjem.claveEjemplarExiste(clav);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
