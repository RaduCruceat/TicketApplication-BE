﻿using FluentValidation;
using TicketApplication.Data.Entities;
using TicketApplication.Data.Repositories;
using TicketApplication.Services.Dtos;
using TicketApplication.Validators.GhiseuValidators;

namespace TicketApplication.Services
{
    public class GhiseuService : IGhiseuService
    {
        private readonly IGhiseuRepository _ghiseuRepository;
        private readonly GhiseuIdValidator _ghiseuIdValidator;
        private readonly AddGhiseuValidator _addGhiseuValidator;
        private readonly EditGhiseuValidator _editGhiseuValidator;
        private readonly DeleteGhiseuValidator _deleteGhiseuValidator;
        private readonly ActiveGhiseuValidator _activeGhiseuValidator;

        public GhiseuService(IGhiseuRepository ghiseuRepository, GhiseuIdValidator ghiseuIdValidator, AddGhiseuValidator addGhiseuValidator, EditGhiseuValidator editGhiseuValidator, DeleteGhiseuValidator deleteGhiseuValidator, ActiveGhiseuValidator activeGhiseuValidator)
        {
            _ghiseuRepository = ghiseuRepository;
            _ghiseuIdValidator = ghiseuIdValidator;
            _addGhiseuValidator = addGhiseuValidator;
            _editGhiseuValidator = editGhiseuValidator;
            _deleteGhiseuValidator = deleteGhiseuValidator;
            _activeGhiseuValidator = activeGhiseuValidator;
        }

        public async Task<GhiseuDto> AddGhiseu(GhiseuDto ghiseuDto)
        {
            var validationResult = _addGhiseuValidator.Validate(ghiseuDto);
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

        public async Task<EditGhiseuDto> EditGhiseu(int ghiseuId, EditGhiseuDto editGhiseuDto)
        {
            var validationResult = _editGhiseuValidator.Validate((ghiseuId, editGhiseuDto));
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingGhiseu = await _ghiseuRepository.GetGhiseuById(ghiseuId);
            if (existingGhiseu == null)
            {
                throw new KeyNotFoundException($"Ghiseu with ID {ghiseuId} not found.");
            }

            var ghiseuNewInfo = MapEditGhiseuDtoToGhiseu(editGhiseuDto);
            ghiseuNewInfo.Id = ghiseuId;

            await _ghiseuRepository.EditGhiseu(ghiseuNewInfo);
            return MapGhiseuToEditGhiseuDto(ghiseuNewInfo);
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
            var ghiseu = await _ghiseuRepository.GetGhiseuById(ghiseuId);
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

        private Ghiseu MapEditGhiseuDtoToGhiseu(EditGhiseuDto editGhiseuDto)
        {
            return new Ghiseu
            {
                Cod = editGhiseuDto.Cod,
                Denumire = editGhiseuDto.Denumire,
                Descriere = editGhiseuDto.Descriere,
                Icon = editGhiseuDto.Icon,     
            };
        }

        // Mapping from Entity to DTO
        private EditGhiseuDto MapGhiseuToEditGhiseuDto(Ghiseu ghiseu)
        {
            return new EditGhiseuDto
            {
                Cod = ghiseu.Cod,
                Denumire = ghiseu.Denumire,
                Descriere = ghiseu.Descriere,
                Icon = ghiseu.Icon,
            };
        }
    }
}