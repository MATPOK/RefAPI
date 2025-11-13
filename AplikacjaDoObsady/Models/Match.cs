using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikacjaDoObsady.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public DateTime MatchDate { get; set; }
        public string LeagueName { get; set; } = string.Empty;

        [NotMapped]
        public string MatchTitle => $"{HomeTeam} - {AwayTeam}";
    }
}
