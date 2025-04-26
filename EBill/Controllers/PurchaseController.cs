using Microsoft.AspNetCore.Mvc;

namespace EBill.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
