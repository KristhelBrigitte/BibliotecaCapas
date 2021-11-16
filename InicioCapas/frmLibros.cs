using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaLogicaDeNegocio;

namespace InicioCapas
{
    public partial class frmLibros : Form
    {
        LNLibro lnl = new LNLibro(Config.cadConexion);//Creo el objeto y paso la cadena de consturctor
        LNCategoria cat = new LNCategoria(Config.cadConexion);
        LNAutor at = new LNAutor(Config.cadConexion);

        ELibro libro;//categoria y autor juju

        ECategoria categoria = new ECategoria("C0001","Comic");
        public frmLibros()
        {
            InitializeComponent();
        }

        #region Metodos

        private void limpirTextos()
        {
            txtClaveAutor.Clear();
            txtTituloLibro.Clear();
            txtClaveCategoria.Text = categoria.ClaveCategoria;
            txtClaveLibro.Clear();
            txtClaveLibro.Focus();
            btnEliminar.Enabled = false;
            libro = null;
            llenarDGV();
        }


        public void llenarDGV(string condicion="")
        {
            LNLibro ln= new LNLibro(Config.cadConexion);
            DataSet ds;
            try
            {
                ds = ln.listarTodos(condicion);
                //ds= ln.listarTodos("titulo like %amor%");

                dgvLibros.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
     
                 mensajeError(ex);
            }
            dgvLibros.Columns[0].HeaderText = "Clave de libro";
            dgvLibros.Columns[1].HeaderText = "Titulo";
            dgvLibros.Columns[2].HeaderText = "Clave Autor"; 
            dgvLibros.Columns[3].HeaderText = "Clave Categoria";

            //dgvLibros.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
        }


        private void mensajeError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
       

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpirTextos();
        }

        private bool textosLlenos()
        {
            string msj = " ";
            bool result =false;
            if (string.IsNullOrEmpty(txtClaveLibro.Text))
            {
                msj = "Debe agregar una clave de libro";
                txtClaveLibro.Focus();

            }else if (string.IsNullOrEmpty(txtTituloLibro.Text))
            {
                msj = "Debe escribir un titulo";
                txtTituloLibro.Focus();
            }
            else if (string.IsNullOrEmpty(txtClaveCategoria.Text))
            {
                msj = "Debe escribir la clave de la categoría";
                txtClaveCategoria.Focus();

            }
            else if(string.IsNullOrEmpty(txtClaveAutor.Text))
            {
                msj = "Debe escribir la clave del autor";
                txtClaveAutor.Focus();
            }
            else
            {
                result= true;
            }
            if (!result)
            {
                MessageBox.Show(msj, "Atención", MessageBoxButtons.OK);
            }
            return result;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string clave="";
            string titulo = "";
            string autor = "";
           
            /*libro.Titulo = txtTituloLibro.Text;
            lnl.modificar(libro, "");
            llenarDGV();*/
            if (textosLlenos())
            {
                if (libro == null)
                {
                    libro = new ELibro(txtClaveLibro.Text, txtTituloLibro.Text, txtClaveAutor.Text, categoria, false);
                }
                else
                {
                    if (revisarCambios(ref clave, ref titulo, ref autor))
                    {
                        libro.ClaveAutor = txtClaveAutor.Text;
                        libro.Titulo = txtTituloLibro.Text;
                        libro.ClaveLibro = txtClaveLibro.Text;
                        libro.Categoria.ClaveCategoria = txtClaveCategoria.Text;
                    }
                }
               
                if (!libro.Existe)//insert si no existe
                {
                    insertarLibro();
                }
                else
                {
                    //update
                    if (!string.IsNullOrEmpty(clave))//false
                    {
                        if (!lnl.claveLibroRepetida(libro.ClaveLibro))
                        {
                            cambiosTituloAutor(clave,autor,titulo);
                        }
                        else
                        {
                            MessageBox.Show("Error:La nueva clave de libro ya está en uso.");
                            txtClaveLibro.Focus();
                        }

                    }
                    else
                    {
                        cambiosTituloAutor(string.Empty, autor, titulo);
                    }
                }
            }
            llenarDGV();
        }

        private void cambiosTituloAutor(string clave,string autor, string titulo)
        {
            if (!string.IsNullOrEmpty(titulo) || !string.IsNullOrEmpty(autor))
            {
                if (!lnl.libroRepetido(libro))
                {
                    hacerModificacion(clave);
                }
                else
                {
                    MessageBox.Show("No se puede actualizar porque el autor y el tiutulo ya existen");
                    limpirTextos();
                }
            }
            else
            {
                hacerModificacion(clave);
            }
        }

        private void hacerModificacion(string clave)
        {
            if (lnl.modificar(libro, clave) > 0)
            {
                MessageBox.Show("Actualizacion realizada"); 
            }
            else
            {
                MessageBox.Show("No se pudo modificar");
            }
            limpirTextos();

        }

        private bool revisarCambios(ref string clave, ref string titulo, ref string autor)
        {
            bool result = false;
            if (txtClaveLibro.Text!=libro.ClaveLibro)// compara la vieja con la nueva 
            {
                result = true;
                clave = libro.ClaveLibro;

            }

            if (txtTituloLibro.Text != libro.Titulo)// compara la vieja con la nueva 
            {
                result = true;
                clave = libro.Titulo;
            }

            if (txtClaveAutor.Text!= libro.ClaveAutor)// compara la vieja con la nueva 
            {
                result = true;
                clave = libro.ClaveAutor;
            }
            return result;
        }

        private void insertarLibro()
        {
            try
            {
                //TODO: Agregar acceso a capa de lógica
                if (!lnl.libroRepetido(libro))
                {
                    if (!lnl.claveLibroRepetida(libro.ClaveLibro))
                    {
                        if (cat.claveCategoriaExiste(libro.Categoria.ClaveCategoria))
                        {
                            if (at.claveAutorExiste(libro.ClaveAutor))
                            {

                                if (lnl.insertar(libro) > 0)
                                {
                                    MessageBox.Show("Guardado con éxito");

                                }
                                else
                                {
                                    MessageBox.Show("Error, no se logró insertar!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("La clave del autor es incorrecta");
                            }

                        }
                        else
                        {
                            MessageBox.Show("La clave de categoria es incorrecta");
                        }
                        //TODO Agregar acceso a capa de lógica
                    }
                }
                else
                {
                    MessageBox.Show("Error el libro es repetido");
                    txtClaveAutor.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTituloLibro.Focus();
            }
        }

        private void frmLibros_Load(object sender, EventArgs e)
        {
            llenarDGV();
        }

        private void dgvLibros_DoubleClick(object sender, EventArgs e)
        {
            int fila = dgvLibros.CurrentRow.Index;//Devuelve indice de la fila seleccionada
            //string condicion = "claveLibro'{0}'";//interpolar luego con string.format(cadena,parametros 1,2,3...)

            string clave = dgvLibros[0, fila].Value.ToString();
            string condicion=$"claveLibro='{clave}'";

            try
            {
                libro = lnl.buscarRegistro(condicion);
                if (libro != null)
                {
                    libro.Existe = true;
                    txtClaveLibro.Text = libro.ClaveLibro;
                    txtClaveAutor.Text = libro.ClaveAutor;
                    txtTituloLibro.Text = libro.Titulo;
                    txtClaveCategoria.Text =  libro.Categoria.ClaveCategoria;
                    btnEliminar.Enabled = true;
                }
            }   
            catch (Exception ex)
            {
                mensajeError(ex);

            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           // limpirTextos();
            DialogResult resp;
            int resultado;

            string msj;
            if (libro!=null && libro.Existe)
            {
                resp = MessageBox.Show($"Confirma que desea eliminar el libro {libro.Titulo} con codigo {libro.ClaveLibro}?", "confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    msj = lnl.eliminarProcedure(libro); 
                    MessageBox.Show(msj);
                    limpirTextos();

                  /*  resultado = lnl.eliminar(libro);  //Eliminar 1
                    if(resultado>0){
                        MessageBox.Show("Libro eliminado", "Exito!");
                        limpirTextos();

                    }
                    else if(resultado==-1)
                    {
                        MessageBox.Show("Se presentó un error", "ERROR");
                    }*/
                }
                else
                {
                    limpirTextos();
                }
            }
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
