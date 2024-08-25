using TicketApplication.Data.Repositories;
using TicketApplication.Services.Dtos;
using TicketApplication.Validators.GhiseuValidators;
using FluentValidation;
using TicketApplication.Data.Entities;

namespace TicketApplication.Services
{
    public class GhiseuService : IGhiseuService
    {
        private readonly IGhiseuRepository _ghiseuRepository;
        private readonly GhiseuIdValidator _ghiseuIdValidator;
        private readonly CreateGhiseuValidator _createGhiseuValidator;
        private readonly UpdateGhiseuValidator _updateGhiseuValidator;
        private readonly DeleteGhiseuValidator _deleteGhiseuValidator;
        private readonly ActiveGhiseuValidator _activeGhiseuValidator;

        public GhiseuService(IGhiseuRepository ghiseuRepository, GhiseuIdValidator ghiseuIdValidator, CreateGhiseuValidator createGhiseuValidator, UpdateGhiseuValidator updateGhiseuValidator, DeleteGhiseuValidator deleteGhiseuValidator, ActiveGhiseuValidator activeGhiseuValidator)
        {
            _ghiseuRepository = ghiseuRepository;
            _ghiseuIdValidator = ghiseuIdValidator;
            _createGhiseuValidator = createGhiseuValidator;
            _updateGhiseuValidator = updateGhiseuValidator;
            _deleteGhiseuValidator = deleteGhiseuValidator;
            _activeGhiseuValidator = activeGhiseuValidator;
        }

        public async Task<GhiseuDto> AddGhiseu(GhiseuDto ghiseuDto)
        {
            var validationResult = _createGhiseuValidator.Validate(ghiseuDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var ghiseu = MapGhiseuDtoToGhiseu(ghiseuDto);
            var addedGhiseu = await _ghiseuRepository.AddGhiseu(ghiseu);
            return MapGhiseuToGhiseuDto(addedGhiseu);
        }

        public async Task<GhiseuDto> GetGhiseuById(int ghiseuId)
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

            return MapGhiseuToGhiseuDto(ghiseu);
        }

        public async Task<IEnumerable<GhiseuDto>> GetAllGhiseu()
        {
            var allGhiseu = await _ghiseuRepository.GetAllGhiseu();
            return allGhiseu.Select(ghiseu => MapGhiseuToGhiseuDto(ghiseu)).ToList();
        }

        public async Task<GhiseuDto> EditGhiseu(int ghiseuId, GhiseuDto ghiseuDto)
        {
            var validationResult = _updateGhiseuValidator.Validate((ghiseuId, ghiseuDto));
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingGhiseu = await _ghiseuRepository.GetGhiseuById(ghiseuId);
            if (existingGhiseu == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {ghiseuId} not found.");
            }

            var ghiseuNewInfo = MapGhiseuDtoToGhiseu(ghiseuDto);
            ghiseuNewInfo.Id = ghiseuId;

            await _ghiseuRepository.EditGhiseu(ghiseuNewInfo);
            return MapGhiseuToGhiseuDto(ghiseuNewInfo);
        }

        public async Task<GhiseuDto> MarkAsActive(int ghiseuId)
        {
            var idValidationResult = _ghiseuIdValidator.Validate(ghiseuId);
            if (!idValidationResult.IsValid)
            {
                throw new ValidationException(idValidationResult.Errors);
            }
            var ghiseu = await _ghiseuRepository.GetGhiseuById(ghiseuId);
            if (ghiseu == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {ghiseuId} not found.");
            }

            await _ghiseuRepository.MarkAsActive(ghiseuId);
            return MapGhiseuToGhiseuDto(ghiseu);
        }


        public async Task<GhiseuDto> MarkAsInactive(int ghiseuId)
        {
            var idValidationResult = _ghiseuIdValidator.Validate(ghiseuId);
            if (!idValidationResult.IsValid)
            {
                throw new ValidationException(idValidationResult.Errors);
            }
            var ghiseu = await _ghiseuRepository.GetGhiseuById(ghiseuId);
            if (ghiseu == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {ghiseuId} not found.");
            }

            await _ghiseuRepository.MarkAsInactive(ghiseuId);
            return MapGhiseuToGhiseuDto(ghiseu);
        }

        public async Task<GhiseuDto> DeleteGhiseu(int ghiseuId)
        {
            var validationResult = _deleteGhiseuValidator.Validate(ghiseuId);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var ghiseu= await _ghiseuRepository.GetGhiseuById(ghiseuId);
            if (ghiseu == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {ghiseuId} not found.");
            }


            await _ghiseuRepository.DeleteGhiseu(ghiseuId);
            return MapGhiseuToGhiseuDto(ghiseu);
        }

        // Mapping from DTO to Entity
        private Ghiseu MapGhiseuDtoToGhiseu(GhiseuDto ghiseuDto)
        {
            return new Ghiseu
            {
                Cod = ghiseuDto.Cod,
                Denumire = ghiseuDto.Denumire,
                Descriere = ghiseuDto.Descriere,
                Icon = ghiseuDto.Icon,
                Activ = ghiseuDto.Activ
            };
        }

        // Mapping from Entity to DTO
        private GhiseuDto MapGhiseuToGhiseuDto(Ghiseu ghiseu)
        {
            return new GhiseuDto
            {
                Cod = ghiseu.Cod,
                Denumire = ghiseu.Denumire,
                Descriere = ghiseu.Descriere,
                Icon = ghiseu.Icon,
                Activ = ghiseu.Activ
            };
        }
    }
}