namespace TicketApplication.Services.Dtos
{
    public class GhiseuWithBonuriDto : GhiseuDto
    {
        public ICollection<BonDto> Bonuri { get; set; } = new List<BonDto>();
    }
}
