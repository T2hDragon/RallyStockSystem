using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Item
    {
        public int Id { get; set; }
        
        public int CurrentQuantity { get; set; }

        public int OptimalQuantity { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int ItemNameId { get; set; }
        public ItemName ItemName { get; set; } = default!;
        
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        
        public int LocationId { get; set; }
        public Location Location { get; set; } = default!;
        
        public ICollection<JobItem>? JobItems { get; set; }

        public int RallyId { get; set; }
        public Rally Rally { get; set; } = default!;
    }
}