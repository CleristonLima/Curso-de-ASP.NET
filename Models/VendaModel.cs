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

        public string Data { get; set; }

        public string Cliente_Id { get; set; }

        public string Vendedor_Id { get; set; }

        public double Total { get; set; }

        public string ListaProdutos { get; set; }

        // Para atender o filtro do relatório
        public List<VendaModel> ListagemVendas(string DataDe, string DataAte)
        {
            return RetornarListagemVendas(DataDe, DataAte);
        }

        // Listagem Geral
        public List<VendaModel> ListagemVendas() 
        {
            return RetornarListagemVendas("1900/01/01", "2200/01/01");
        }

        
        private List<VendaModel> RetornarListagemVendas(string DataDe, string DataAte)
        {
            List<VendaModel> lista = new List<VendaModel>();
            VendaModel item;
            DAL objDAL = new DAL();

            string sql = "SELECT v1.Id, v1.Data, v1.Total, v2.Nome as Vendedor, c.Nome as Cliente from " +
                        "Venda v1 inner join Vendedor v2 on v1.Vendedor_id = v2.id " +
                        "inner join Cliente c on v1.Cliente_id = c.id " +
                        $"WHERE v1.Data >= '{DataDe}' and v1.Data <= '{DataAte}' " +
                        "order by v1.data, total";

            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Data = DateTime.Parse(dt.Rows[i]["Data"].ToString()).ToString("dd/MM/yyyy"),
                    Total = double.Parse(dt.Rows[i]["Total"].ToString()),
                    Vendedor_Id = dt.Rows[i]["Vendedor"].ToString(),
                    Cliente_Id = dt.Rows[i]["Cliente"].ToString()
                };

                lista.Add(item);
            }

            return lista;

        }

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

                // Baixa o produto do estoque
                sql = "Update produto " +
                      $"set Quantidade_estoque = (Quantidade_estoque - " + int.Parse(lista_produtos[i].QtdeProduto.ToString()) + ") " + 
                      $"where id = {lista_produtos[i].CodigoProduto.ToString()}";

                objDAL.ExecutarComandoSQL(sql);
            }
        }
    }
}
