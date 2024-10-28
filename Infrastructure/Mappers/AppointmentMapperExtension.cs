public static class AppointmentMapperExtension
{
    public static AppointmentReadDTO AppointmentToAppointmentRead(this Appointment appointment)
    {
        return new AppointmentReadDTO()
        {
            Id = appointment.Id,
            StartTime = appointment.StartTime,
            Duration = appointment.Duration,
            Status = appointment.Status,
            WorkerId = appointment.WorkerId,
            ClientId = appointment.ClientId,
            ServiceId = appointment.ServiceId
        };
    }

    public static Appointment AppointmentUpdate(this Appointment appointment, AppointmentUpdateDTO appointmentUpdateDTO)
    {
        appointment.StartTime = appointmentUpdateDTO.StartTime;
        appointment.Duration = appointmentUpdateDTO.Duration;
        appointment.Status = appointmentUpdateDTO.Status;
        appointment.WorkerId = appointmentUpdateDTO.WorkerId;
        appointment.ClientId = appointmentUpdateDTO.ClientId;
        appointment.ServiceId = appointmentUpdateDTO.ServiceId;
        return appointment;
    }

    public static Appointment AppointmentCreateToAppointment(this AppointmentCreateDTO appointmentCreateDTO)
    {
        return new Appointment()
        {
            StartTime = appointmentCreateDTO.StartTime,
            Duration = appointmentCreateDTO.Duration,
            Status = appointmentCreateDTO.Status,
            WorkerId = appointmentCreateDTO.WorkerId,
            ClientId = appointmentCreateDTO.ClientId,
            ServiceId = appointmentCreateDTO.ServiceId
        };
    }

    public static Appointment DeleteAppointment(this Appointment appointment)
    {
        appointment.IsDeleted = true;
        appointment.DeletedAt = DateTime.UtcNow;
        appointment.UpdatedAt = DateTime.UtcNow;
        appointment.Version += 1;
        return appointment;
    }
}
