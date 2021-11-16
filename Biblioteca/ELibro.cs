using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class ELibro
    {


        #region Propiedades
        public string ClaveLibro { get; set;}  //Propiedades
        public string Titulo { get; set; }
        public string ClaveAutor { get; set; }
        public ECategoria Categoria { get; set; }
 //       public EEstado Estado { get; set; }
        public bool Existe { get; set; }
        #endregion

        #region Constructores
        public ELibro()
        {
            ClaveLibro = string.Empty;
            Titulo=string.Empty;
            ClaveAutor = string.Empty;
            Categoria = new ECategoria();
            Existe = false;
        }

        public ELibro(string claveLi,string tit, string claAutor, ECategoria Cate, bool ex)
        {
            ClaveLibro = claveLi;
            Titulo = tit;
            ClaveAutor = claAutor;
            Categoria = Cate;
            Existe = ex;

        }
        #endregion
    }


}
