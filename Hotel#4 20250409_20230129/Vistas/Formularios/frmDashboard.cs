using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vistas.Formularios
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            ocultarSubMenu(false);
        }

        #region "Metodo para ocultar mis subMenus"
        private void ocultarSubMenu(bool estado)
        {
            pnlMenuReservas.Visible = estado;
        }

        #endregion

        private void btnReservas_Click(object sender, EventArgs e)
        {
            if (pnlMenuReservas.Visible == false)
            {
                pnlMenuReservas.Visible = true;
            }
            else
            {
                ocultarSubMenu(false);
            }
        }
    }
}
