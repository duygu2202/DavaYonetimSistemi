using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DavaYonetimDB.Service.Services;
using DavaYonetimDB.Web.Models;

namespace DavaYonetimDB.Web.Controllers
{
    public class RaporController : Controller
    {
        private readonly IRaporLogService _raporLogService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RaporController(
            IRaporLogService raporLogService,
            UserManager<ApplicationUser> userManager)
        {
            _raporLogService = raporLogService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var logs = await _raporLogService.GetUserLogsAsync(user.Id);
            return View(logs);
        }
    }
} 