public class DavaSirket
{
    public int DavaId { get; set; }
    public int SirketId { get; set; }
    public bool IsDavaEden { get; set; }
    
    public Dava Dava { get; set; }
    public Sirket Sirket { get; set; }
} 