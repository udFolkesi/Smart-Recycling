﻿using CORE.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class CollectionPoint: BaseEntity
    {
        public required string Location { get; set; }
        //public int Volume { get; set; }
        public required string Status { get; set; }
        public int Fullness { get; set; }

        public CollectionPointComposition CollectionPointComposition { get; set; } //required
        public ICollection<Operation> Operations { get; set; }
        public ICollection<Transportation> Transportations { get; set; }
        public ICollection<CollectionPointStatistics> CollectionPointStatistics { get; set; }
    }
}
