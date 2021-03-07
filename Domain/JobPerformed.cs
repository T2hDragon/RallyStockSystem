namespace Domain
{
    public class JobPerformed
    {
        public int Id { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; } = default!;

        public int RallyId { get; set; }
        public Rally Rally { get; set; } = default!;
    }
}