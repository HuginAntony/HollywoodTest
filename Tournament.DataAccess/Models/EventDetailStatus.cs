using System.Collections.Generic;

namespace Tournament.DataAccess.Models
{
    public class EventDetailStatus
    {
        public long EventDetailStatusId { get; set; }
        public EventDetailStatusNames EventDetailStatusName { get; set; }

        public virtual ICollection<EventDetail> EventDetails { get; set; }
    }
}