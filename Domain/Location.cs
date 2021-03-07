using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Location
    {
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        public int Id { get; set; }
        
        public ICollection<Item>? Items { get; set; }
    }
}