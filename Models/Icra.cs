public class Icra
{
    public int Id { get; set; }
    public string UyapBirim { get; set; }
    public string EsasNo { get; set; }
    public string OfisNo { get; set; }
    public string EskiRafNo { get; set; }
    public DateTime TakipTarihi { get; set; }
    public int DurumId { get; set; }
    public string FormTipi { get; set; }
    public decimal AsilAlacak { get; set; }
    public decimal TakipCikisi { get; set; }
    public decimal Kalan { get; set; }
    public decimal ToplamOdeme { get; set; }
    public DateTime? InfazTarihi { get; set; }
    public DateTime? KapamaTarihi { get; set; }

    // Navigation properties
    public DurumIcra Durum { get; set; }
    public ICollection<IcraSirket> Alacaklilar { get; set; }
    public ICollection<IcraSirket> Borclular { get; set; }
    public ICollection<IcraSorumlu> Sorumlular { get; set; }
} 