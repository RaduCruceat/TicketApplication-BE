using TicketApplication.Services.Dtos.BonDtos;

namespace TicketApplication.Services
{
    public interface IBonService
    {
        Task<BonDto> AddBon(BonDto bonDto);
        Task<BonDto> GetBonById(int bonId);
        Task<IEnumerable<BonDtoID>> GetAllBonByGhiseuId(int ghiseuId);
        Task<IEnumerable<BonDtoID>> GetAllBon();
        Task<BonDto> MarkAsInProgress(int bonId);
        Task<BonDto> MarkAsReceived(int bonId);
        Task<BonDto> MarkAsClosed(int bonId);
    }
}
