using Microsoft.AspNetCore.Mvc;

using BulkyBook.Data.IRepository;
using BulkyBook.Models;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: CoverTypeController
        public ActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverTypeRepository.GetAll();
            return View(coverTypeList);
        }

        // GET: CoverTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoverTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoverType coverType, IFormCollection collection)
        {
            try
            {
                _unitOfWork.CoverTypeRepository.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Create Cover Type Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoverTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var coverType = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            return View(coverType);
        }

        // POST: CoverTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CoverType coverType, IFormCollection collection)
        {
            try
            {
                _unitOfWork.CoverTypeRepository.Update(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Update Cover Type Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoverTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoverTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var coverType = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(c => c.Id == id);
                _unitOfWork.CoverTypeRepository.Remove(coverType);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
