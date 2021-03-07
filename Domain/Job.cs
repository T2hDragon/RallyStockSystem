using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Job
    {
        public int Id { get; set; }

        public int JobNameId { get; set; }
        public JobName JobName { get; set; } = default!;
        
        public ICollection<JobItem> JobItems { get; set; } = default!;

        public ICollection<JobPerformed>? JobsPerformed { get; set; }

        public int RallyId { get; set; }
        public Rally Rally { get; set; } = default!;

    }
}