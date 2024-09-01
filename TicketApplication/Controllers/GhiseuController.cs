using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Services;
using TicketApplication.Services.Dtos;
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

    [HttpGet("GetAll", Name = "GetAllGhiseu")]
    public async Task<ActionResult<ResponseValidator<IEnumerable<GhiseuDto>>>> GetAllGhiseu()
    {
        try
        {
            var ghiseuList = await _ghiseuService.GetAllGhiseu();
            return Ok(ResponseValidator<IEnumerable<GhiseuDto>>.Success(ghiseuList));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<IEnumerable<GhiseuDto>>.Failure("Validation error occurred: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<IEnumerable<GhiseuDto>>.Failure($"An error occurred: {e.Message}"));
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
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghiseu with ID {id} not found."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(ghiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("Validation error occurred: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"An error occurred: {e.Message}"));
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
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("Validation error occurred: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"An error occurred: {e.Message}"));
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
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghiseu with ID {id} not found."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(updatedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("Validation error occurred: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"An error occurred: {e.Message}"));
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
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghiseu with ID {id} not found."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(updatedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("Validation error occurred: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"An error occurred: {e.Message}"));
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
                return NotFound(ResponseValidator<GhiseuDto>.Failure($"Ghiseu with ID {id} not found."));
            }
            return Ok(ResponseValidator<GhiseuDto>.Success(deletedGhiseu));
        }
        catch (ValidationException e)
        {
            return BadRequest(ResponseValidator<GhiseuDto>.Failure("Validation error occurred: " + e.Errors.FirstOrDefault()?.ErrorMessage));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ResponseValidator<GhiseuDto>.Failure(e.Message));
        }
        catch (Exception e)
        {
            return StatusCode(500, ResponseValidator<GhiseuDto>.Failure($"An error occurred: {e.Message}"));
        }
    }
}
