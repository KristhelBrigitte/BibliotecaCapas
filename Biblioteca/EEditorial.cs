using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class EEditorial
    {
        #region Atributos
        string claveEditorial;
        string nombre;
        #endregion

        #region Propiedades
        public string ClaveEditorial { get; set; }
        public string Nombre { get; set;}

        #endregion

        #region Constructores
        public EEditorial()
        {
            ClaveEditorial = string.Empty;
            Nombre = string.Empty;
        }

        public EEditorial(string clav, string nom)
        {
            ClaveEditorial = clav;
            Nombre = nom;
        }
        #endregion

    }
}
