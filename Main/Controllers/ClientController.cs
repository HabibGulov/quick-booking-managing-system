using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/client")]

public class ClientController(IClientReposiory clientRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateClient([FromBody] ClientCreateDTO clientCreateDTO)
    {
        var result = clientRepository.CreateClient(clientCreateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetClients([FromQuery] ClientFilter clientFilter)
    {
        var result = clientRepository.GetClients(clientFilter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<ClientReadDTO>>>.Success(null!, result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetClientById(int id)
    {
        var result = clientRepository.GetClientById(id);
        return result != null
            ? Ok(ApiResponse<ClientReadDTO>.Success(null!, result))
            : NotFound(ApiResponse<ClientReadDTO>.Fail(null!, result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateClient([FromBody] ClientUpdateDTO clientUpdateDTO)
    {
        var result = clientRepository.UpdateClient(clientUpdateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteClient(int id)
    {
        var result = clientRepository.DeleteClient(id);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, true))
            : NotFound(ApiResponse<bool>.Fail(null!, false));
    }
}