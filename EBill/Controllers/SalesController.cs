using Microsoft.AspNetCore.Mvc;

namespace EBill.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult SalesIndex()
        {
            return View();
        }
    }
}
