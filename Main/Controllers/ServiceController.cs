using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/service")]
public class ServiceController(IServiceRepository serviceRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateService([FromBody] ServiceCreateDTO serviceCreateDTO)
    {
        var result = serviceRepository.CreateService(serviceCreateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetServices([FromQuery] ServiceFilter serviceFilter)
    {
        var result = serviceRepository.GetServices(serviceFilter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ServiceReadDTO>>>.Success(null!, result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetServiceById(int id)
    {
        var result = serviceRepository.GetServiceById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(ApiResponse<ServiceReadDTO>.Success(null!, result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateService([FromBody] ServiceUpdateDTO serviceUpdateDTO)
    {
        var result = serviceRepository.UpdateService(serviceUpdateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteService(int id)
    {
        var result = serviceRepository.DeleteService(id);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, false));
    }
}
