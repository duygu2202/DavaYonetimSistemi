public class Dava
{
    public int Id { get; set; }
    public string UyapBirim { get; set; }
    public string EsasNo { get; set; }
    public string OfisNo { get; set; }
    public string EskiRafNo { get; set; }
    public int DurumId { get; set; }
    public DateTime DavaTarihi { get; set; }
    public string DavaKonusu { get; set; }
    public decimal DavaDegeri { get; set; }
    public string Asama { get; set; }
    public string AltAsama { get; set; }
    public DateTime KayitTarihi { get; set; }
    public DateTime? ArsivTarihi { get; set; }

    // Navigation properties
    public DurumDava Durum { get; set; }
    public ICollection<DavaSirket> DavaEdenler { get; set; }
    public ICollection<DavaSirket> DavaEdilenler { get; set; }
    public ICollection<DavaSorumlu> Sorumlular { get; set; }
} 