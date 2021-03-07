namespace Domain
{
    public class JobItem
    {
        public int Id { get; set; }

        public int ItemQuantity { get; set; }

        public int JobId { get; set; }
        public Job Job { get; set; } = default!;

        public int ItemId { get; set; }
        public Item Item { get; set; } = default!;
    }
}