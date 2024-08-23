namespace TicketApplication.Data.Entities
{
    public class Bon
    {
        public int Id { get; set; }
        public int IdGhiseu { get; set; }
        public StareEnum Stare { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        // Navigation property
        public Ghiseu Ghiseu { get; set; }
    }
    public enum StareEnum
    {
        InCursDePreluare,
        Preluat,
        Inchis
    }
}
