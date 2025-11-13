using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikacjaDoObsady.Models
{
    public class MatchAssignment
    {
        [Key]
        public int MatchAssignmentId { get; set; }

        // --- Relacja do Match ---
        [Required(ErrorMessage = "Mecz jest wymagany.")]
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        [ValidateNever]
        public required Match Match { get; set; }

        // --- Relacja do Sędziego Głównego ---
        [Required(ErrorMessage = "Sędzia Główny jest wymagany.")]
        public int MainRefereeId { get; set; } // Zmieniono nazwę na bardziej precyzyjną dla klucza obcego
        [ForeignKey("MainRefereeId")]
        [ValidateNever]
        public required Referee MainReferee { get; set; } // Właściwość nawigacyjna

        // --- Relacja do Asystenta Sędziego 1 ---
        [Required(ErrorMessage = "Sędzia Asystent I jest wymagany.")]
        public int AssistantReferee1Id { get; set; } // Zmieniono nazwę
        [ForeignKey("AssistantReferee1Id")]
        [ValidateNever]
        public required Referee AssistantReferee1 { get; set; } // Właściwość nawigacyjna

        // --- Relacja do Asystenta Sędziego 2 ---
        [Required(ErrorMessage = "Sędzia Asystent II jest wymagany.")]
        public int AssistantReferee2Id { get; set; } // Zmieniono nazwę
        [ForeignKey("AssistantReferee2Id")]
        [ValidateNever]
        public required Referee AssistantReferee2 { get; set; } // Właściwość nawigacyjna

        // --- Relacja do Czwartego Sędziego (opcjonalna) ---
        public int? FourthOfficialId { get; set; } // Typ nullable int dla opcjonalnego klucza obcego
        [ForeignKey("FourthOfficialId")]
        [ValidateNever]
        public Referee? FourthOfficial { get; set; } // Typ nullable Referee dla opcjonalnego obiektu

        // --- Status meczu ---
        [Required(ErrorMessage = "Status jest wymagany.")]
        public MatchStatus Status { get; set; } = MatchStatus.Scheduled; // Używamy enum zdefiniowanego poniżej
    }
}