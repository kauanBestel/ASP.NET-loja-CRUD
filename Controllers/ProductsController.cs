using loja.Models;
using loja.Services;
using Microsoft.AspNetCore.Mvc;


namespace loja.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AplicationBdContext context;
        private readonly IWebHostEnvironment environment;

        public ProductsController(AplicationBdContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            //var products = context.Products.ToList();
            var products = context.Products.OrderByDescending(p => p.Id).ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {

            if (productDto.ImageFileName == null)
            {
                ModelState.AddModelError("ImageFileName", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(productDto);
            }


            //salvar arquivo da imagen
            string newFileName = DateTime.Now.ToString("yyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ImageFileName!.FileName);

            string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.ImageFileName.CopyTo(stream);
            }

            Product product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageFile = newFileName,
                DateTime = DateTime.Now,
            };


            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("index", "Products");
        }

    }
}
