namespace Tournament.Core.Models
{
    public class TournamentRequest
    {
        public string TournamentName { get; set; }
    }

    public class TournamentResponse
    {
        public long TournamentId { get; set; }
        public string TournamentName { get; set; }
    }
}
