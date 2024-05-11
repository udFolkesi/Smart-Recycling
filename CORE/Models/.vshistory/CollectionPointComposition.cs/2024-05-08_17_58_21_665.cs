using CORE.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class CollectionPointComposition
    {
        //[Key]
        [ForeignKey(nameof(Id))]
        public int Id { get; set; }
        public required string TrashType { get; set; }
        public int Weight { get; set; }
        public int MaxVolume { get; set; }

        public CollectionPoint CollectionPoint { get; set; }
    }
}
