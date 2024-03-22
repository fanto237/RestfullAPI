using MagicVillaAPI.Models;
using MagicVillaAPI.Models.Dto;
using MagicVillaAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers;

using Mapping = AutoMapper;

[ApiController]
[Route("api/villas")]
public class VillaApiController : ControllerBase
{
    private readonly ILogger<VillaApiController> _logger;
    private readonly Mapping.IMapper _mapper;
    private readonly IVillaRepository _repository;
    private readonly Response _response;


    public VillaApiController(ILogger<VillaApiController> logger, Mapping.IMapper mapper,
        IVillaRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
        _response = new Response();
    }

    /// <summary>
    ///     endpoint call to retrieve all villas
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response>> GetAll()
    {
        _logger.LogInformation("Getting all villas");
        var villas = await _repository.GetAllAsync();
        _response.Status = "success";
        _response.Result = villas;
        return Ok(_response);
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
    public async Task<ActionResult<Response>> Get(int id)
    {
        if (id == 0)
        {
            _logger.LogError("The id can't be 0");
            _response.Status = "fail";
            _response.Result = "The id can't be 0";
            return BadRequest(_response);
        }

        var villa = await _repository.GetByIdAsync(id);
        if (villa is null)
        {
            _logger.LogError("There is no villa found corresponding to this id :{Id}", id);
            _response.Status = "fail";
            _response.Result = $"There is no villa found corresponding to this id :{id}";
            return NotFound(_response);
        }

        _logger.LogInformation("Getting villa by id : {Id}", id);
        _response.Status = "fail";
        _response.Result = villa;
        return Ok(_response);
    }

    /// <summary>
    ///     endpoint called to create a new villa
    /// </summary>
    /// <param name="villaDto">the villa that has been created</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response>> Create([FromBody] VillaCreateDto villaDto)
    {
        var villaToCreate = await _repository.GetByNameAsync(villaDto.Name);
        if (villaToCreate is not null)
        {
            _logger.LogError("The villa : {VillaDtoName} already exist in the API", villaDto.Name);
            ModelState.AddModelError("CustomError", $"The villa : {villaDto.Name} already exist in the API");
            _response.Status = "fail";
            _response.Result = $"The villa : {villaDto.Name} already exist";
            return BadRequest(_response);
        }

        var model = _mapper.Map<Villa>(villaDto);
        await _repository.CreateAsync(model);
        _response.Status = "success";
        _response.Result = model;
        return CreatedAtRoute("GetVilla", new { id = model.Id }, _response);
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
    public async Task<ActionResult<Response>> Delete(int id)
    {
        if (id is 0)
        {
            _response.Status = "fail";
            _response.Result = "The id can not be 0";
            return BadRequest(_response);
        }

        var villaToDelete = await _repository.GetByIdAsync(id);

        if (villaToDelete is null)
        {
            _logger.LogError("There is no villa found corresponding to this id :{Id}", id);
            _response.Status = "fail";
            _response.Result = $"There is no villa found corresponding to this id :{id}";
            return NotFound(_response);
        }

        await _repository.RemoveAsync(villaToDelete);
        _logger.LogInformation("The villa with the id {Id} has been deleted", id);
        _response.Status = "success";
        _response.Result = null;
        return Ok(_response);
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response>> Update(int id, [FromBody] VillaUpdateDto? villaUpdateDto)
    {
        if (id == 0)
        {
            _logger.LogError("The Id of the villa to update can not be equal to 0");
            _response.Status = "fail";
            _response.Result = "The Id of the villa to update can not be equal to 0";
            return BadRequest(_response);
        }
        if (villaUpdateDto is null || villaUpdateDto.Id != id)
        {
            _logger.LogError("The villa can't be null or have an id different to the id of the villa to update ");
            _response.Status = "fail";
            _response.Result = "The villa can't be null or have an id different to the id of the villa to update";
            return BadRequest(_response);
        }

        var villaToUpdate = _mapper.Map<Villa>(villaUpdateDto);
        villaToUpdate.Id = villaUpdateDto.Id;
        villaToUpdate.UpdatedDate = DateTime.UtcNow;
        await _repository.UpdateAsync(villaToUpdate);
        _response.Status = "success";
        _response.Result = villaToUpdate;
        _logger.LogInformation("The villa with the Id {Id} has been updated", id);
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response>> Patch(int id, [FromBody] JsonPatchDocument<VillaUpdateDto>? patchDto)
    {
        if (id == 0 || patchDto is null)
        {
            _logger.LogError("The Id can't be different to 0 or the patch document it not valid");
            _response.Status = "fail";
            _response.Result = null;
            return BadRequest(_response);
        }

        var villaToPatch = await _repository.GetByIdAsync(id, false);
        if (villaToPatch is null)
        {
            _logger.LogError("The no villa found corresponding to this id :{Id}", id);
            _response.Status = "fail";
            _response.Result = $"The no villa found corresponding to this id :{id}";
            return NotFound(_response);
        }

        var villaUpdateDto = _mapper.Map<VillaUpdateDto>(villaToPatch);
        patchDto.ApplyTo(villaUpdateDto, ModelState);

        if (!ModelState.IsValid)
            return BadRequest();

        villaToPatch = _mapper.Map<Villa>(villaUpdateDto);
        villaToPatch.UpdatedDate = DateTime.Now;
        await _repository.UpdateAsync(villaToPatch);
        _response.Status = "success";
        _response.Result = villaToPatch;
        _logger.LogInformation("The villa with the Id {Id} has been updated", id);
        return Ok(_response);
    }
}