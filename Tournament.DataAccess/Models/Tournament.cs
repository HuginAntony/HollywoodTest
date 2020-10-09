using System.Collections.Generic;

namespace Tournament.DataAccess.Models
{
    public class Tournament
    {
        public long TournamentId { get; set; }
        public string TournamentName { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}