using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Data.Entities;
using TicketApplication.Services;
using TicketApplication.Services.Dtos.GhiseuDtos;
using TicketApplication.Validators.ResponseValidator;

[ApiController]
[Route("[controller]")]
public class GhiseuController : ControllerBase
{
    private readonly IGhiseuService _ghiseuService;

    public GhiseuController(IGhiseuService ghiseuService)
    {
        _ghiseuService = ghiseuService;
    }

    [HttpGet("GetIcons", Name = "GetAllIcons")]
    public ActionResult<ResponseValidator<IEnumerable<string?>>> GetIcons()
    {
        try
        {
            var iconsPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            var icons = Directory.GetFiles(iconsPath)
                                 .Select(Path.GetFileName)
                                 .ToList();

            if (!icons.Any())
            {
                return NotFound(ResponseValidator<IEnumerable<string?>>.Failure("Nu au fost găsite pictograme."));
            }
            return Ok(ResponseValidator<IEnumerable<string?>>.Success(icons));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<IEnumerable<string?>>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpGet("GetAll", Name = "GetAllGhiseu")]
    public async Task<ActionResult<ResponseValidator<IEnumerable<GhiseuDtoID>>>> GetAllGhiseu()
    {
        try
        {
            var ghiseuList = await _ghiseuService.GetAllGhiseu();
            if (ghiseuList == null)
            {
                return NotFound(ResponseValidator<GhiseuDtoID>.Failure($"Lista de ghișee este goală."));
            }
            return Ok(ResponseValidator<IEnumerable<GhiseuDtoID>>.Success(ghiseuList));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<IEnumerable<GhiseuDtoID>>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<IEnumerable<GhiseuDtoID>>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpGet("Get/{id}", Name = "GetGhiseuById")]
    public async Task<ActionResult<ResponseValidator<GhiseuDto>>> GetGhiseuById(int id)
    {
        try
        {
            var ghiseu = await _ghiseuService.GetGhiseuById(id);
            if (ghiseu == null)
            {
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghișeul cu id-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(ghiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPost("Add", Name = "AddGhiseu")]
    public async Task<ActionResult<ResponseValidator<GhiseuDto>>> AddGhiseu([FromBody] GhiseuDto ghiseu)
    {
        try
        {
            var addedGhiseu = await _ghiseuService.AddGhiseu(ghiseu);
            return Ok(ResponseValidator<GhiseuDto>.Success(addedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPut("Edit/{id}", Name = "EditGhiseu")]
    public async Task<ActionResult<ResponseValidator<EditGhiseuDto>>> EditGhiseu(int id, [FromBody] EditGhiseuDto ghiseu)
    {
        try
        {
            var updatedGhiseu = await _ghiseuService.EditGhiseu(id, ghiseu);
            if (updatedGhiseu == null)
            {
                return NotFound(ResponseValidator<EditGhiseuDto>.Failure($"Ghișeul cu id-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<EditGhiseuDto>.Success(updatedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<EditGhiseuDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<EditGhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<EditGhiseuDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPut("MarkAsActive/{id}", Name = "MarkGhiseuAsActive")]
    public async Task<ActionResult<ResponseValidator<GhiseuDto>>> MarkAsActive(int id)
    {
        try
        {
            var updatedGhiseu = await _ghiseuService.MarkAsActive(id);
            if (updatedGhiseu == null)
            {
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghișeul cu id-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(updatedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPut("MarkAsInactive/{id}", Name = "MarkGhiseuAsInactive")]
    public async Task<ActionResult<ResponseValidator<GhiseuDto>>> MarkAsInactive(int id)
    {
        try
        {
            var updatedGhiseu = await _ghiseuService.MarkAsInactive(id);
            if (updatedGhiseu == null)
            {
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghișeul cu id-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(updatedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpDelete("Delete/{id}", Name = "DeleteGhiseu")]
    public async Task<ActionResult<ResponseValidator<GhiseuDto>>> DeleteGhiseu(int id)
    {
        try
        {
            var deletedGhiseu = await _ghiseuService.DeleteGhiseu(id);
            if (deletedGhiseu == null)
            {
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghișeul cu id-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(deletedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }
}
