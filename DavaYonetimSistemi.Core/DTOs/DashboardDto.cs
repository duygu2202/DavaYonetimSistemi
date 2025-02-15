namespace DavaYonetimDB.Core.DTOs
{
    public class DashboardDto
    {
        public string SirketAdi { get; set; }
        public DashboardSayilar Sayilar { get; set; }
        public DashboardGrafikler Grafikler { get; set; }
    }

    public class DashboardSayilar
    {
        public int ToplamDavaSayisi { get; set; }
        public int ToplamIcraSayisi { get; set; }
        public decimal ToplamDavaDegeri { get; set; }
        public decimal ToplamIcraTakipDegeri { get; set; }
        public int DerdestDavaSayisi { get; set; }
        public int DerdestIcraSayisi { get; set; }
    }

    public class DashboardGrafikler
    {
        public List<GrafikVeri> DavaDurumlari { get; set; }
        public List<GrafikVeri> IcraDurumlari { get; set; }
        public List<GrafikVeri> AylikDavaIstatistikleri { get; set; }
        public List<GrafikVeri> AylikIcraIstatistikleri { get; set; }
    }

    public class GrafikVeri
    {
        public string Etiket { get; set; }
        public int Deger { get; set; }
        public string Renk { get; set; }
    }
} 