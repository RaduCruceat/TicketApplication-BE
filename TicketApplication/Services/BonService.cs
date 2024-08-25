using TicketApplication.Data.Entities;
using TicketApplication.Data.Repositories;
using TicketApplication.Services.Dtos;
using TicketApplication.Validators.BonValidators;
using FluentValidation;

namespace TicketApplication.Services
{
    public class BonService : IBonService
    {
        private readonly IBonRepository _bonRepository;
        private readonly IGhiseuRepository _ghiseuRepository;
        private readonly BonIdValidator _bonIdValidator;
        private readonly CreateBonValidator _createBonValidator;
        private readonly UpdateBonStatusValidator _updateBonStatusValidator;

        public BonService(IGhiseuRepository ghiseuRepository,IBonRepository bonRepository, BonIdValidator bonIdValidator, CreateBonValidator createBonValidator, UpdateBonStatusValidator updateBonStatusValidator)
        {
            _bonRepository = bonRepository;
            _bonIdValidator = bonIdValidator;
            _createBonValidator = createBonValidator;
            _updateBonStatusValidator = updateBonStatusValidator;
            _ghiseuRepository = ghiseuRepository;
        }

        public async Task<BonDto> AddBon(BonDto bonDto)
        {
            var ghiseuExists = await _ghiseuRepository.GetGhiseuById(bonDto.IdGhiseu);
            if (ghiseuExists == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {bonDto.IdGhiseu} not found.");
            }
            var validationResult = _createBonValidator.Validate(bonDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var bon = MapBonDtoToBon(bonDto);
            var addedBon = await _bonRepository.AddBon(bon);
            return MapBonToBonDto(addedBon);
        }

        public async Task<BonDto> GetBonById(int bonId)
        {
            var validationResult = _bonIdValidator.Validate(bonId);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var bon = await _bonRepository.GetBonById(bonId);
            if (bon == null)
            {
                throw new KeyNotFoundException($"Bon with ID {bonId} not found.");
            }

            return MapBonToBonDto(bon);
        }

        public async Task<IEnumerable<BonDto>> GetAllBon()
        {
            var bonuri = await _bonRepository.GetAllBon();
            return bonuri.Select(bon => MapBonToBonDto(bon)).ToList();
        }

        public async Task<BonDto> MarkAsInProgress(int bonId)
        {
            var validationResult = _bonIdValidator.Validate(bonId);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _bonRepository.MarkAsInProgress(bonId);
            var updatedBon = await _bonRepository.GetBonById(bonId);
            if (updatedBon == null)
            {
                throw new KeyNotFoundException($"Bon with ID {bonId} not found after marking as closed.");
            }
            return MapBonToBonDto(updatedBon);
        }

        public async Task<BonDto> MarkAsReceived(int bonId)
        {
            var validationResult = _bonIdValidator.Validate(bonId);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _bonRepository.MarkAsReceived(bonId);
            var updatedBon = await _bonRepository.GetBonById(bonId);
            if (updatedBon == null)
            {
                throw new KeyNotFoundException($"Bon with ID {bonId} not found after marking as closed.");
            }
            return MapBonToBonDto(updatedBon);
        }

        public async Task<BonDto> MarkAsClosed(int bonId)
        {
            var validationResult = _bonIdValidator.Validate(bonId);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _bonRepository.MarkAsClose(bonId);
            var updatedBon = await _bonRepository.GetBonById(bonId);
            if (updatedBon == null)
            {
                throw new KeyNotFoundException($"Bon with ID {bonId} not found after marking as closed.");
            }

            return MapBonToBonDto(updatedBon);
        }

        private Bon MapBonDtoToBon(BonDto bonDto)
        {      
            return new Bon
            {
                IdGhiseu = bonDto.IdGhiseu,
                Stare = bonDto.Stare,
                CreatedAt = bonDto.CreatedAt,
                ModifiedAt = bonDto.ModifiedAt, 
            };
        }

        private BonDto MapBonToBonDto(Bon bon)
        {
            return new BonDto
            {
                IdGhiseu = bon.IdGhiseu,
                Stare = bon.Stare,
                CreatedAt = bon.CreatedAt,
                ModifiedAt = bon.ModifiedAt
            };
        }
    }
}
