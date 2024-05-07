﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class RecyclingPoint
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string RecyclingType { get; set; }
        public int Workload { get; set; }
        public int RecyclingPointStatisticsID { get; set; }
        [ForeignKey(nameof(RecyclingPointStatisticsID))]

        public ICollection<Transportation> Transportation { get; set; }
        public ICollection<RecyclingPointStatistics> RecyclingPointStatistics { get; set; }
    }
}
