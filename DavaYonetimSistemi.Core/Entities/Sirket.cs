using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class Sirket : BaseEntity
    {
        public string Name { get; set; }
        
        // Navigation Properties
        public ICollection<DavaSirket> DavaSirketleri { get; set; }
        public ICollection<IcraSirket> IcraSirketleri { get; set; }
    }
} 