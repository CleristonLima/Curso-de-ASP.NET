using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaProdutos = new ProdutoModel().ListarTodosProdutos();
            return View();
        }

        // Vai renderizar a tela Cadatro
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if (id != null)
            {
                //Carregar o registro do cliente numa ViewBag
                ViewBag.Produto = new ProdutoModel().RetornarProduto(id);
            }

            return View();
        }

        // Vai receber os dados
        [HttpPost]
        public IActionResult Cadastro(ProdutoModel produto)
        {
            if(ModelState.IsValid || true)
            {
                produto.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirProduto(int id)
        {
            new ProdutoModel().Excluir(id);
            return View();
        }

    }
}
