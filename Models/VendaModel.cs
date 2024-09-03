using Mysqlx.Crud;
using Newtonsoft.Json;
using SistemaVendas.Uteis;
using System.Data;
using System.Text.Json.Serialization;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        public string Id { get; set; }

        public string Cliente_Id { get; set; }

        public string Vendedor_Id { get; set; }

        public double Total { get; set; }

        public string ListaProdutos { get; set; }

        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarTodosClientes();
        }

        public List<VendedorModel> RetornarListaVendedores()
        {
            return new VendedorModel().ListarTodosVendedores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarTodosProdutos();
        }

        public void Inserir()
        {
            DAL objDAL = new DAL();

            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string sql = "INSERT INTO VENDA(Data, Total, Vendedor_id, Cliente_id) " +
                        $"VALUES('{dataVenda}', {Total.ToString().Replace(",",".")}, {Vendedor_Id}, {Cliente_Id})";

            objDAL.ExecutarComandoSQL(sql);

            // Recuperar o ID da venda
            sql = $"SELECT id from Venda Where data='{dataVenda}' and Vendedor_id={Vendedor_Id} and Cliente_id={Cliente_Id} order by Id desc limit 1";
            DataTable dt = objDAL.RetDataTable(sql);
            string id_venda = dt.Rows[0]["id"].ToString();

            // Serializar o JSON da lista de produtos selecionando e gravar na tabela itens_venda

            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);
            for (int i = 0; i < lista_produtos.Count; i++) 
            {
                sql = "insert into itens_venda(Venda_id, Produto_id, Qtde_produto, Preco_produto) " +
                    $"Values({id_venda}, {lista_produtos[i].CodigoProduto.ToString()}," +
                    $"{lista_produtos[i].QtdeProduto.ToString()}," +
                    $"{lista_produtos[i].PrecoUnitario.ToString().Replace(",",".")})";

                objDAL.ExecutarComandoSQL(sql);
            }
        }
    }
}
