namespace Tournament.Core.Models
{
    public class EventDetailResponse
    {
        public long EventDetailId { get; set; }
        public long EventId { get; set; }
        public long EventDetailStatusId { get; set; }
        public string EventDetailName { get; set; }
        public short EventDetailNumber { get; set; }
        public decimal EventDetailOdd { get; set; }
        public short FinishingPosition { get; set; }
        public bool FirstTimer { get; set; }
    }
}