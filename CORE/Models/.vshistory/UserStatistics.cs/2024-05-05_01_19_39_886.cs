﻿using CORE.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class UserStatistics
    {
        [Key]
        [ForeignKey("Id")]
        public int Id { get; set; }
        public int Bonuses { get; set; }
        public int Recycled { get; set; }
        // Collection Points

        public required User User { get; set; }
    }
}
