using FluentValidation;
using TicketApplication.Data.Entities;
using TicketApplication.Data.EFRepositories;
using TicketApplication.Services.Dtos.BonDtos ;
using TicketApplication.Services.Dtos.GhiseuDtos;
using TicketApplication.Validators.BonValidators;
using TicketApplication.Validators.GhiseuValidators;

namespace TicketApplication.Services
{
    public class BonService : IBonService
    {
        private readonly IBonRepositoryEF _bonRepository;
        private readonly IGhiseuRepositoryEF _ghiseuRepository;
        private readonly GhiseuIdValidator _ghiseuIdValidator;
        private readonly BonIdValidator _bonIdValidator;
        private readonly AddBonValidator _addBonValidator;
        private readonly EditBonStatusValidator _editBonStatusValidator;

        public BonService(IGhiseuRepositoryEF ghiseuRepository, IBonRepositoryEF bonRepository, BonIdValidator bonIdValidator, AddBonValidator addBonValidator, EditBonStatusValidator editBonStatusValidator, GhiseuIdValidator ghiseuIdValidator)
        {
            _bonRepository = bonRepository;
            _bonIdValidator = bonIdValidator;
            _addBonValidator = addBonValidator;
            _editBonStatusValidator = editBonStatusValidator;
            _ghiseuRepository = ghiseuRepository;
            _ghiseuIdValidator = ghiseuIdValidator;
        }

        public async Task<BonDto> AddBon(BonDto bonDto)
        {
            var ghiseuExists = await _ghiseuRepository.GetGhiseuById(bonDto.IdGhiseu);
            if (ghiseuExists == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {bonDto.IdGhiseu} not found.");
            }
            var validationResult = _addBonValidator.Validate(bonDto);
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
        public async Task<IEnumerable<BonDtoID>> GetAllBonByGhiseuId(int ghiseuId)
        {
            var validationResult = _ghiseuIdValidator.Validate(ghiseuId);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var ghiseu = await _ghiseuRepository.GetGhiseuById(ghiseuId);
            if (ghiseu == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {ghiseuId} not found.");
            }
            var bons = await _bonRepository.GetAllBonByGhiseuId(ghiseuId);
            if (bons == null)
            {
                throw new KeyNotFoundException($"Bons with ghiseuId {ghiseuId} not found.");
            }
            return bons.Select(bon => MapBonToBonDtoID(bon)).ToList();
        }


        public async Task<IEnumerable<BonDtoID>> GetAllBon()
        {
            var bonuri = await _bonRepository.GetAllBon();
            return bonuri.Select(bon => MapBonToBonDtoID(bon)).ToList();
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

        //with id
        private Bon MapBonDtoIDToBon(BonDtoID bonDto)
        {
            return new Bon
            {
                Id = bonDto.Id,
                IdGhiseu = bonDto.IdGhiseu,
                Stare = bonDto.Stare,
                CreatedAt = bonDto.CreatedAt,
                ModifiedAt = bonDto.ModifiedAt,
            };
        }

        private BonDtoID MapBonToBonDtoID(Bon bon)
        {
            return new BonDtoID
            {
                Id = bon.Id,
                IdGhiseu = bon.IdGhiseu,
                Stare = bon.Stare,
                CreatedAt = bon.CreatedAt,
                ModifiedAt = bon.ModifiedAt
            };
        }


    }
}
