using EBill.Models.Data;
using EBill.Models.Entities;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBill.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllCategory()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListOfCAtegory()
        {
            var data = await _context.categories.ToListAsync();
            return Json(new { 
                Data = data,
                Status = "Success",
            });
        }

        public async Task<IActionResult> GetById(int id)
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id) { 

            return View();
        
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> CreateCategory(CategoryVM data) 
        {
            int cretedBy = 0;

            if (User.IsInRole("Admin")) {

                cretedBy = 1;
            }
            else if(User.IsInRole("Manager")){

                cretedBy = 2;
            }

            if (data == null)
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Data is empty!!"
                });
            }
            else
            {
                var category = new Category
                {
                    Name = data.name,
                    CreatedBy = cretedBy,
                    CreatedDate = DateTime.Now,
                    status = true,
                };

                var result = await _context.categories.AddAsync(category);
                await _context.SaveChangesAsync();


                return Json(new
                {
                    Status = "Success",
                    Message = "Data insert sucessfully"
                });
            
            }
        }
    }
}
