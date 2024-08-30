using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendedorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaVendedores = new VendedorModel().ListarTodosVendedores();
            return View();
        }

        // Vai renderizar a tela Cadatro
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if (id != null)
            {
                //Carregar o registro do cliente numa ViewBag
                ViewBag.Vendedor = new VendedorModel().RetornarVendedor(id);
            }

            return View();
        }

        // Vai receber os dados
        [HttpPost]
        public IActionResult Cadastro(VendedorModel vendedor)
        {
            if(ModelState.IsValid || true)
            {
                vendedor.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirVendedor(int id)
        {
            new VendedorModel().Excluir(id);
            return View();
        }

    }
}
