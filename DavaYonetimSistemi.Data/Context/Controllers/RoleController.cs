using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Interfaces;

namespace DavaYonetimDB.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new CreateRoleDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _roleService.CreateRoleAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _roleService.UpdateRoleAsync(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> Users(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var users = await _roleService.GetUsersInRoleAsync(role.Name);
            ViewBag.RoleName = role.Name;
            return View(users);
        }
    }
} 