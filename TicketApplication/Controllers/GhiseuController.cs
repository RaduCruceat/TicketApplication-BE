using TicketApplication.Services.Dtos;
using TicketApplication.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<ActionResult<IEnumerable<GhiseuDto>>> GetAllGhiseu()
    {
        try
        {
            var ghiseuList = await _ghiseuService.GetAll();
            return Ok(ghiseuList);
        }
        catch (FluentValidation.ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    [HttpGet("Get/{id}", Name = "GetGhiseuById")]
    public async Task<ActionResult<GhiseuDto>> GetGhiseuById(int id)
    {
        try
        {
           var ghiseu = await _ghiseuService.GetGhiseuById(id);
           if (ghiseu == null)
           {
               return NotFound($"Ghiseu with ID {id} not found.");
           }
           return Ok(ghiseu);
        }
        catch (FluentValidation.ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    [HttpPost("Add", Name = "AddGhiseu")]
    public async Task<ActionResult<GhiseuDto>> AddGhiseu([FromBody] GhiseuDto ghiseu)
    {
        try
        {
            var addedGhiseu = await _ghiseuService.AddGhiseu(ghiseu);
            return Ok(addedGhiseu);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    [HttpPut("MarkAsActive/{id}", Name = "MarkGhiseuAsActive")]
    public async Task<ActionResult<GhiseuDto>> MarkAsActive(int id)
    {
        try
        {
            var updatedGhiseu = await _ghiseuService.MarkAsActive(id);
            if (updatedGhiseu == null)
            {
                return NotFound($"Ghiseu with ID {id} not found.");
            }
            return Ok(updatedGhiseu);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    [HttpPut("MarkAsInactive/{id}", Name = "MarkGhiseuAsInactive")]
    public async Task<ActionResult<GhiseuDto>> MarkAsInactive(int id)
    {
        try
        {
            var updatedGhiseu = await _ghiseuService.MarkAsInactive(id);
            if (updatedGhiseu == null)
            {
                return NotFound($"Ghiseu with ID {id} not found.");
            }
            return Ok(updatedGhiseu);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    [HttpDelete("Delete/{id}", Name = "DeleteGhiseu")]
    public async Task<ActionResult<GhiseuDto>> DeleteGhiseu(int id)
    {
        try
        {
            var deletedGhiseu = await _ghiseuService.DeleteGhiseu(id);
            if (deletedGhiseu == null)
            {
                return NotFound($"Ghiseu with ID {id} not found.");
            }
            return Ok(deletedGhiseu);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }
}