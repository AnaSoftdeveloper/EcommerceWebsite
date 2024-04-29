using Bulky.Models;
using Bulky.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BulkyWeb.Controllers
{
    public class CustomerController : Controller
    {
        private List<Customer> lstCustomers;
        public CustomerController() {
            lstCustomers = new List<Customer>()
            {
                new Customer(){id=1, customerName="sara", customerEmail="sara@gmail.com" },
                new Customer(){id=2, customerName="Peter", customerEmail="peter@gmail.com" },
                new Customer(){id=3, customerName="Jackie", customerEmail="Jackie@gmail.com" }
            };
        }
        public IActionResult Index()
        {      
            return View(lstCustomers);
        }

        [HttpGet]
        public IActionResult CreateCustomer(){
            List<Province> lstProv = new List<Province>() { 
            new Province{Id=1, Name="ON"},
            new Province{Id=2,Name="BC"}
            };

            ViewBag.provList = lstProv;

            return View();
        }
        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerDto CreateCustomerDto)
        {
            if (CreateCustomerDto == null) { return View();}
  
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    customerName = CreateCustomerDto.customerName,
                    customerEmail = CreateCustomerDto.customerEmail,
                    customerPhone = CreateCustomerDto.customerPhone,
                    pronoun = CreateCustomerDto.pronoun,
                    province = CreateCustomerDto.province
                };
            } 
            return RedirectToAction("Index");
        }
    }  
}
