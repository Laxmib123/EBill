using EBill.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EBill.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductWriteDto request)
        {
            return View();
        }

    }
}
