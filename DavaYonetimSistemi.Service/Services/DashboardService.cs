namespace DavaYonetimDB.Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DashboardDto> GetDashboardDataAsync(int sirketId)
        {
            var sirket = await _unitOfWork.Sirketler.GetByIdAsync(sirketId);
            
            var dashboard = new DashboardDto
            {
                SirketAdi = sirket.Name,
                Sayilar = await GetSayilarAsync(sirketId),
                Grafikler = await GetGrafiklerAsync(sirketId)
            };

            return dashboard;
        }

        private async Task<DashboardSayilar> GetSayilarAsync(int sirketId)
        {
            var davalar = await _unitOfWork.Davalar
                .Where(d => d.DavaSirketleri.Any(ds => ds.SirketId == sirketId))
                .ToListAsync();

            var icralar = await _unitOfWork.Icralar
                .Where(i => i.IcraSirketleri.Any(ic => ic.SirketId == sirketId))
                .ToListAsync();

            return new DashboardSayilar
            {
                ToplamDavaSayisi = davalar.Count,
                ToplamIcraSayisi = icralar.Count,
                ToplamDavaDegeri = davalar.Sum(d => d.DavaDegeri),
                ToplamIcraTakipDegeri = icralar.Sum(i => i.TakipCikisi),
                DerdestDavaSayisi = davalar.Count(d => d.DurumDava.Name == "DERDEST"),
                DerdestIcraSayisi = icralar.Count(i => i.DurumIcra.Name == "DERDEST")
            };
        }

        private async Task<DashboardGrafikler> GetGrafiklerAsync(int sirketId)
        {
            return new DashboardGrafikler
            {
                DavaDurumlari = await GetDavaDurumlariAsync(sirketId),
                IcraDurumlari = await GetIcraDurumlariAsync(sirketId),
                AylikDavaIstatistikleri = await GetAylikDavaIstatistikleriAsync(sirketId),
                AylikIcraIstatistikleri = await GetAylikIcraIstatistikleriAsync(sirketId)
            };
        }

        private async Task<List<GrafikVeri>> GetDavaDurumlariAsync(int sirketId)
        {
            var davalar = await _unitOfWork.Davalar
                .Where(d => d.DavaSirketleri.Any(ds => ds.SirketId == sirketId))
                .Include(d => d.DurumDava)
                .ToListAsync();

            var durumlar = davalar
                .GroupBy(d => d.DurumDava.Name)
                .Select(g => new GrafikVeri
                {
                    Etiket = g.Key,
                    Deger = g.Count(),
                    Renk = GetDurumRengi(g.Key)
                })
                .ToList();

            return durumlar;
        }

        private async Task<List<GrafikVeri>> GetIcraDurumlariAsync(int sirketId)
        {
            var icralar = await _unitOfWork.Icralar
                .Where(i => i.IcraSirketleri.Any(ic => ic.SirketId == sirketId))
                .Include(i => i.DurumIcra)
                .ToListAsync();

            var durumlar = icralar
                .GroupBy(i => i.DurumIcra.Name)
                .Select(g => new GrafikVeri
                {
                    Etiket = g.Key,
                    Deger = g.Count(),
                    Renk = GetDurumRengi(g.Key)
                })
                .ToList();

            return durumlar;
        }

        private async Task<List<GrafikVeri>> GetAylikDavaIstatistikleriAsync(int sirketId)
        {
            var baslangicTarihi = DateTime.Now.AddMonths(-11);
            var davalar = await _unitOfWork.Davalar
                .Where(d => d.DavaSirketleri.Any(ds => ds.SirketId == sirketId) && 
                       d.CreatedDate >= baslangicTarihi)
                .ToListAsync();

            var aylikIstatistikler = Enumerable.Range(0, 12)
                .Select(i => baslangicTarihi.AddMonths(i))
                .Select(tarih => new GrafikVeri
                {
                    Etiket = tarih.ToString("MMMM yyyy"),
                    Deger = davalar.Count(d => d.CreatedDate.Month == tarih.Month && 
                                             d.CreatedDate.Year == tarih.Year),
                    Renk = "#3498db"
                })
                .ToList();

            return aylikIstatistikler;
        }

        private async Task<List<GrafikVeri>> GetAylikIcraIstatistikleriAsync(int sirketId)
        {
            var baslangicTarihi = DateTime.Now.AddMonths(-11);
            var icralar = await _unitOfWork.Icralar
                .Where(i => i.IcraSirketleri.Any(ic => ic.SirketId == sirketId) && 
                       i.CreatedDate >= baslangicTarihi)
                .ToListAsync();

            var aylikIstatistikler = Enumerable.Range(0, 12)
                .Select(i => baslangicTarihi.AddMonths(i))
                .Select(tarih => new GrafikVeri
                {
                    Etiket = tarih.ToString("MMMM yyyy"),
                    Deger = icralar.Count(i => i.CreatedDate.Month == tarih.Month && 
                                             i.CreatedDate.Year == tarih.Year),
                    Renk = "#2ecc71"
                })
                .ToList();

            return aylikIstatistikler;
        }

        private string GetDurumRengi(string durumAdi)
        {
            return durumAdi.ToUpper() switch
            {
                "DERDEST" => "#3498db",       // Mavi
                "KARAR" => "#2ecc71",         // Yeşil
                "İSTİNAF" => "#e74c3c",       // Kırmızı
                "TEMYİZ" => "#f1c40f",        // Sarı
                "HİTAM" => "#95a5a6",         // Gri
                "İNFAZ" => "#9b59b6",         // Mor
                "İMHA" => "#34495e",          // Koyu Gri
                "DÜŞME" => "#e67e22",         // Turuncu
                _ => "#bdc3c7"                // Varsayılan Gri
            };
        }

        // Diğer yardımcı metodlar...
    }
} 