using Microsoft.AspNetCore.Mvc;

namespace SistemaVendas.Controllers
{
    public class ConfiguracoesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
