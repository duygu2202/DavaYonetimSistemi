using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;
using DavaYonetimDB.Core.Entities.Adliye;
using DavaYonetimDB.Core.Entities.DurumDava;
using DavaYonetimDB.Core.Entities.Sorumlu;
using DavaYonetimDB.Core.Entities.DavaSirket;

namespace DavaYonetimDB.Core.Entities.Dava
{
    public class Dava : BaseEntity
    {
        public int AdliyeId { get; set; }
        public int DurumDavaId { get; set; }
        public int SorumluId { get; set; }
        
        public string UyapBirim { get; set; }
        public string EsasNo { get; set; }
        public string OfisNo { get; set; }
        public string EskiRafNo { get; set; }
        public DateTime DavaT { get; set; }
        public string DavaKonusu { get; set; }
        public decimal DavaDegeri { get; set; }
        public string Asama { get; set; }
        public string AltAsama { get; set; }
        public DateTime KayitT { get; set; }
        public DateTime? ArsivT { get; set; }
        
        // Navigation Properties
        public Adliye Adliye { get; set; }
        public DurumDava DurumDava { get; set; }
        public Sorumlu Sorumlu { get; set; }
        public ICollection<DavaSirket> DavaSirketleri { get; set; }
    }
} 