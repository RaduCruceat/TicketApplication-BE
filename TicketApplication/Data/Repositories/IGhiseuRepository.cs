using TicketApplication.Data.Entities;

namespace TicketApplication.Data.Repositories
{
    public interface IGhiseuRepository
    {
        Task<Ghiseu> AddGhiseu(Ghiseu ghiseu);
        Task EditGhiseu(Ghiseu ghiseu);
        Task MarkAsActive(int id);
        Task MarkAsInactive(int id);
        Task DeleteGhiseu(int id);
    }

}
