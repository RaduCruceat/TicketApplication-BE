using TicketApplication.Services.Dtos;

namespace TicketApplication.Services
{
    public interface IGhiseuService
    {
        Task<GhiseuDto> AddGhiseu(GhiseuDto ghiseuDto);
        Task<GhiseuDto> GetGhiseuById(int ghiseuId);
        Task<IEnumerable<GhiseuDto>> GetAllGhiseu();
        Task<GhiseuDto> EditGhiseu(int ghiseuId, GhiseuDto ghiseuDto);
        Task<GhiseuDto> MarkAsActive(int ghiseuId);
        Task<GhiseuDto> MarkAsInactive(int ghiseuId);
        Task<GhiseuDto> DeleteGhiseu(int ghiseuId);
    }
}
