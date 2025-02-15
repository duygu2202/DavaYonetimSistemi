public class IcraSirket
{
    public int IcraId { get; set; }
    public int SirketId { get; set; }
    public bool IsAlacakli { get; set; }
    
    public Icra Icra { get; set; }
    public Sirket Sirket { get; set; }
} 