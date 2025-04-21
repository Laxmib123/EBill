using EBill.DTO;
using EBill.Models.Data;
using EBill.Models.Entities;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    status = true,
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

        public async Task<IActionResult> GetAllProduct()
        {
            return View();

        }


        [HttpGet]
        public async Task<IActionResult> ListOfProduct()
        {
            var data = await _context.products
                          .Where(x => x.status == true)
                          .Include(x => x.Category)
                          .ToListAsync();
            var products = data.Select(x => new ProductReadDto
            {
                Name = x.Name,
                Id = x.Id,
                CreatedBy = x.CreatedBy == 1 ? "Admin" : "Manager",
                CreatedDate = x.CreatedDate.ToString(),
                LastModifiedBy = x.LastModifiedBy == null ? "" : x.LastModifiedBy == 1 ? "Admin" : "Manager",
                LastModifiedDate = x.LastModifiedDate.ToString(),
                Category = x.Category.Name,
                SellingPrice = x.SellingPrice,
                PurchasePrice = x.PurchasePrice
            });

            return Json(new
            {
                Status = "Success",
                Data = products
            });

        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            var data = await _context.products
                          .Include(x => x.Category)
                          .FirstOrDefaultAsync(x => x.Id == id);
            var product = new ProductUpdateDto
            {
                Name = data.Name,
                Id = data.Id,
                CreatedBy = data.CreatedBy == 1 ? "Admin" : "Manager",
                CreatedDate = data.CreatedDate.ToString(),
                LastModifiedBy = data.LastModifiedBy == null ? "" : data.LastModifiedBy == 1 ? "Admin" : "Manager",
                LastModifiedDate = data.LastModifiedDate.ToString(),
                CategoryId = data.Category.Id,
                SellingPrice = data.SellingPrice,
                PurchasePrice = data.PurchasePrice
            };

            return Json(new
            {
                Status = "Success",
                Data = product
            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateVM request)
        {
            if (request == null)
            {

                return Json(new
                {
                    Status = "Fail",
                    Message = "Unable to insert Product!!"
                });

            }
            else
            {

                var data = await _context.products
                          .Include(x => x.Category)
                          .FirstOrDefaultAsync(x => x.Id == request.Id);

                if(data == null)
                {

                    return Json(new
                    {
                        Status = "Fail",
                        Message = "Product not found!!"
                    });
                }

                data.Name = request.Name;
                data.PurchasePrice = request.PurchasePrice;
                data.SellingPrice = request.SellingPrice;
                data.CategoryId = request.CategoryId;
                data.LastModifiedBy = GetCreatedBy();
                data.LastModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return Json(new
                {
                    Status = "Success",
                    Message = "Product updated successfully!!"
                });


            }


        }


    }
}
