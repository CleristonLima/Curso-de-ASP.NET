namespace SistemaVendas.Models
{
    public class ItemVendaModel
    {
        // Foi criado para fazer o armazenamento de dados do JSON
        public string CodigoProduto {  get; set; }
        public string DescricaoProduto { get; set; }
        public string QtdeProduto { get; set; }
        public string PrecoUnitario { get; set; }
        public string Total { get; set; }

    }
}
