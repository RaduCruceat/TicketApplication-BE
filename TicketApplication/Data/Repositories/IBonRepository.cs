using TicketApplication.Data.Entities;

namespace TicketApplication.Data.Repositories
{
    public interface IBonRepository
    {
        Task<Bon> AddBon(Bon bon);
        Task MarkAsInProgress(int id);
        Task MarkAsReceived(int id);
        Task MarkAsClose(int id);
    }
}
