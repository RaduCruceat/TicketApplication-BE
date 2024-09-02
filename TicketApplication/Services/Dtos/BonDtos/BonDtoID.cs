using TicketApplication.Common;

namespace TicketApplication.Services.Dtos.BonDtos
{
    public class BonDtoID
    {
        public int Id { get; set; }
        public int IdGhiseu { get; set; }
        public StareEnum Stare { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
