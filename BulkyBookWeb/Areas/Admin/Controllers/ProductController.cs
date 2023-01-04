using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Data.IRepository;
using BulkyBook.Models.ViewModels;
using BulkyBook.Models;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
                CoverTypeList = _unitOfWork.CoverTypeRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
            };
            return View(productVM);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM productVM, IFormFile file)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                //if (file != null)
                //{
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                _unitOfWork.ProductRepository.Add(productVM.Product);
                _unitOfWork.Save();
                //}
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(productVM);
            }
        }

        // GET: ProductController/Edit/5    
        public ActionResult Edit(int id)
        {
            ProductVM productVM = new()
            {
                Product = _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id),
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
                CoverTypeList = _unitOfWork.CoverTypeRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
            };
            return View(productVM);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #region API CALLS
        [HttpGet]
        public ActionResult GetAll()
        {
            string[] includeProps = { "Category", "CoverType" };
            var products = _unitOfWork.ProductRepository.GetAll(includeProps);
            return Json(new
            {
                data = products
            });
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        #endregion API CALLS
    }
}