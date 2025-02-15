namespace DavaYonetimDB.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardDataAsync(int sirketId);
    }
} 