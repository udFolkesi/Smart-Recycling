using CORE.Abstractions;
using System;
using System.Collections.Generic;
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

        public required RecyclingPoint RecyclingPoint { get; set; }
    }
}
