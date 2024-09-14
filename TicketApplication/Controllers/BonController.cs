using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Data.Entities;
using TicketApplication.Services;
using TicketApplication.Services.Dtos.BonDtos;
using TicketApplication.Validators.ResponseValidator;

[ApiController]
[Route("[controller]")]
public class BonController : ControllerBase
{
    private readonly IBonService _bonService;

    public BonController(IBonService bonService)
    {
        _bonService = bonService;
    }

    [HttpGet("GetAll", Name = "GetAllBon")]
    public async Task<ActionResult<ResponseValidator<IEnumerable<BonDtoID>>>> GetAllBon()
    {
        try
        {
            var bonList = await _bonService.GetAllBon();
            if (bonList == null)
            {
                return NotFound(ResponseValidator<BonDtoID>.Failure("Lista de bonuri este goală sau toate ghiseele sunt inchise."));
            }
            return Ok(ResponseValidator<IEnumerable<BonDtoID>>.Success(bonList));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<IEnumerable<BonDtoID>>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<IEnumerable<BonDtoID>>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpGet("Get/{id}", Name = "GetBonById")]
    public async Task<ActionResult<ResponseValidator<BonDto>>> GetBonById(int id)
    {
        try
        {
            var bon = await _bonService.GetBonById(id);
            if (bon == null)
            {
                return NotFound(ResponseValidator<BonDto>.Failure($"Bonul cu ID-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<BonDto>.Success(bon));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<BonDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<BonDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<BonDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpGet("GetAll/{ghiseuId}", Name = "GetAllBonsByGhiseuId")]
    public async Task<ActionResult<ResponseValidator<IEnumerable<BonDtoID>>>> GetAllBonByGhiseuId(int ghiseuId)
    {
        try
        {
            var bons = await _bonService.GetAllBonByGhiseuId(ghiseuId);
            if (bons == null || !bons.Any())
            {
                return NotFound(ResponseValidator<BonDtoID>.Failure($"Ghiseul cu id-ul {ghiseuId} este inactiv sau nu contine bonuri."));
            }
            return Ok(ResponseValidator<IEnumerable<BonDtoID>>.Success(bons));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<IEnumerable<BonDtoID>>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<IEnumerable<BonDtoID>>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPost("Add", Name = "AddBon")]
    public async Task<ActionResult<ResponseValidator<BonDto>>> AddBon([FromBody] BonDto bon)
    {
        try
        {
            var addedBon = await _bonService.AddBon(bon);
            return Ok(ResponseValidator<BonDto>.Success(addedBon));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<BonDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<BonDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPut("MarkAsInProgress/{id}", Name = "MarkBonAsInProgress")]
    public async Task<ActionResult<ResponseValidator<BonDto>>> MarkAsInProgress(int id)
    {
        try
        {
            var updatedBon = await _bonService.MarkAsInProgress(id);
            if (updatedBon == null)
            {
                return NotFound(ResponseValidator<BonDto>.Failure($"Bonul cu ID-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<BonDto>.Success(updatedBon));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<BonDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<BonDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<BonDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPut("MarkAsReceived/{id}", Name = "MarkBonAsReceived")]
    public async Task<ActionResult<ResponseValidator<BonDto>>> MarkAsReceived(int id)
    {
        try
        {
            var updatedBon = await _bonService.MarkAsReceived(id);
            if (updatedBon == null)
            {
                return NotFound(ResponseValidator<BonDto>.Failure($"Bonul cu ID-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<BonDto>.Success(updatedBon));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<BonDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<BonDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<BonDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }

    [HttpPut("MarkAsClose/{id}", Name = "MarkBonAsClose")]
    public async Task<ActionResult<ResponseValidator<BonDto>>> MarkAsClose(int id)
    {
        try
        {
            var updatedBon = await _bonService.MarkAsClosed(id);
            if (updatedBon == null)
            {
                return NotFound(ResponseValidator<BonDto>.Failure($"Bonul cu ID-ul {id} nu a fost găsit."));
            }
            return Ok(ResponseValidator<BonDto>.Success(updatedBon));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<BonDto>.Failure("A apărut o eroare de validare: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<BonDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<BonDto>.Failure($"A apărut o eroare: {e.Message}"));
        }
    }
}
