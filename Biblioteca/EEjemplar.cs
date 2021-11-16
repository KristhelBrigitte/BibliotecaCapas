using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class EEjemplar
    {
        #region Atributos
        string claveEjemplar;
        ELibro libro;
        ECondicion condicion;
        EEstado estado;
        string edicion;
        EEditorial editorial;
        int numeroPaginas;
        #endregion

        #region Propiedades

        public string ClaveEjemplar { get; set; }
        public ELibro Libro { get; set; }
        public ECondicion Condicion { get; set; }
        public EEstado Estado{ get; set; }
        public string Edicion { get; set; }
        public EEditorial Editorial { get; set; }
        public int NumeroPaginas { get; set; }
        #endregion

        #region Constructores
        public EEjemplar()
        {
            ClaveEjemplar = string.Empty;
            Libro = new ELibro();
            Condicion = new ECondicion();
            Estado = new EEstado();
       //     Edicion = string.Empty;
            Editorial = new EEditorial();
            NumeroPaginas= 0;
        }

        public EEjemplar(string clavEjem, ELibro lib, ECondicion condi, EEstado estado,string ed,EEditorial edi,int num)
        {
            ClaveEjemplar = string.Empty;
            Libro = new ELibro();
            Condicion = new ECondicion();
            Estado = new EEstado();
            Edicion = string.Empty;
            Editorial = new EEditorial();
            NumeroPaginas = 0;
        }

        #endregion



    }
}
