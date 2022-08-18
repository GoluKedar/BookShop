using BookShop.DataAccess.Repository.IRepository;
using BookShop.Models;
using BookShop.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();

            if (id == null)
                return View(coverType);

            // coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_GetById, parameter);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var parameters = new DynamicParameters();

                parameters.Add("@Name", coverType.Name);
                if (coverType.Id == 0)
                {
                    //_unitOfWork.CoverType.Add(coverType);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Add, parameters);

                }
                else
                {
                    //_unitOfWork.CoverType.Update(coverType);
                    parameters.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameters);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            //without Stored Procedure
            //var coverTypeObj = _unitOfWork.CoverType.GetAll(); 
            //with SP
            var coverTypeObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_GetCover_GetAll, null);
            return Json(new { data = coverTypeObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //Without SP
            //var coverTypeObj = _unitOfWork.CoverType.Get(id);
            //with sp
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var coverTypeObj = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_GetById, parameter);
            if (coverTypeObj == null)
                return Json(new { success = false, message = "Error while deleting" });
            //_unitOfWork.CoverType.Remove(coverTypeObj);
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully" });
        }
        #endregion
    }
}
