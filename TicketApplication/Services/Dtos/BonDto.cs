using TicketApplication.Common;

namespace TicketApplication.Services.Dtos
{
    public class BonDto
    {
        public int IdGhiseu { get; set; }
        public  StareEnum Stare { get; set; }
        public  DateTime CreatedAt { get; set; }
        public  DateTime ModifiedAt { get; set; }
    }
   
}
