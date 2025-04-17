using EBill.DTO;
using EBill.Models.Data;
using EBill.Models.Entities;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EBill.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductVM request)
        {
            if (request == null) {

                return Json(new
                {
                    Status = "Fail",
                    Message = "Unable to insert Product!!"
                });

            }
            else
            {
                var product = new Product
                {
                    Name = request.Name,
                    PurchasePrice = request.PurchasePrice,
                    SellingPrice = request.SellingPrice,
                    Stock = 0,
                    CategoryId = request.CategoryId,
                    CreatedBy = GetCreatedBy(),
                    CreatedDate = DateTime.Now
                };

                var stored = await _context.products.AddAsync(product);
                await _context.SaveChangesAsync();
                return Json(new
                {
                    Status = "Success",
                    Message = "Product inserted successfully!!"
                });
            }

            
        }

        private int GetCreatedBy()
        {
            int cretedBy = 0;

            if (User.IsInRole("Admin"))
            {

                cretedBy = 1;
            }
            else if (User.IsInRole("Manager"))
            {

                cretedBy = 2;
            }

            return cretedBy;
        }

    }
}
