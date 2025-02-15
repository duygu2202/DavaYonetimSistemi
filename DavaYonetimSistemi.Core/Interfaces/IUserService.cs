namespace DavaYonetimDB.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(string id);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task UpdateUserRolesAsync(string userId, IEnumerable<string> roles);
    }
} 