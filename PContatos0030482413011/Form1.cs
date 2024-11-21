using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace PContatos0030482413011
{
    public partial class frmMain : Form
    {
        public static SqlConnection Connection;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                Connection = new SqlConnection("data source=SILVA-PC;initial catalog=LP2;trusted_connection=true"); // Connection String.
                Connection.Open();
            }
            catch (Exception ex) // Connection error.
            {
                MessageBox.Show($"Erro ao abrir banco de dados:\"{ex.Message}\".");
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cadastroDeContatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmContact frmContact = new frmContact();
            frmContact.MdiParent = this;
            frmContact.WindowState = FormWindowState.Maximized;
            frmContact.Show();
        }
    }
}
