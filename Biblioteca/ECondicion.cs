using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class ECondicion
    {
        #region Atributos
        string claveCondicion;
        string descripcion;
        #endregion

        #region Propiedades
        public string ClaveCondicion { get; set; }
        public string Descripcion { get; set; }

        #endregion

        #region Constructores

        public ECondicion()
        {
            ClaveCondicion = string.Empty;
            Descripcion = string.Empty;
        }

        public ECondicion(string clav,string des )
        {
            ClaveCondicion = clav;
            Descripcion = des;
        }



        #endregion





    }
}
