using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class IcraSirket : BaseEntity
    {
        public int IcraId { get; set; }
        public int SirketId { get; set; }
        public bool IsAlacakli { get; set; }
        
        // Navigation Properties
        public Icra Icra { get; set; }
        public Sirket Sirket { get; set; }
    }
} 