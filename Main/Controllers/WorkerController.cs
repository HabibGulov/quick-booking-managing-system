using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/worker")]
public class WorkerController(IWorkerRepository workerRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateWorker([FromBody] WorkerCreateDTO workerCreateDTO)
    {
        var result = workerRepository.CreateWorker(workerCreateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetWorkers([FromQuery] WorkerFilter workerFilter)
    {
        var result = workerRepository.GetWorkers(workerFilter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<WorkerReadDTO>>>.Success(null!, result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetWorkerById(int id)
    {
        var result = workerRepository.GetWorkerById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(ApiResponse<WorkerReadDTO>.Success(null!, result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateWorker([FromBody] WorkerUpdateDTO workerUpdateDTO)
    {
        var result = workerRepository.UpdateWorker(workerUpdateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteWorker(int id)
    {
        var result = workerRepository.DeleteWorker(id);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, false));
    }
}
