using EBill.DTO;
using EBill.Models.Data;
using EBill.Models.Entities;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            var categories = data.Select(x => new CategoryReadDto
            {
                Name = x.Name,
                CreatedBy = x.CreatedBy == 1 ? "Admin":"Manager",
                CreatedDate = x.CreatedDate.ToString(),
                LastModifiedBy = x.LastModifiedBy == null?"":x.LastModifiedBy == 1?"Admin":"Manager",
                Id = x.Id,
                LastModifiedDate = x.LastModifiedDate.ToString(),

            });
            return Json(new { 
                Data = categories,
                Status = "Success",
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.categories.FirstOrDefaultAsync(x => x.Id == id);
            return Json(new
            {
                Data = category,
                Status = "Success",
            });
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
        public async Task<IActionResult> CreateCategory(CategoryVM data)
        {

            bool isAvailable = CheckIsAvailable(data.name);

            if (isAvailable) {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Category named " + "'"+data.name+"'" + " is already present!!"
                });
            }

            
            int cretedBy = GetCreatedBy();

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

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryWriteDto request)
        {
            var data = await _context.categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (data == null)
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Category not found!!"
                });
            }
            else
            {
                data.Name = request.Name;
                data.LastModifiedBy = GetCreatedBy();
                data.LastModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return Json(new
                {
                    Status = "Success",
                    Message = "Data updated successfully!!"
                });
            }
          
        }

        [HttpGet]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                _context.categories.Remove(category);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    Status = "Success",
                    Message = "Category deleted successfully!!"
                });
            }
            return Json(new
            {

            });
        }


        public bool CheckIsAvailable(string name)
        {

            var data = _context.categories.Where(x  => x.Name == name).FirstOrDefault();

            if (data == null)
            {

                return false;
                
            }
            return true;

        }

    }
}
