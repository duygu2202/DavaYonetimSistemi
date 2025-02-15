namespace DavaYonetimDB.Core.DTOs
{
    public class DavaDto
    {
        public int Id { get; set; }
        public string UyapBirim { get; set; }
        public string EsasNo { get; set; }
        public string OfisNo { get; set; }
        public string EskiRafNo { get; set; }
        public string Durum { get; set; }
        public DateTime DavaT { get; set; }
        public string DavaKonusu { get; set; }
        public decimal DavaDegeri { get; set; }
        public string Sorumlu { get; set; }
        public List<string> DavaEdenler { get; set; }
        public List<string> DavaEdilenler { get; set; }
        public string Asama { get; set; }
        public string AltAsama { get; set; }
        public DateTime KayitT { get; set; }
        public DateTime? ArsivT { get; set; }
    }
} 