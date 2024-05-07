using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class Operation
    {
        public required string TrashType { get; set; }
        public int Weight { get; set; }

        [ForeignKey(nameof(UserID))]
        public int UserID { get; set; }
        [ForeignKey(nameof(CollectionPointID))]
        public int CollectionPointID { get; set; }
        
        public required User User { get; set; }
        public required CollectionPoint CollectionPoint { get; set; }
    }
}
