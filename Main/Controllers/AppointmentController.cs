using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/appointment")]
public class AppointmentController(IAppointmentRepository appointmentRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateAppointment([FromBody] AppointmentCreateDTO appointmentCreateDTO)
    {
        var result = appointmentRepository.CreateAppointment(appointmentCreateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAppointments([FromQuery] AppointmentFilter appointmentFilter)
    {
        var result = appointmentRepository.GetAppointments(appointmentFilter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<AppointmentReadDTO>>>.Success(null!, result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAppointmentById(int id)
    {
        var result = appointmentRepository.GetAppointmentById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(ApiResponse<AppointmentReadDTO>.Success(null!, result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateAppointment([FromBody] AppointmentUpdateDTO appointmentUpdateDTO)
    {
        var result = appointmentRepository.UpdateAppointment(appointmentUpdateDTO);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteAppointment(int id)
    {
        var result = appointmentRepository.DeleteAppointment(id);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, false));
    }
}
