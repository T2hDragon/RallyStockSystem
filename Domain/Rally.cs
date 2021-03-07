using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Rally
    {
        public int Id { get; set; }
        
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        [MaxLength(64)]
        public string Location { get; set; } = default!;
        
        public ICollection<Job>? Jobs { get; set; }
        
        public ICollection<Item>? Items { get; set; }

        public ICollection<JobPerformed>? JobsPerformed { get; set; }

    }
}