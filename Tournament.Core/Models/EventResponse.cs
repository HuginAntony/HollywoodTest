using System;

namespace Tournament.Core.Models
{
    public class EventResponse
    {
        public long EventId { get; set; }
        public long TournamentId { get; set; }
        public string EventName { get; set; }
        public short EventNumber { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public bool AutoClose { get; set; }
    }
}