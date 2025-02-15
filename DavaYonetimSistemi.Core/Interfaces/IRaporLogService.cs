public interface IRaporLogService
{
    Task LogRaporAsync(string userId, string raporAdi, string dosyaTipi, string dosyaAdi);
    Task<IEnumerable<RaporLogDto>> GetUserLogsAsync(string userId);
    Task<IEnumerable<RaporLogDto>> GetAllLogsAsync();
} 