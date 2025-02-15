using System;
using System.Collections.Generic;
using DavaYonetimDB.Core.Entities.Base;

namespace DavaYonetimDB.Core.Entities
{
    public class DurumIcra : BaseEntity
    {
        public string Name { get; set; }
        
        // Navigation Properties
        public ICollection<Icra> Icralar { get; set; }
    }
} 