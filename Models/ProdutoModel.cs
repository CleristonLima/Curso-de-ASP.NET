using SistemaVendas.Uteis;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome do produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a descrição do produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a Preço Unitário do produto")]
        public decimal? Preco_Unitario { get; set; }

        [Required(ErrorMessage = "Informe a Quantidade em estoque do produto")]
        public decimal? Quantidade_estoque { get; set; }

        [Required(ErrorMessage = "Informe a Unidade de Medida do produto")]
        public string Unidade_medida { get; set; }

        [Required(ErrorMessage = "Informe o link da imagem do produto")]
        public string Link_foto { get; set; }

        public List<ProdutoModel> ListarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            DAL objDAL = new DAL();
            string sql = "SELECT Id, Nome, Descricao, Preco_unitario, Quantidade_estoque, Unidade_medida, Link_foto FROM Produto order by Nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["ID"].ToString(),
                    Nome = dt.Rows[i]["NOME"].ToString(),
                    Descricao = dt.Rows[i]["Descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[i]["Preco_unitario"].ToString()),
                    Quantidade_estoque = decimal.Parse(dt.Rows[i]["Quantidade_estoque"].ToString()),
                    Unidade_medida = dt.Rows[i]["Unidade_medida"].ToString(),
                    Link_foto = dt.Rows[i]["Link_foto"].ToString()
                };

                lista.Add(item);
            }

            return lista;
        }

        public ProdutoModel RetornarProduto(int? id)
        {

            ProdutoModel item;
            DAL objDAL = new DAL();
            string sql = $"SELECT id, Nome, Descricao, Preco_unitario, Quantidade_estoque, Unidade_medida, Link_foto FROM Produto WHERE id='{id}' order by Nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            item = new ProdutoModel
            {
                    Id = dt.Rows[0]["Id"].ToString(),
                    Nome = dt.Rows[0]["Nome"].ToString(),
                    Descricao = dt.Rows[0]["Descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[0]["Preco_unitario"].ToString()),
                    Quantidade_estoque = decimal.Parse(dt.Rows[0]["Quantidade_estoque"].ToString()),
                    Unidade_medida = dt.Rows[0]["Unidade_medida"].ToString(),
                    Link_foto = dt.Rows[0]["Link_foto"].ToString()
            };

            return item;
        }

        // Insert ou Update

        public void Gravar() 
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;

            if (Id != null)
            {
                sql = $"UPDATE PRODUTO SET NOME = '{Nome}', " +
                    $"Descricao = '{Descricao}', " +
                    $"Preco_Unitario = '{Preco_Unitario.ToString().Replace(",",".")}', " + // Irá converter a virgula pelo ponto
                    $"Quantidade_estoque = '{Quantidade_estoque}', " +
                    $"Unidade_medida = '{Unidade_medida}', " +
                    $"Link_foto = '{Link_foto}' " +
                    $"WHERE id = '{Id}'";
            }
            else
            {
                sql = $"INSERT INTO PRODUTO(NOME, Descricao, Preco_unitario, Quantidade_estoque, Unidade_medida, Link_foto) " +
                    $"VALUES('{Nome}', '{Descricao}', '{Preco_Unitario}', '{Quantidade_estoque}', '{Unidade_medida}', '{Link_foto}')";
            }
            objDAL.ExecutarComandoSQL(sql);
        }

        //DELETE
        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM PRODUTO WHERE ID='{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
