using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class EEstado
    {
        #region Atributos
        string claveEstado;
        string descripcion;
        #endregion

        #region Propiedades
        public string ClaveEstado { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Constructores{
        public EEstado()
        {
            ClaveEstado = string.Empty;
            Descripcion = string.Empty;
        }

        public EEstado(string clave, string des)
        {
            ClaveEstado = clave;
            Descripcion = des;
        }
        #endregion

    }
}
