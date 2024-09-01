using TicketApplication.Services.Dtos;

namespace TicketApplication.Services
{
    public interface IGhiseuService
    {
        Task<GhiseuDto> AddGhiseu(GhiseuDto ghiseuDto);
        Task<GhiseuDto> GetGhiseuById(int ghiseuId);
        Task<IEnumerable<GhiseuDto>> GetAllGhiseu();
        Task<EditGhiseuDto> EditGhiseu(int ghiseuId, EditGhiseuDto editGhiseuDto);
        Task<GhiseuDto> MarkAsActive(int ghiseuId);
        Task<GhiseuDto> MarkAsInactive(int ghiseuId);
        Task<GhiseuDto> DeleteGhiseu(int ghiseuId);
    }
}
