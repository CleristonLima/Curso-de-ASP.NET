using SistemaVendas.Uteis;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;

namespace SistemaVendas.Models
{
    public class LoginModel
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage="Informe o email do usuário!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email incorreto!")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário!")]
        public string Senha { get; set; }

        public bool ValidarLogin()
        {
            string sql = "SELECT ID, NOME FROM VENDEDOR WHERE EMAIL = @email AND SENHA = @senha";

            MySqlCommand Command = new MySqlCommand(sql);
            Command.Parameters.AddWithValue("@email", Email);
            Command.Parameters.AddWithValue("@senha", Senha);

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(Command);

            if(dt.Rows.Count ==1)
            {
                // Ira gravar o login e senha do usuário
                Id = dt.Rows[0]["ID"].ToString();
                Nome = dt.Rows[0]["NOME"].ToString();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
