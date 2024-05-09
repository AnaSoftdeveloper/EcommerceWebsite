using Bulky.Models.Dtos;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepo)
        {
            _logger = logger;
            _productRepository = productRepo;
        }

        public IActionResult Index()
        {
            //fetch all products
            IEnumerable<Product> lstProduct = _productRepository.GetAll(includeProperties:"Category");
            return View(lstProduct);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(CreateCustomerDto customer)
        {


            return View();
        }

        public IActionResult Details(int Id)
        {
            Product product = _productRepository.Get(u=>u.Id==Id, includeProperties:"Category");
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
