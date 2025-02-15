using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class Sorumlu : BaseEntity
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        
        // Navigation Properties
        public ICollection<Dava> Davalar { get; set; }
        public ICollection<Icra> Icralar { get; set; }
    }
} 