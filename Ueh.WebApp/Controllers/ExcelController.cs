using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using System.Data.OleDb;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Repositorys;

namespace Ueh.WebApp.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IPhanCongRepository _phancongRepository;
        private readonly UehDbContext context;

        public ExcelController(UehDbContext context, IPhanCongRepository phancongRepository)
        {
            _phancongRepository = phancongRepository;
            this.context = context;

        }
        public IActionResult Index()
        {
            var data = _phancongRepository.GetPhanCongs();
            return View(data);
        }

        public IActionResult ImportExcelFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportExcelFile(IFormFile formFile)
        {
            try
            {
                if (_phancongRepository.ImportExcelFile(formFile))
                    ViewBag.message = "formFile Import Success";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public IActionResult ExportToExcel()
        {
            var content = _phancongRepository.ExportToExcel();
            if (content != null)
            {
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "dsphancong.xlsx");
            }
            return ViewBag.message = "formFile Import Success";


        }

    }
}
