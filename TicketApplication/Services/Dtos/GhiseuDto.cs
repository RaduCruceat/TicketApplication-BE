namespace TicketApplication.Services.Dtos
{
    public class GhiseuDto
    {
        public int Id { get; set; }
        public required string Cod { get; set; }
        public required string Denumire { get; set; }
        public string? Descriere { get; set; }
        public string? Icon { get; set; }
        public bool Activ { get; set; }
    }
}
