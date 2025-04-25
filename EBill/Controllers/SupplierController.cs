using EBill.DTO;
using EBill.Models.Data;
using EBill.Models.Entities;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    Status = "Failed",
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

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {

            var data = await _context.suppliers.Where(x => x.status == true).ToListAsync();

            if (data.Count > 0) {

                var suppliers = data.Select(x => new SupplierReadDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    ContactPerson = x.ContactPerson,
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    CreatedBy = x.CreatedBy == 1 ? "Admin" : "Manager",
                    CreatedDate = x.CreatedDate.ToString(),
                    LastModifiedBy = x.LastModifiedBy == null ? "" : x.LastModifiedBy == 1 ? "Admin" : "Manager",
                    LastModifiedDate = x.LastModifiedDate.ToString()


                });


                return Json(new
                {
                    Status = "Success",
                    Data = suppliers

                });

            }

            return Json(new
            {
                Status = "Failed",
                Message = "Data not found" +
                "!"

            });

        }


        [HttpGet]
        public async Task<IActionResult> DeleteSupplier(int id)
        {

            var data = await GetById(id);
            if(data == null)
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Supplier is not found!!"
                });
            }

            data.status = false;
            await _context.SaveChangesAsync();

            return Json(new
            {
                Status = "Success",
                Message = "Supplier Deleted Successfully!!"
            });

        }

        public async Task<Supplier> GetById(int id)
        {
            var data = await _context.suppliers.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }


        [HttpGet]
        public async Task<IActionResult> GetDataById(int id)
        {

            var data = await GetById(id);
            if (data == null)
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Supplier is not found!!"
                });
            }

            var supplier = new SupplierReadDto
            {
                Id = data.Id,
                Name = data.Name,
                Address = data.Address,
                ContactPerson = data.ContactPerson,
                EmailAddress = data.EmailAddress,
                PhoneNumber = data.PhoneNumber,
                CreatedBy = data.CreatedBy == 1 ? "Admin" : "Manager",
                CreatedDate = data.CreatedDate.ToString(),
                LastModifiedBy = data.LastModifiedBy == null ? "" : data.LastModifiedBy == 1 ? "Admin" : "Manager",
                LastModifiedDate = data.LastModifiedDate.ToString()

            };
          

            return Json(new
            {
                Status = "Success",
                Data = supplier
            });

        }


        [HttpPost]
        public async Task<IActionResult> UpdateSuppliers(SupplierUpdateDto request)
        {
            if (request == null)
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Supplier details is empty!!"

                });


            }

            var data = await GetById(request.Id);

            if (data == null)
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Supplier details is empty!!"

                });


            }

            data.Address = request.Address;
            data.ContactPerson = request.ContactPerson;
            data.EmailAddress = request.EmailAddress;
            data.PhoneNumber = request.PhoneNumber;
            data.Name = request.Name;
            data.LastModifiedBy = GetCreatedBy();
            data.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Json(new
            {
                Status = "Success",
                Message = "Supplier Updated Successfully!!"

            });



        }


    }


}
