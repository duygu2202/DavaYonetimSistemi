namespace DavaYonetimDB.Core.Interfaces
{
    public interface IService<T, TDto> where T : class where TDto : class
    {
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task RemoveAsync(int id);
    }
} 