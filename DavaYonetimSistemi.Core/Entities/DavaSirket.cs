using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class DavaSirket : BaseEntity
    {
        public int DavaId { get; set; }
        public int SirketId { get; set; }
        public bool IsDavaEden { get; set; }
        
        // Navigation Properties
        public Dava Dava { get; set; }
        public Sirket Sirket { get; set; }
    }
} 