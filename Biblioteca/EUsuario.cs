using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidades
{
    public class EUsuario
    {
        #region Atributos
        string claveUsuario;
        string curp;
        string nombre;
        string apPaterno;
        string apMaterno;
        DateTime fechaNacimiento;
        string email;
        string direccion;
        #endregion

        #region Propiedades
        public string ClaveUsuario { get; set; }
        public string Curp { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        #endregion

        #region Constructores
        public EUsuario()
        {
            ClaveUsuario=string.Empty;
            Curp=string.Empty;
            Nombre=string.Empty;
            ApPaterno=string.Empty;
            ApMaterno=string.Empty;
            FechaNacimiento=DateTime.Today;
            Email=string.Empty;
            Direccion=string.Empty;
        }
        public EUsuario(string clav,string cur,string nom,string apePaterno, string apeMaterno,DateTime fechaNa, string email, string dire )
        {
            ClaveUsuario = clav;
            Curp = cur;
            Nombre = nom;
            ApPaterno = apePaterno;
            ApMaterno = apMaterno;
            FechaNacimiento = fechaNa;
            Email = email;
            Direccion = dire;
        }
        #endregion

    }
}
