using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaDeNegocio;
using CapaEntidades;
namespace InicioCapas
{
    public partial class FrmPrestamos : Form
    {
       
        LNPrestamo pres = new LNPrestamo(Config.cadConexion);
        LNLibro lnl = new LNLibro(Config.cadConexion);//Creo el objeto y paso la cadena de consturctor
        LNCategoria cat = new LNCategoria(Config.cadConexion);
        LNAutor at = new LNAutor(Config.cadConexion);
        LNEjemplar eje = new LNEjemplar(Config.cadConexion);
        LNUsuario LnUsu = new LNUsuario(Config.cadConexion);
        EPrestamo ePres;
        const string quote = "\'";
       
        public FrmPrestamos()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        public void button3_Click(object sender, EventArgs e)
        {

            EEjemplar Eejemplar = eje.buscar("claveEjemplar="+quote+txtClaveE.Text+quote);
            if (eje.claveEjemplarExiste(txtClaveE.Text))
            {
                llenarDGVEjemplares("claveEjemplar=" + quote + txtClaveE.Text + quote);
                llenarDGVLibros("claveLibro=" + quote + Eejemplar.Libro.ClaveLibro + quote);
            }
            else
            {
                MessageBox.Show("Error el ejemplar no existe");
            }
        }

        public void llenarDGVEjemplares(string condicion = "")
        {
            LNEjemplar eje = new LNEjemplar(Config.cadConexion);
            DataSet data;
            try
            {
                data = eje.listarTodos(condicion);
        
                dgvLibros.DataSource = data.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        public void llenarDGVLibros(string condicion = "")
        {
            LNLibro ln = new LNLibro(Config.cadConexion);
            DataSet ds;
            try
            {
                ds = ln.listarTodos(condicion);
                //ds= ln.listarTodos("titulo like %amor%");

                dgvLib.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
            
            //dgvLibros.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
        }

        private void mensajeError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            
            EUsuario usuario = LnUsu.buscar("claveUsuario="+quote+txtClave.Text+quote);
            if (LnUsu.claveUsuarioExiste(txtClave.Text))
            {
                txtNomUsu.Text = usuario.Nombre;
                txtCurp.Text = usuario.Curp;
                txtAp1.Text = usuario.ApPaterno;
                txtAp2.Text = usuario.ApMaterno;
                txtFechaN.Text = usuario.FechaNacimiento.ToString();
                txtNomUsu.Text = usuario.Nombre;
                txtEmail.Text = usuario.Email;
                txtDireccion.Text = usuario.Direccion;
            }
            else
            {
                MessageBox.Show("Error:El usuario no existe");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            EPrestamo presta;
            EEjemplar Eejemplar = eje.buscar("claveEjemplar=" + quote + txtClaveE.Text + quote);
            if (LnUsu.claveUsuarioExiste(txtClave.Text))
            {
                if (eje.claveEjemplarExiste(txtClaveE.Text))
                {
                    if (!pres.clavePrestamoExiste(txtClavPres.Text))
                    {
                        string x = txtClavPres.Text;
                        LNUsuario LnUsu = new LNUsuario(Config.cadConexion);
                        EUsuario eUsuario = LnUsu.buscar("claveUsuario=" + quote + txtClave.Text + quote);
                        EEjemplar eEjemplar = eje.buscar("claveEjemplar=" + quote + txtClaveE.Text + quote);
                        presta = new EPrestamo(x, eEjemplar, eUsuario, dtpEntrega.Value, dtpDevolucion.Value);
                        pres.insertar(presta);
                        pres.
                    }
                    else
                    {
                        MessageBox.Show("Error:La clave de prestamo está repetida");
                    }
                }
                else
                {
                    MessageBox.Show("Error el ejemplar no existe");
                }
            }
            else
            {
                MessageBox.Show("Error:El usuario no existe");
            }
        }

   

        private void FrmPrestamos_Load(object sender, EventArgs e)
        {
            llenarDGVPrestamos();
        }
        public void llenarDGVPrestamos(string condicion = "")
        {
            DataSet ds;
            try
            {
                ds = pres.listarTodos(condicion);

                dgvPrestamos.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int resultado;
          //  ePres = pres.buscar($"clavePrestamo='{txtClavPres.Text}'");
            resultado = pres.eliminar(ePres);  //Eliminar 1
            if(resultado>0){
                MessageBox.Show("Libro eliminado", "Exito!");
               //limpirTextos();
            }
            else if(resultado==-1)
            {
                MessageBox.Show("Se presentó un error", "ERROR");
            }

        }

        private void dgvPrestamos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int fila = dgvPrestamos.CurrentRow.Index;//Devuelve indice de la fila seleccionada
            //string condicion = "claveLibro'{0}'";//interpolar luego con string.format(cadena,parametros 1,2,3...)

            string clave = dgvPrestamos[0, fila].Value.ToString();
            string condicion = $"clavePrestamo='{clave}'";

            try
            {
                ePres = pres.buscar(condicion);
                if (ePres != null)
                {
                    // ePres.Existe = true;
                    txtClavPres.Text = ePres.ClavePrestamo;
                    txtClaveE.Text = ePres.Ejemplar.ClaveEjemplar;
                    txtClave.Text = ePres.Usuario.ClaveUsuario;
                    btnEliminar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int resultado;

            if(txtClavPres.Text != ePres.ClavePrestamo)// compara la vieja con la nueva 
            {
                string claveVieja = ePres.ClavePrestamo;
                llenarActualizacion();
                resultado = pres.modificar(ePres,claveVieja);
                if (resultado > 0)
                {
                    MessageBox.Show("Prestamo actualizado!");
                    llenarDGVPrestamos();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
            else
            {
                llenarActualizacion();
                resultado = pres.modificar(ePres);
                llenarDGVPrestamos();
            }

     

        }
        public void llenarActualizacion()
        {
            ePres.ClavePrestamo = txtClavPres.Text;
            ePres.Usuario.ClaveUsuario = txtClave.Text;
            ePres.Ejemplar.ClaveEjemplar = txtClaveE.Text;
            ePres.FechaPrestamo = dtpEntrega.Value;
            ePres.FechaDevolucion = dtpDevolucion.Value;
        }
       
    }
}
