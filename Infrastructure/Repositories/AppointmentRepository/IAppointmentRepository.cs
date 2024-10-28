public interface IAppointmentRepository
{
    bool CreateAppointment(AppointmentCreateDTO appointmentCreateDTO);
    bool DeleteAppointment(int id);
    AppointmentReadDTO? GetAppointmentById(int id);
    PaginationResponse<IEnumerable<AppointmentReadDTO>> GetAppointments(AppointmentFilter appointmentFilter);
    bool UpdateAppointment(AppointmentUpdateDTO appointmentUpdateDTO);
}
