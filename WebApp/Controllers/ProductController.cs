using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
	public class ProductController : Controller
	{
		AppDbContext _context;
		IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;

        }

		public IActionResult Index()
		{
			IEnumerable<Product> products = _context.Products;
			foreach(var x in products)
			{
				x.Category = _context.Categories.FirstOrDefault(obj => obj.Id == x.CategoryId);
			}
			return View(products);
		}

		public IActionResult Upsert(int? id)
		{
			ProductVM productVM = new ProductVM
			{
				Product = new Product (),
				ListCategory = _context.Categories.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				})
			};
			
			if (id == null)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _context.Products.Find(id);
				if (productVM.Product == null)
				{
					return NotFound();
				}
				return View(productVM);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
			if (ModelState.IsValid)
			{
				var files = HttpContext.Request.Form.Files;
				string webRootPath = _webHostEnvironment.WebRootPath;

				if (productVM.Product.Id==0)
				{
					string upload = webRootPath + WC.ImagePass;
					string fileName = Guid.NewGuid().ToString();
					string extension = Path.GetExtension(files[0].FileName);

					using (var FileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
					{
						files[0].CopyTo(FileStream);
					}

					productVM.Product.Img = fileName + extension;
					_context.Products.Add(productVM.Product);

				}
				else
				{

				}
                _context.SaveChanges();

                RedirectToAction("Index");
			}
			productVM.ListCategory = _context.Categories.Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
            return View(productVM);
        }
	
		public IActionResult Create()
		{
            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                ListCategory = _context.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(productVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM productVM)
        {
			if (ModelState.IsValid)
			{
				_context.Products.Add(productVM.Product);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}

            return View(productVM);
        }
       
		public IActionResult Edit(int? id)
        {
            if (id == 0|| id==null){return NotFound();}
			Product product = _context.Products.Find(id);
			if (product == null){return NotFound();}
			ProductVM productVM = new ProductVM
			{
				Product = product,
				ListCategory = _context.Categories.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				})
			};
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(productVM.Product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productVM);
        }
    }
}
