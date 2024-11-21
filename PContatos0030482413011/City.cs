using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PContatos0030482413011
{
    internal class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityFu { get; set; }

        public DataTable List()
        {
            SqlDataAdapter dACity = new SqlDataAdapter();

            DataTable dTCity = new DataTable(); // Table with the cities data.

            try
            {
                dACity = new SqlDataAdapter("SELECT * FROM CIDADE ORDER BY NOME_CIDADE", frmMain.Connection); // SQL command to get the data in the db, ordered by city name.
                dACity.Fill(dTCity); // Data.
                dACity.FillSchema(dTCity, SchemaType.Source); // Table structure.
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar a tabela de cidades: {ex.Message}");
            }

            return dTCity;
        }
    }
}
