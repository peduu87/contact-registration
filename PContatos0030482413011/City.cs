using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

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

            DataTable dTCity = new DataTable();

            try
            {
                dACity = new SqlDataAdapter("SELECT * FROM CIDADE ORDER BY NOME_CIDADE", frmMain.Connection);
                dACity.Fill(dTCity); // Dados
                dACity.FillSchema(dTCity, SchemaType.Source); // Inf. estrutura tabela.
            }
            catch (Exception)
            {
                throw; // criar uma exceção.
            }

            return dTCity;
        }
    }
}
