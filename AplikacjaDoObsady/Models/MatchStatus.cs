namespace AplikacjaDoObsady.Models
{
    public enum MatchStatus
    {
        Scheduled,       // Zaplanowany
        Completed,       // Ukończony
        Canceled,        // Odwołany (poprawiono literówkę z Canceled)
        Interrupted,     // Przerwany (poprawiono literówkę z Interapted)
        Postponed        // Przełożony (opcjonalnie, jeśli chcesz mieć taką opcję)
    }
}