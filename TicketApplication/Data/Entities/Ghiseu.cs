namespace TicketApplication.Data.Entities
{
    public class Ghiseu
    {
        public int Id { get; set; }
        public required string  Cod { get; set; }
        public required string Denumire { get; set; }
        public required string Descriere { get; set; }
        public required string Icon { get; set; }
        public required bool Activ { get; set; }

        // Navigation property
        public ICollection<Bon>? Bonuri { get; set; }
    }
}
