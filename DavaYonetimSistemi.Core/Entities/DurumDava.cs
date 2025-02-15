using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class DurumDava : BaseEntity
    {
        public string Name { get; set; }
        
        // Navigation Properties
        public ICollection<Dava> Davalar { get; set; }
    }
} 