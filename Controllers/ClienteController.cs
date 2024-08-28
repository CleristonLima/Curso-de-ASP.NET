using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
            return View();
        }

        // Vai renderizar a tela Cadatro
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if (id != null)
            {
                //Carregar o registro do cliente numa ViewBag
                ViewBag.Cliente = new ClienteModel().RetornarCliente(id);
            }

            return View();
        }

        // Vai receber os dados
        [HttpPost]
        public IActionResult Cadastro(ClienteModel cliente)
        {
            if(ModelState.IsValid || true)
            {
                cliente.Gravar();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["IdExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirCliente(int id)
        {
            new ClienteModel().Excluir(id);
            return View();
        }

    }
}
