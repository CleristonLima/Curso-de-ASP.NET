using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaVendas.Uteis
{
    // Data Access Layer
    public class DAL
    {
        private static string Server = "localhost";
        private static string Database = "sistema_venda";
        private static string User = "root";
        private static string Password = "";
        private static string ConnectionString = $"Server={Server};Database={Database};Uid={User};Pwd={Password};SslMode=None;Charset=utf8;";
        private static MySqlConnection Connection;

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
        }

        // Espera um parametro do tipo string contendo um comando SQL do tipo SELECT
        public DataTable RetDataTable(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand Command = new MySqlCommand(sql, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(Command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Recebe o comando preparado
        public DataTable RetDataTable(MySqlCommand Command)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                Command.Connection = conn;

                MySqlDataAdapter da = new MySqlDataAdapter(Command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Espera um parametro do tipo string contendo um comando SQL do tipo INSERT, UPDATE E DELETE
        public void ExecutarComandoSQL(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand Command = new MySqlCommand(sql, conn);
                Command.ExecuteNonQuery();
            }
        }
    }
}