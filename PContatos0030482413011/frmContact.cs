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

        private BindingSource bnContact = new BindingSource();
        private DataSet dsContact = new DataSet();
        private BindingSource bnCity = new BindingSource();
        private DataSet dsCity = new DataSet();
        private bool bInclusao = false;

        private void frmContact_Load(object sender, EventArgs e)
        {
            try
            {
                Contact Con = new Contact();
                dsContact.Tables.Add(Con.List());
                bnContact.DataSource = dsContact.Tables["Contato"];
                dgvContact.DataSource = bnContact;
                bnvContact.BindingSource = bnContact;

                tbxId.DataBindings.Add("TEXT", bnContact, "id_contato");
                tbxName.DataBindings.Add("TEXT", bnContact, "nome_contato");
                tbxAdress.DataBindings.Add("TEXT", bnContact, "end_contato");
                tbxPhone.DataBindings.Add("TEXT", bnContact, "cel_contato");
                tbxEmail.DataBindings.Add("TEXT", bnContact, "email_contato");
                dtpRegDate.DataBindings.Add("TEXT", bnContact, "dtcadastro_contato");

                // carrega dados da cidade
                City Cid = new City();
                dsCity.Tables.Add(Cid.List());

                cbxCity.DataSource = dsCity.Tables["Cidade"];

                //CAMPO QUE SERÁ MOSTRADO PARA O USUÁRIO

                cbxCity.DisplayMember = "nome_cidade";

                //CAMPO QUE É A CHAVE DA TABELA CIDADE E QUE LIGA COM A TABELA DE ALUNO

                cbxCity.ValueMember = "id_cidade";


                //No momento de linkar os componentes com o Binding Source linkar também o combox da cidade

                cbxCity.DataBindings.Add("SelectedValue", bnContact, "cidade_id_cidade"); // AJUSTAR DROPDOWNSTYLE PARA DropDownList PARA NAO DEIXAR 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (tbContact.SelectedIndex == 0)
            {
                tbContact.SelectTab(1);
            }

            bnContact.AddNew();

            tbxName.Enabled = true;
            tbxAdress.Enabled = true;
            cbxCity.Enabled = true;
            // vai para o primeiro 
            cbxCity.SelectedIndex = 0;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // validar os dados
            if (tbxName.Text == "")
            {
                MessageBox.Show("Nome inválido!");
            }
            else if (tbxAdress.Text == "")
            {
                MessageBox.Show("Endereço inválido!");
            }
            else if (cbxCity.SelectedIndex == -1)
            {
                MessageBox.Show("Cidade inválida!");
            }
            else if (tbxPhone.Text == "")
            {
                MessageBox.Show("Celular inválido!");
            }
            else if (tbxEmail.Text == "")
            {
                MessageBox.Show("E-mail inválido!");
            }
            else
            {
                Contact RegCon = new Contact();

                RegCon.ContactName = tbxName.Text;
                RegCon.ContactAdress = tbxAdress.Text;
                RegCon.ContactCityId = Convert.ToInt32(cbxCity.SelectedValue.ToString());
                RegCon.ContactPhone = tbxPhone.Text;
                RegCon.ContactEmail = tbxEmail.Text;
                RegCon.ContactRegisterDate = dtpRegDate.Value;


                if (bInclusao)
                {
                    if (RegCon.Insert() > 0)
                    {
                        MessageBox.Show("Contato adicionado com sucesso!");

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

                        // recarrega o grid
                        dsContact.Tables.Clear();
                        dsContact.Tables.Add(RegCon.List());
                        bnContact.DataSource = dsContact.Tables["Contato"];
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar contato!");
                    }
                }
                else
                {
                    RegCon.ContactId = Convert.ToInt32(tbxId.Text);

                    if (RegCon.Update() > 0)
                    {
                        MessageBox.Show("Contato alterado com sucesso!");

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

                        // recarrega o grid
                        dsContact.Tables.Clear();
                        dsContact.Tables.Add(RegCon.List());
                        bnContact.DataSource = dsContact.Tables["Contato"];

                    }
                    else
                    {
                        MessageBox.Show("Erro ao alterar contato!");
                    }

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbContact.SelectedIndex == 0)
            {
                tbContact.SelectTab(1);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbContact.SelectedIndex == 0)
            {
                tbContact.SelectTab(1);
            }


            if (MessageBox.Show("Confirma exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Contact RegCon = new Contact();
                RegCon.ContactId = Convert.ToInt32(tbxId.Text);
                if (RegCon.Delete() > 0)
                {
                    MessageBox.Show("Contato excluído com sucesso!");


                    // recarrega o grid
                    dsContact.Tables.Clear();
                    dsContact.Tables.Add(RegCon.List());
                    bnContact.DataSource = dsContact.Tables["Contato"];
                }
                else
                {
                    MessageBox.Show("Erro ao excluir contato!");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bnContact.CancelEdit();

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
