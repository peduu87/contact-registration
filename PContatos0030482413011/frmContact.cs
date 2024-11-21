using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PContatos0030482413011
{
    public partial class frmContact : Form
    {
        public frmContact()
        {
            InitializeComponent();
        }

        private BindingSource bsContact = new BindingSource();
        private DataSet dsContact = new DataSet();
        private BindingSource bnCity = new BindingSource(); // Unused, 'cause cities are not updated in this form.
        private DataSet dsCity = new DataSet();
        private bool bInclusao = false;

        private void frmContact_Load(object sender, EventArgs e)
        {
            try
            {
                Contact Con = new Contact();
                dsContact.Tables.Add(Con.List()); // Gets the data table CONTATO.
                bsContact.DataSource = dsContact.Tables["Contato"]; // Pass CONTATO to BindingSource.
                dgvContact.DataSource = bsContact; // Links CONTATO as the data source to the DataGridView, wich shows it.
                bnvContact.BindingSource = bsContact; // Links CONTATO management to the BindingNavigator.

                // Links the Controls to the db columns.
                tbxId.DataBindings.Add("TEXT", bsContact, "id_contato");
                tbxName.DataBindings.Add("TEXT", bsContact, "nome_contato");
                tbxAdress.DataBindings.Add("TEXT", bsContact, "end_contato");
                tbxPhone.DataBindings.Add("TEXT", bsContact, "cel_contato");
                tbxEmail.DataBindings.Add("TEXT", bsContact, "email_contato");
                dtpRegDate.DataBindings.Add("TEXT", bsContact, "dtcadastro_contato");

                // Loads cities.
                City Cit = new City();
                dsCity.Tables.Add(Cit.List()); // Gets the data table CIDADE.

                cbxCity.DataSource = dsCity.Tables["Cidade"]; // Defines CIDADE as the ComboBox data source.

                // This column will be shown to the user.
                cbxCity.DisplayMember = "nome_cidade";

                // Key that links CIDADE and CONTATO tables.
                cbxCity.ValueMember = "id_cidade";

                cbxCity.DataBindings.Add("SelectedValue", bsContact, "cidade_id_cidade"); // Remember to Change DropDownStyle to DropDownList
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar: {ex.Message}");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (tbContact.SelectedIndex == 0)
            {
                tbContact.SelectTab(1); // Pass the user to the "Detalhes" tab, to fill it with info.
            }

            bsContact.AddNew();

            tbxName.Enabled = true;
            tbxAdress.Enabled = true;
            cbxCity.Enabled = true;
            cbxCity.SelectedIndex = 0; // Selects the first city as default.
            tbxPhone.Enabled = true;
            tbxEmail.Enabled = true;
            dtpRegDate.Enabled = true;

            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bInclusao = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbContact.SelectedIndex == 0)
            {
                tbContact.SelectTab(1); // Pass the user to the "Detalhes" tab, to fill it with info.
            }

            tbxName.Enabled = true;
            tbxAdress.Enabled = true;
            cbxCity.Enabled = true;
            tbxPhone.Enabled = true;
            tbxEmail.Enabled = true;
            dtpRegDate.Enabled = true;

            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            bInclusao = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Data validation.
            if (tbxName.Text == String.Empty)
            {
                MessageBox.Show("Nome inválido.");
            }
            else if (tbxAdress.Text == String.Empty)
            {
                MessageBox.Show("Endereço inválido.");
            }
            else if (cbxCity.SelectedIndex == -1)
            {
                MessageBox.Show("Cidade inválida.");
            }
            else if (tbxPhone.Text == String.Empty)
            {
                MessageBox.Show("Celular inválido.");
            }
            else if (tbxEmail.Text == String.Empty)
            {
                MessageBox.Show("E-mail inválido.");
            }
            else
            {
                Contact RegCon = new Contact();

                // Creates a Contact object to register the info.
                RegCon.ContactName = tbxName.Text;
                RegCon.ContactAdress = tbxAdress.Text;
                RegCon.ContactCityId = Convert.ToInt32(cbxCity.SelectedValue.ToString());
                RegCon.ContactPhone = tbxPhone.Text;
                RegCon.ContactEmail = tbxEmail.Text;
                RegCon.ContactRegisterDate = dtpRegDate.Value;

                if (bInclusao) // Used when adding a new contact.
                {
                    if (RegCon.Insert() > 0) // If some row was affected.
                    {
                        MessageBox.Show("Contato adicionado com sucesso.");

                        tbxName.Enabled = false;
                        tbxAdress.Enabled = false;
                        cbxCity.Enabled = false;
                        tbxPhone.Enabled = false;
                        tbxEmail.Enabled = false;
                        dtpRegDate.Enabled = false;

                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;

                        bInclusao = false;

                        // Refresh.
                        dsContact.Tables.Clear();
                        dsContact.Tables.Add(RegCon.List());
                        bsContact.DataSource = dsContact.Tables["Contato"];
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar contato.");
                    }
                }
                else // Used when updating a contact.
                {
                    RegCon.ContactId = Convert.ToInt32(tbxId.Text); // Sets the ID to the Contact object.

                    if (RegCon.Update() > 0) // If some row was affected.
                    {
                        MessageBox.Show("Contato alterado com sucesso.");

                        tbxName.Enabled = false;
                        tbxAdress.Enabled = false;
                        cbxCity.Enabled = false;
                        tbxPhone.Enabled = false;
                        tbxEmail.Enabled = false;
                        dtpRegDate.Enabled = false;

                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;

                        // Refresh.
                        dsContact.Tables.Clear();
                        dsContact.Tables.Add(RegCon.List());
                        bsContact.DataSource = dsContact.Tables["Contato"];

                    }
                    else
                    {
                        MessageBox.Show("Erro ao alterar contato.");
                    }

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbContact.SelectedIndex == 0)
            {
                tbContact.SelectTab(1);
            }

            if (MessageBox.Show("Confirmar exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Contact RegCon = new Contact();
                RegCon.ContactId = Convert.ToInt32(tbxId.Text);
                if (RegCon.Delete() > 0)
                {
                    MessageBox.Show("Contato excluído com sucesso.");

                    // Refresh.
                    dsContact.Tables.Clear();
                    dsContact.Tables.Add(RegCon.List());
                    bsContact.DataSource = dsContact.Tables["Contato"];
                }
                else
                {
                    MessageBox.Show("Erro ao excluir contato.");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bsContact.CancelEdit();

            tbxName.Enabled = false;
            tbxAdress.Enabled = false;
            cbxCity.Enabled = false;
            tbxPhone.Enabled = false;
            tbxEmail.Enabled = false;
            dtpRegDate.Enabled = false;

            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            bInclusao = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
