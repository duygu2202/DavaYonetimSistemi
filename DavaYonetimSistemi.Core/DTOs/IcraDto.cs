namespace DavaYonetimDB.Core.DTOs
{
    public class IcraDto
    {
        public int Id { get; set; }
        public DateTime TakipT { get; set; }
        public string Durum { get; set; }
        public string FormTipi { get; set; }
        public decimal AsilAlacak { get; set; }
        public decimal TakipCikisi { get; set; }
        public decimal Kalan { get; set; }
        public decimal ToplamOdeme { get; set; }
        public List<string> Alacaklilar { get; set; }
        public List<string> Borclular { get; set; }
        public string Sorumlu { get; set; }
        public string Konusu { get; set; }
        public DateTime? InfazT { get; set; }
        public DateTime? KapamaT { get; set; }
    }
} 