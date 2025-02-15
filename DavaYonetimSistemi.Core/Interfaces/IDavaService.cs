namespace DavaYonetimDB.Core.Interfaces
{
    public interface IDavaService : IService<Dava, DavaDto>
    {
        Task<IEnumerable<DavaDto>> GetDavasByAdliyeAsync(int adliyeId);
        Task<IEnumerable<DavaDto>> GetDavasBySorumluAsync(int sorumluId);
        Task<IEnumerable<DavaDto>> GetDavasBySirketAsync(int sirketId, bool isDavaEden);
        Task<DataTableResponse<DavaListDto>> GetDavaListAsync(DataTableParameters parameters, int sirketId, string durum = null);
        Task<IEnumerable<DavaListDto>> GetDavaExportListAsync(int sirketId, string durum = null);
    }

    public class DataTableResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
} 