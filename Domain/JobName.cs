using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class JobName
    {
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        public int Id { get; set; }
    }
}