using MagicVillaAPI.Data;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Controllers;
using Mapping = AutoMapper;
[ApiController]
[Route("api/villaAPI")]
// [Route("api/[controller]")]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    private readonly DataContext _dbContext;
    private readonly Mapping.IMapper _mapper;
    // private readonly 
    

    public VillaApiController(ILogger<VillaApiController> logger, DataContext dbContext, Mapping.IMapper mapper)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    ///     endpoint call to retrieve all villas
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
    {
        _logger.LogInformation("Getting all villas");
        var villas = await _dbContext.Villas.ToListAsync();
        return Ok(villas.Select(u => _mapper.Map<VillaDto>(u)));
    }


    /// <summary>
    ///     endpoint call to retrieve one villa base on its id
    /// </summary>
    /// <param name="id">id of the villa to get</param>
    /// <returns></returns>
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
    // [Route("api/villaAPI/{id:int}")]
    public async Task<ActionResult<VillaDto>> Get(int id)
    {
        if (id == 0)
        {
            _logger.LogError("The id can't be different to 0");
            return BadRequest();
        }

        var villa = await _dbContext.Villas.FindAsync(id);
        if (villa is null)
        {
            _logger.LogError("There is no villa found corresponding to this id :{Id}", id);
            return NotFound();
        }

        _logger.LogInformation("Getting villa by id : {Id}", id);
        return Ok(villa);
    }

    /// <summary>
    ///     endpoint called to create a new villa
    /// </summary>
    /// <param name="villaDto">the villa that has been created</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VillaCreateDto>> Create([FromBody] VillaCreateDto villaDto)
    {
        if (await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name == villaDto.Name) is not null)
        {
            _logger.LogError("The villa : {VillaDtoName} already exist in the API", villaDto.Name);
            ModelState.AddModelError("CustomError", $"The villa : {villaDto.Name} already exist in the API");
            return BadRequest(ModelState);
        }

        var model = _mapper.Map<Villa>(villaDto);
        await _dbContext.Villas.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
    }

    /// <summary>
    ///     endpoint accessed to delete a specific villa
    /// </summary>
    /// <param name="id">id of the villa to be deleted</param>
    /// <returns></returns>
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        if (id is 0)
            return BadRequest();
        var villaToDelete = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villaToDelete is null)
        {
            _logger.LogError("There is no villa found corresponding to this id :{Id}", id);
            return NotFound();
        }

        _dbContext.Villas.Remove(villaToDelete);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("The villa with the id {Id} has been deleted", id);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] VillaUpdateDto? villaUpdateDto)
    {
        if (villaUpdateDto is null || villaUpdateDto.Id != id)
        {
            _logger.LogError("The villa can't be null or have an id different to the id of the villa to update ");
            return BadRequest();
        }

        var villaToUpdate = _mapper.Map<Villa>(villaUpdateDto);
        villaToUpdate.Id = villaUpdateDto.Id;
        villaToUpdate.UpdatedDate = DateTime.Now;
        _dbContext.Villas.Update(villaToUpdate);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("The villa with the Id {Id} has been updated", id);
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<VillaUpdateDto>? patchDto)
    {
        if (id == 0 || patchDto is null)
        {
            _logger.LogError("The Id can't be different to 0 or the patch document it not valid");
            return BadRequest();
        }

        var villaToPatch = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (villaToPatch is null)
        {
            _logger.LogError("The no villa found corresponding to this id :{Id}", id);
            return NotFound();
        }

        var villaUpdateDto = _mapper.Map<VillaUpdateDto>(villaToPatch);

        patchDto.ApplyTo(villaUpdateDto, ModelState);
        if (!ModelState.IsValid)
            return BadRequest();

        villaToPatch = _mapper.Map<Villa>( villaUpdateDto  );
        villaToPatch.UpdatedDate = DateTime.Now;
        _dbContext.Villas.Update(villaToPatch);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("The villa with the Id {Id} has been updated", id);
        return Ok();
    }
}