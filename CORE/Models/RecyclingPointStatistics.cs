﻿using CORE.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class RecyclingPointStatistics: BaseEntity
    {
        public int Collected { get; set; }
        public int Recycled { get; set; }
        public required string Period { get; set; }
        [ForeignKey(nameof(RecyclingPointID))]
        public int RecyclingPointID { get; set; }

        public RecyclingPoint RecyclingPoint { get; set; }
    }
}
