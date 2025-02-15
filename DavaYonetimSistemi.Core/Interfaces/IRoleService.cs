namespace DavaYonetimDB.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(string id);
        Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task UpdateRoleAsync(RoleDto roleDto);
        Task DeleteRoleAsync(string id);
        Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName);
    }
} 