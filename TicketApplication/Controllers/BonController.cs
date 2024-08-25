using TicketApplication.Services.Dtos;
using TicketApplication.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<ActionResult<IEnumerable<BonDto>>> GetAllBon()
    {
        try
        {
            var bonList = await _bonService.GetAllBon();
            return Ok(bonList);
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

    [HttpGet("Get/{id}", Name = "GetBonById")]
    public async Task<ActionResult<BonDto>> GetBonById(int id)
    {
        try
        {
            var bon = await _bonService.GetBonById(id);
            if (bon == null)
            {
                return NotFound($"Bon with ID {id} not found.");
            }
            return Ok(bon);
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

    [HttpPost("Add", Name = "AddBon")]
    public async Task<ActionResult<BonDto>> AddBon([FromBody] BonDto bon)
    {
        try
        {
            var addedBon = await _bonService.AddBon(bon);
            return Ok(addedBon);
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

    [HttpPut("MarkAsInProgress/{id}", Name = "MarkBonAsInProgress")]
    public async Task<ActionResult<BonDto>> MarkAsInProgress(int id)
    {
        try
        {
            var updatedBon = await _bonService.MarkAsInProgress(id);
            if (updatedBon == null)
            {
                return NotFound($"Bon with ID {id} not found.");
            }
            return Ok(updatedBon);
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

    [HttpPut("MarkAsReceived/{id}", Name = "MarkBonAsReceived")]
    public async Task<ActionResult<BonDto>> MarkAsReceived(int id)
    {
        try
        {
            var updatedBon = await _bonService.MarkAsReceived(id);
            if (updatedBon == null)
            {
                return NotFound($"Bon with ID {id} not found.");
            }
            return Ok(updatedBon);
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

    [HttpPut("MarkAsClose/{id}", Name = "MarkBonAsClose")]
    public async Task<ActionResult<BonDto>> MarkAsClose(int id)
    {
        try
        {
            var updatedBon = await _bonService.MarkAsClosed(id);
            if (updatedBon == null)
            {
                return NotFound($"Bon with ID {id} not found.");
            }
            return Ok(updatedBon);
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