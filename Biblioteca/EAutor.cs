using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class EAutor
    {

        #region Atributos
        string claveAutor;
        string nombre;
        string apPaterno;
        string apMaterno;
        #endregion

        #region Propiedades
        public string ClaveAutor { get; set; }

        public string Nombre { set; get; }

        public string ApPaterno { set; get; }

        public string ApMaterno { set; get; }

        #endregion

        #region Constructores
        public EAutor (){
            ClaveAutor = string.Empty;
            Nombre = string.Empty;
            ApPaterno = string.Empty;
            ApMaterno = string.Empty;
        }

        public EAutor(string clave, string nom, string ap1,string ap2)
        {
            ClaveAutor = clave;
            Nombre = nom;
            ApPaterno = ap1;
            ApMaterno = ap2;
        }
        #endregion

    }
}
