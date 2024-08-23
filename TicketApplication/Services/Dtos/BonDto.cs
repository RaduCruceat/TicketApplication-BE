namespace TicketApplication.Services.Dtos
{
    public class BonDto
    {
        public int Id { get; set; }
        public int IdGhiseu { get; set; }
        public required string Stare { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
