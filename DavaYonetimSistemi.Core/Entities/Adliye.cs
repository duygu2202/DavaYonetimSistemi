using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class Adliye : BaseEntity
    {
        public string Name { get; set; }
        public string Sehir { get; set; }
        
        // Navigation Properties
        public ICollection<Dava> Davalar { get; set; }
        public ICollection<Icra> Icralar { get; set; }
    }
} 