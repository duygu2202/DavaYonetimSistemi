public class DataTableParameters
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public Search Search { get; set; }
    public List<Order> Order { get; set; }
    public List<Column> Columns { get; set; }

    // Filtreleme parametreleri
    public string UyapBirimi { get; set; }
    public string EsasNo { get; set; }
    public string Sorumlu { get; set; }
    public DateTime? BaslangicTarihi { get; set; }
    public DateTime? BitisTarihi { get; set; }
    public string DavaEdenler { get; set; }
    public string DavaEdilenler { get; set; }
    public string Alacaklilar { get; set; }
    public string Borclular { get; set; }
}

public class Search
{
    public string Value { get; set; }
    public bool Regex { get; set; }
}

public class Order
{
    public int Column { get; set; }
    public string Dir { get; set; }
}

public class Column
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public Search Search { get; set; }
} 