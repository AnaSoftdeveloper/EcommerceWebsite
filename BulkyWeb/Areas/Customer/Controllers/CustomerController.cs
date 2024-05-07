using Bulky.Models;
using Bulky.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerController : Controller
    {
        private readonly List<CustomerDto> lstCustomers;
        public CustomerController()
        {
            lstCustomers = new List<CustomerDto>()
            {
                new CustomerDto(){ customerName="sara", customerEmail="sara@gmail.com" },
                new CustomerDto(){ customerName="Peter", customerEmail="peter@gmail.com" },
                new CustomerDto(){ customerName="Jackie", customerEmail="Jackie@gmail.com" }
            };
        }
        public IActionResult Index()
        {
            return View(lstCustomers);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
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
            if (CreateCustomerDto == null) { return View(); }

            if (ModelState.IsValid)
            {
                CustomerDto customer = new CustomerDto()
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
