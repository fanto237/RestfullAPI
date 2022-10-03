using MagicVillaAPI.Data;
using MagicVillaAPI.Mapper;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Controllers;

[ApiController]
[Route("api/villaAPI")]
// [Route("api/[controller]")]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    private readonly DataContext _dbContext;
    private readonly IMapper _mapper;
    

    public VillaApiController(ILogger<VillaApiController> logger, DataContext dbContext, IMapper mapper)
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
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        _logger.LogInformation("Getting all villas");
        var allVillas = _dbContext.Villas.ToList();
        return Ok(allVillas);
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
    public ActionResult<VillaDto> Get(int id)
    {
        if (id == 0)
        {
            _logger.LogError("The id can't be different to 0", id);
            return BadRequest();
        }

        var villa = _dbContext.Villas.FirstOrDefault(u =>u.Id == id);
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDto> Create([FromBody] VillaDto? villaDto)
    {
        // if (!ModelState.IsValid)
        //     return BadRequest(ModelState);

        if (_dbContext.Villas.ToList().FirstOrDefault(u => u.Name == villaDto?.Name) is not null)
        {
            _logger.LogError("The villa : {VillaDtoName} already exist in the API", villaDto?.Name);
            ModelState.AddModelError("CustomError", $"The villa : {villaDto?.Name} already exist in the API");
            return BadRequest(ModelState);
        }

        if (villaDto is null)
        {
            _logger.LogError("The Villa to be created is not valid");
            return BadRequest(villaDto);
        }

        if (villaDto.Id > 0)
        {
            _logger.LogError("The Id can't be bigger than 0");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var entity = _mapper.Map<Villa>(villaDto);
        _dbContext.Villas.Add(entity);
        _dbContext.SaveChanges();
        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
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
    public IActionResult Delete(int id)
    {
        if (id is 0)
            return BadRequest();
        var villaToDelete = _dbContext.Villas.FirstOrDefault(u => u.Id == id);
        if (villaToDelete is null)
        {
            _logger.LogError("There is no villa found corresponding to this id :{Id}", id);
            return NotFound();
        }

        _dbContext.Villas.Remove(villaToDelete);
        _dbContext.SaveChanges();
        _logger.LogInformation("The villa with the id {Id} has been deleted", id);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] VillaDto? villaDto)
    {
        if (villaDto is null || villaDto.Id != id)
        {
            _logger.LogError("The villa can't be null or have an id different to the id of the villa to update ");
            return BadRequest();
        }

        // var villaToUpdate = _dbContext.Villas.FirstOrDefault(u => u.Id == id);
        // if (villaToUpdate is null)
        // {
        //     _logger.LogError("The no villa found corresponding to this id :{Id}", id);
        //     return NotFound();
        // }

        var villaToUpdate = _mapper.Map<Villa>(villaDto);
        villaToUpdate.Id = villaDto.Id;
        villaToUpdate.UpdatedDate = DateTime.Now;
        _dbContext.Villas.Update(villaToUpdate);
        _dbContext.SaveChanges();
        _logger.LogInformation("The villa with the Id {Id} has been updated", id);
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Patch(int id, [FromBody] JsonPatchDocument<VillaDto>? patchDto)
    {
        if (id == 0 || patchDto is null)
        {
            _logger.LogError("The Id can't be different to 0 or the patch document it not valid");
            return BadRequest();
        }

        var villaToPatch = _dbContext.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
        if (villaToPatch is null)
        {
            _logger.LogError("The no villa found corresponding to this id :{Id}", id);
            return NotFound();
        }

        var villaDto = _mapper.Map<VillaDto>(villaToPatch);

        patchDto.ApplyTo(villaDto, ModelState);
        if (!ModelState.IsValid)
            return BadRequest();

        villaToPatch = _mapper.Map<Villa>( villaDto  );
        villaToPatch.UpdatedDate = DateTime.Now;
        _dbContext.Villas.Update(villaToPatch);
        _dbContext.SaveChanges();
        _logger.LogInformation("The villa with the Id {Id} has been updated", id);
        return Ok();
    }
}