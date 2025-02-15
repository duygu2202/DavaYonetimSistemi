public class Sirket
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation properties
    public ICollection<IcraSirket> IcraAlacakli { get; set; }
    public ICollection<IcraSirket> IcraBorclu { get; set; }
    public ICollection<DavaSirket> DavaEden { get; set; }
    public ICollection<DavaSirket> DavaEdilen { get; set; }
} 