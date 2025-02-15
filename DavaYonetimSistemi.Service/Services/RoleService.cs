using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Entities;
using DavaYonetimDB.Core.Interfaces;

namespace DavaYonetimDB.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDtos = new List<RoleDto>();

            foreach (var role in roles)
            {
                var roleDto = _mapper.Map<RoleDto>(role);
                roleDto.UserCount = (await _userManager.GetUsersInRoleAsync(role.Name)).Count;
                roleDtos.Add(roleDto);
            }

            return roleDtos;
        }

        public async Task<RoleDto> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return null;

            var roleDto = _mapper.Map<RoleDto>(role);
            roleDto.UserCount = (await _userManager.GetUsersInRoleAsync(role.Name)).Count;
            return roleDto;
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = new IdentityRole(createRoleDto.Name);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return await GetRoleByIdAsync(role.Id);
        }

        public async Task UpdateRoleAsync(RoleDto roleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id);
            if (role == null) throw new Exception("Rol bulunamadı");

            role.Name = roleDto.Name;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) throw new Exception("Rol bulunamadı");

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole.Any())
            {
                throw new Exception("Bu role atanmış kullanıcılar var. Önce kullanıcıları başka bir role atayın.");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
} 