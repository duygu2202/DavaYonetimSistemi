namespace DavaYonetimDB.Core.Interfaces
{
    public interface IIcraService : IService<Icra, IcraDto>
    {
        Task<IEnumerable<IcraDto>> GetIcrasByAdliyeAsync(int adliyeId);
        Task<IEnumerable<IcraDto>> GetIcrasBySorumluAsync(int sorumluId);
        Task<IEnumerable<IcraDto>> GetIcrasBySirketAsync(int sirketId, bool isAlacakli);
        Task<DataTableResponse<IcraListDto>> GetIcraListAsync(DataTableParameters parameters, int sirketId, string durum = null);
        Task<IEnumerable<IcraListDto>> GetIcraExportListAsync(int sirketId, string durum = null);
    }
} 