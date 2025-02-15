using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;
using DavaYonetimDB.Core.Entities.Adliye;
using DavaYonetimDB.Core.Entities.Sorumlu;
using DavaYonetimDB.Core.Entities.DurumIcra;
using DavaYonetimDB.Core.Entities.IcraSirket;

namespace DavaYonetimDB.Core.Entities.Icra
{
    public class Icra : BaseEntity
    {
        public int AdliyeId { get; set; }
        public int DurumIcraId { get; set; }
        public int SorumluId { get; set; }
        
        public DateTime TakipT { get; set; }
        public string FormTipi { get; set; }
        public decimal AsilAlacak { get; set; }
        public decimal TakipCikisi { get; set; }
        public decimal Kalan { get; set; }
        public decimal ToplamOdeme { get; set; }
        public string Konusu { get; set; }
        public DateTime? InfazT { get; set; }
        public DateTime? KapamaT { get; set; }
        
        // Navigation Properties
        public Adliye Adliye { get; set; }
        public DurumIcra DurumIcra { get; set; }
        public Sorumlu Sorumlu { get; set; }
        public ICollection<IcraSirket> IcraSirketleri { get; set; }
    }
} 