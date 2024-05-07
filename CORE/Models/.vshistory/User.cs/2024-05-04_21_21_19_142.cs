﻿using CORE.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class User: BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public bool IsAdmin { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int UserStatisticsID { get; set; }

        public UserStatistics? UserStatistics { get; set; }
    }
}
