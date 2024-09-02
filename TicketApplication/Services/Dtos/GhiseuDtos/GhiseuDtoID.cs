namespace TicketApplication.Services.Dtos.GhiseuDtos
{
    public class GhiseuDtoID
    {
        public int Id { get; set; }
        public required string Cod { get; set; }
        public required string Denumire { get; set; }
        public required string Descriere { get; set; }
        public required string Icon { get; set; }
        public bool Activ { get; set; }
    }
}
