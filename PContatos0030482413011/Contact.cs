using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO.Ports;

namespace PContatos0030482413011
{
    internal class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactAdress { get; set; }
        public int ContactCityId { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime ContactRegisterDate { get; set; }

        public DataTable List()
        {
            SqlDataAdapter dAContact = new SqlDataAdapter();

            DataTable dBContact = new DataTable();

            try
            {
                dAContact = new SqlDataAdapter("SELECT * FROM CONTATO ORDER BY NOME_CONTATO", frmMain.Connection);
                dAContact.Fill(dBContact); // Dados
                dAContact.FillSchema(dBContact, SchemaType.Source); // Inf. estrutura tabela.
            }
            catch (Exception)
            {
                throw; // criar uma exceção.
            }

            return dBContact;
        }

        public int Insert()
        {
            int insReturn = 0;

            try
            {
                SqlCommand mycommand;

                mycommand = new SqlCommand("INSERT INTO CONTATO VALUES (@nomecontato, @endcontato, @cidadeidcidade, @celcontato, @emailcontato, @dtcadastrocontato)", frmMain.Connection);

                mycommand.Parameters.Add(new SqlParameter("@nomecontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@endcontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@cidadeidcidade", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@celcontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@emailcontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@dtcadastrocontato", SqlDbType.Date));


                mycommand.Parameters["@nomecontato"].Value = ContactName;
                mycommand.Parameters["@endcontato"].Value = ContactAdress;
                mycommand.Parameters["@cidadeidcidade"].Value = ContactCityId;
                mycommand.Parameters["@celcontato"].Value = ContactPhone;
                mycommand.Parameters["@emailcontato"].Value = ContactEmail;
                mycommand.Parameters["@dtcadastrocontato"].Value = ContactRegisterDate;

                insReturn = mycommand.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }

            return insReturn;
        }

        public int Update() // Alteração.
        {
            int updReturn = 0;

            try
            {
                SqlCommand mycommand;

                mycommand = new SqlCommand("UPDATE CONTATO SET NOME_CONTATO = @nomecontato, END_CONTATO = @endcontato," +
                    " CIDADE_ID_CIDADE = @cidadeidcidade, CEL_CONTATO = @celcontato, EMAIL_CONTATO = @emailcontato," +
                    " DTCADASTRO_CONTATO = @dtcadastrocontato WHERE ID_CONTATO = @idcontato", frmMain.Connection);

                mycommand.Parameters.Add(new SqlParameter("@idcontato", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@nomecontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@endcontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@cidadeidcidade", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@celcontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@emailcontato", SqlDbType.VarChar));
                mycommand.Parameters.Add(new SqlParameter("@dtcadastrocontato", SqlDbType.Date));


                mycommand.Parameters["@idcontato"].Value = ContactId;
                mycommand.Parameters["@nomecontato"].Value = ContactName;
                mycommand.Parameters["@endcontato"].Value = ContactAdress;
                mycommand.Parameters["@cidadeidcidade"].Value = ContactCityId;
                mycommand.Parameters["@celcontato"].Value = ContactPhone;
                mycommand.Parameters["@emailcontato"].Value = ContactEmail;
                mycommand.Parameters["@dtcadastrocontato"].Value = ContactRegisterDate;


                updReturn = mycommand.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            return updReturn;
        }

        public int Delete() // Exclusão.
        {
            int regN = 0;

            try
            {
                SqlCommand mycommand;

                mycommand = new SqlCommand("DELETE FROM CONTATO WHERE ID_CONTATO=@idcontato", frmMain.Connection);

                mycommand.Parameters.Add(new SqlParameter("@idcontato", SqlDbType.Int));
                mycommand.Parameters["@idcontato"].Value = ContactId;

                regN = mycommand.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }

            return regN;
        }
    }
}
