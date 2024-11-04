using TicketApplication.Data.Entities;

namespace TicketApplication.Data.EFRepositories
{
    public interface IGhiseuRepositoryEF
    {
        Task<Ghiseu?> GetGhiseuById(int id);
        Task<IEnumerable<Ghiseu>> GetAllGhiseu();
        Task<Ghiseu> AddGhiseu(Ghiseu ghiseu);
        Task EditGhiseu(Ghiseu ghiseu);
        Task MarkAsActive(int id);
        Task MarkAsInactive(int id);
        Task DeleteGhiseu(int id);
    }
}
