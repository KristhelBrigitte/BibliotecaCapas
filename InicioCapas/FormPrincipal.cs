using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InicioCapas
{
    public partial class FormPrincipal : Form
    {
        frmLibros lib;
        FrmPrestamos pre;
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void prestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (pre == null)
            {
                pre = new FrmPrestamos();
                pre.MdiParent = this;
                pre.FormClosed += new FormClosedEventHandler(cerrar);
                pre.Show();
            }
            else
            {
                pre.Activate();

            }
        }

        private void librosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (lib == null)
           {
                lib = new frmLibros();
                lib.MdiParent = this;
                lib.FormClosed += new FormClosedEventHandler(cerrar);
                lib.Show();
           }
           else
           {
                lib.Activate();
           }
        }
        private void cerrar(object sender, FormClosedEventArgs e)
        {
            lib = null ;pre = null;
        }

      
    }
 
}

