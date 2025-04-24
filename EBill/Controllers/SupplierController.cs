using EBill.Models.Data;
using EBill.Models.Entities;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EBill.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ListOfSupplier()
        {
            return View();
        }

        public IActionResult SupplierCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveSupplier(SupplierSaveVM request)
        {

            if (request == null) {

                return Json(new
                {
                    Status = "Success",
                    Message = "Supplier details is empty!!"

                });

            }
            else {

                if (request.PhoneNumber.Length == 10 &&
                    request.PhoneNumber.StartsWith("98") &&
                    request.PhoneNumber.All(char.IsDigit))
                {

                    var supplier = new Supplier
                    {
                        Name = request.Name,
                        PhoneNumber = request.PhoneNumber,
                        ContactPerson = request.ContactPerson,
                        Address = request.Address,
                        EmailAddress = request.EmailAddress,
                        CreatedDate = DateTime.Now,
                        CreatedBy = GetCreatedBy(),
                        status = true,


                    };

                    await _context.suppliers.AddAsync(supplier);
                    _context.SaveChanges();



                    return Json(new
                    {
                        Status = "Success",
                        Message = "Supplier data save successfully!!"

                    });

                }
                else {

                    return Json(new
                    {
                        Status = "Failed",
                        Message = "Phone Number must be 10 digits and started from 98!!"

                    });


                }

                

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
