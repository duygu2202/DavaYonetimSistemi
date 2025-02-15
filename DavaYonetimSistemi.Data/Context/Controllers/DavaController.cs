using Microsoft.AspNetCore.Mvc;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DavaYonetimDB.Web.Controllers
{
    public class DavaController : Controller
    {
        private readonly IDavaService _davaService;
        private readonly IExportService _exportService;
        private readonly IRaporLogService _raporLogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DavaController(
            IDavaService davaService,
            IExportService exportService,
            IRaporLogService raporLogService,
            UserManager<ApplicationUser> userManager)
        {
            _davaService = davaService;
            _exportService = exportService;
            _raporLogService = raporLogService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.ListTitle = "Tüm Davalar";
            return View();
        }

        public IActionResult Derdest()
        {
            ViewBag.ListTitle = "Derdest Davalar";
            ViewBag.Durum = "DERDEST";
            return View("Index");
        }

        public IActionResult Karar()
        {
            ViewBag.ListTitle = "Karar Verilen Davalar";
            ViewBag.Durum = "KARAR";
            return View("Index");
        }

        public IActionResult Anayasa()
        {
            ViewBag.ListTitle = "Anayasa Mahkemesi Davaları";
            ViewBag.Durum = "ANAYASA MAHKEMESİ";
            return View("Index");
        }

        public IActionResult Hitam()
        {
            ViewBag.ListTitle = "Hitam Verilen Davalar";
            ViewBag.Durum = "HİTAM";
            return View("Index");
        }

        // Diğer durum action'ları...

        [HttpPost]
        public async Task<IActionResult> GetList(DataTableParameters parameters)
        {
            var user = await _userManager.GetUserAsync(User);
            var durum = ViewBag.Durum; // Eğer belirli bir durum için filtreleme yapılacaksa

            var result = await _davaService.GetDavaListAsync(parameters, user.SirketId, durum);
            
            return Json(new
            {
                draw = parameters.Draw,
                recordsTotal = result.TotalCount,
                recordsFiltered = result.FilteredCount,
                data = result.Data
            });
        }

        [HttpPost]
        public async Task<IActionResult> Export(string type, string durum)
        {
            var user = await _userManager.GetUserAsync(User);
            var fileName = $"Dava_Listesi_{DateTime.Now:yyyyMMddHHmmss}";
            var data = await _davaService.GetDavaExportListAsync(user.SirketId, durum);

            var fileResult = type.ToLower() switch
            {
                "excel" => await _exportService.ExportToExcelAsync(data, fileName),
                "pdf" => await _exportService.ExportToPdfAsync(data, fileName),
                _ => throw new ArgumentException("Geçersiz dosya tipi")
            };

            // Rapor logunu kaydet
            await _raporLogService.LogRaporAsync(user.Id, $"{durum ?? "TÜM"} DAVA LİSTESİ", type, fileName);

            return Json(new { success = true, fileUrl = fileResult.FileUrl });
        }

        public async Task<IActionResult> Details(int id)
        {
            var dava = await _davaService.GetByIdAsync(id);
            if (dava == null)
            {
                return NotFound();
            }
            return View(dava);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DavaDto davaDto)
        {
            if (ModelState.IsValid)
            {
                await _davaService.AddAsync(davaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(davaDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dava = await _davaService.GetByIdAsync(id);
            if (dava == null)
            {
                return NotFound();
            }
            return View(dava);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DavaDto davaDto)
        {
            if (ModelState.IsValid)
            {
                await _davaService.UpdateAsync(davaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(davaDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dava = await _davaService.GetByIdAsync(id);
            if (dava == null)
            {
                return NotFound();
            }
            return View(dava);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _davaService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 