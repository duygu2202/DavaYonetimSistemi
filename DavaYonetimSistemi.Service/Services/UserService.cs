using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DavaYonetimDB.Core.DTOs;
using DavaYonetimDB.Core.Entities;
using DavaYonetimDB.Core.Interfaces;

namespace DavaYonetimDB.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                userDtos.Add(userDto);
            }

            return userDtos;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return userDto;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new ApplicationUser
            {
                UserName = createUserDto.Email,
                Email = createUserDto.Email,
                Ad = createUserDto.Ad,
                Soyad = createUserDto.Soyad,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            if (createUserDto.SelectedRoles?.Any() == true)
            {
                await _userManager.AddToRolesAsync(user, createUserDto.SelectedRoles);
            }

            return await GetUserByIdAsync(user.Id);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            user.Email = userDto.Email;
            user.UserName = userDto.Email;
            user.Ad = userDto.Ad;
            user.Soyad = userDto.Soyad;
            user.IsActive = userDto.IsActive;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            return await _userManager.GetRolesAsync(user);
        }

        public async Task UpdateUserRolesAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }
    }
} 