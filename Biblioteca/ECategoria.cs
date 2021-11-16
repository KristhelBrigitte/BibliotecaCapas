using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class ECategoria
    {
        #region Atributos
        string claveCategoria;
        string descripcion;
        #endregion

        #region Propiedades
        public string ClaveCategoria { get; set; }

        public string Descripcion { get; set; }
        #endregion

        #region Constructores
        public ECategoria()
        {
            ClaveCategoria = string.Empty;
            Descripcion = string.Empty;
        }

        public ECategoria(string cla, string des)
        {
            ClaveCategoria = cla;
            Descripcion = des;
        }
        #endregion


    }
}
