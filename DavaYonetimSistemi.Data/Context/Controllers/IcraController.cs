using Microsoft.AspNetCore.Mvc;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DavaYonetimDB.Web.Controllers
{
    public class IcraController : Controller
    {
        private readonly IIcraService _icraService;
        private readonly IExportService _exportService;
        private readonly IRaporLogService _raporLogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IcraController(
            IIcraService icraService,
            IExportService exportService,
            IRaporLogService raporLogService,
            UserManager<ApplicationUser> userManager)
        {
            _icraService = icraService;
            _exportService = exportService;
            _raporLogService = raporLogService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.ListTitle = "Tüm İcra Takipleri";
            return View();
        }

        public IActionResult Derdest()
        {
            ViewBag.ListTitle = "Derdest İcra Takipleri";
            ViewBag.Durum = "DERDEST";
            return View("Index");
        }

        public IActionResult DerdestItirazli()
        {
            ViewBag.ListTitle = "Derdest İtirazlı İcra Takipleri";
            ViewBag.Durum = "DERDEST İTİRAZLI";
            return View("Index");
        }

        public IActionResult DerdestTeminat()
        {
            ViewBag.ListTitle = "Derdest Teminat İcra Takipleri";
            ViewBag.Durum = "DERDEST TEMİNAT";
            return View("Index");
        }

        public IActionResult InfazZamanAsimi()
        {
            ViewBag.ListTitle = "İnfaz Zaman Aşımı İcra Takipleri";
            ViewBag.Durum = "İNFAZ ZAMAN AŞIMI";
            return View("Index");
        }

        public IActionResult Imha()
        {
            ViewBag.ListTitle = "İmha Edilen İcra Takipleri";
            ViewBag.Durum = "İMHA";
            return View("Index");
        }

        public IActionResult Itirazli()
        {
            ViewBag.ListTitle = "İtirazlı İcra Takipleri";
            ViewBag.Durum = "İTİRAZLI";
            return View("Index");
        }

        public IActionResult Kapali()
        {
            ViewBag.ListTitle = "Kapalı İcra Takipleri";
            ViewBag.Durum = "KAPALI";
            return View("Index");
        }

        public IActionResult Dusme()
        {
            ViewBag.ListTitle = "Düşen İcra Takipleri";
            ViewBag.Durum = "DÜŞME";
            return View("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var icra = await _icraService.GetByIdAsync(id);
            if (icra == null)
            {
                return NotFound();
            }
            return View(icra);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IcraDto icraDto)
        {
            if (ModelState.IsValid)
            {
                await _icraService.AddAsync(icraDto);
                return RedirectToAction(nameof(Index));
            }
            return View(icraDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var icra = await _icraService.GetByIdAsync(id);
            if (icra == null)
            {
                return NotFound();
            }
            return View(icra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IcraDto icraDto)
        {
            if (ModelState.IsValid)
            {
                await _icraService.UpdateAsync(icraDto);
                return RedirectToAction(nameof(Index));
            }
            return View(icraDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var icra = await _icraService.GetByIdAsync(id);
            if (icra == null)
            {
                return NotFound();
            }
            return View(icra);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _icraService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DataTableParameters parameters)
        {
            var user = await _userManager.GetUserAsync(User);
            var durum = ViewBag.Durum;

            var result = await _icraService.GetIcraListAsync(parameters, user.SirketId, durum);
            
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
            var fileName = $"Icra_Listesi_{DateTime.Now:yyyyMMddHHmmss}";
            var data = await _icraService.GetIcraExportListAsync(user.SirketId, durum);

            var fileResult = type.ToLower() switch
            {
                "excel" => await _exportService.ExportToExcelAsync(data, fileName),
                "pdf" => await _exportService.ExportToPdfAsync(data, fileName),
                _ => throw new ArgumentException("Geçersiz dosya tipi")
            };

            // Rapor logunu kaydet
            await _raporLogService.LogRaporAsync(user.Id, $"{durum ?? "TÜM"} İCRA LİSTESİ", type, fileName);

            return Json(new { success = true, fileUrl = fileResult.FileUrl });
        }
    }
} 