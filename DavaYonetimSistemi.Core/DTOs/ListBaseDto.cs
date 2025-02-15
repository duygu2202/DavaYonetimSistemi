public class ListBaseDto
{
    public string UyapBirimi { get; set; }
    public string EsasNo { get; set; }
    public string OfisNo { get; set; }
    public string EskiEsasNo { get; set; }
    public string Durum { get; set; }
    public DateTime? KayitTarihi { get; set; }
    public DateTime? ArsivTarihi { get; set; }
    public string Sorumlu { get; set; }
}

public class DavaListDto : ListBaseDto
{
    public string DavaKonusu { get; set; }
    public decimal DavaDegeri { get; set; }
    public List<string> DavaEdenler { get; set; }
    public List<string> DavaEdilenler { get; set; }
    public string Asama { get; set; }
    public string AltAsama { get; set; }
}

public class IcraListDto : ListBaseDto
{
    public List<string> Alacaklilar { get; set; }
    public List<string> Borclular { get; set; }
    public decimal TakipDegeri { get; set; }
    public decimal Tahsilat { get; set; }
    public decimal Bakiye { get; set; }
} 