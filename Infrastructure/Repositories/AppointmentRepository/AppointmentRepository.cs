public class AppointmentRepository(QuickBookingDbContext context) : IAppointmentRepository
{
    public bool CreateAppointment(AppointmentCreateDTO appointmentCreateDTO)
    {
        try
        {
            if (!CanBookAppointment(appointmentCreateDTO))
            {
                return false;
            }

            var duration = (from appoint in context.Appointments
                            join service in context.Services on appoint.ServiceId equals service.Id
                            select service.Duration).FirstOrDefault();

            var appointment = new Appointment
            {
                StartTime = appointmentCreateDTO.StartTime,
                Duration = duration,
                Status = appointmentCreateDTO.Status,
                ClientId = appointmentCreateDTO.ClientId,
                WorkerId = appointmentCreateDTO.WorkerId,
                ServiceId = appointmentCreateDTO.ServiceId
            };

            context.Appointments.Add(appointment);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    // private bool CanBookAppointment(AppointmentCreateDTO appointmentCreateDTO)
    // {
    //     // Получаем конечное время новой записи
    //     var newStartTime = appointmentCreateDTO.StartTime;
    //     var newEndTime = newStartTime.Add(appointmentCreateDTO.Duration);

    //     // Проверяем пересечения с существующими записями

    //     List<Appointment> appointments = context.Appointments.ToList();

    //     return !appointments
    //         .Any(a => a.WorkerId == appointmentCreateDTO.WorkerId
    //                    && a.Status != AppointmentStatus.Confirmed
    //                    && a.StartTime < newEndTime
    //                    && a.StartTime.Add(a.Duration) > newStartTime);
    // }

    private bool CanBookAppointment(AppointmentCreateDTO appointmentCreateDTO)
    {
        var newStartTime = appointmentCreateDTO.StartTime;
        var newEndTime = newStartTime.Add(appointmentCreateDTO.Duration);

        var worker = context.Workers.FirstOrDefault(w => w.Id == appointmentCreateDTO.WorkerId);
        if (worker == null || !IsWithinWorkHours(newStartTime, appointmentCreateDTO.Duration, worker.WorkHours))
        {
            return false; 
        }

        List<Appointment> appointments = context.Appointments.ToList();

        return !appointments
            .Where(a => a.WorkerId == appointmentCreateDTO.WorkerId && a.Status == AppointmentStatus.Confirmed)
            .Any(a => a.StartTime < newEndTime && a.StartTime.Add(a.Duration) > newStartTime);
    }


    private bool IsWithinWorkHours(DateTime startTime, TimeSpan duration, string workHours)
    {
        var endTime = startTime.Add(duration);

        var hours = workHours.Split('-');
        if (hours.Length != 2) return false;

        if (TimeSpan.TryParse(hours[0], out TimeSpan workStart) &&
            TimeSpan.TryParse(hours[1], out TimeSpan workEnd))
        {
            return startTime.TimeOfDay >= workStart && endTime.TimeOfDay <= workEnd;
        }
        return false;
    }


    public bool DeleteAppointment(int id)
    {
        try
        {
            Appointment? appointment = context.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment == null) return false;
            appointment.DeleteAppointment();
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public AppointmentReadDTO? GetAppointmentById(int id)
    {
        try
        {
            Appointment? appointment = context.Appointments.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            return appointment?.AppointmentToAppointmentRead();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public PaginationResponse<IEnumerable<AppointmentReadDTO>> GetAppointments(AppointmentFilter appointmentFilter)
    {
        try
        {
            IQueryable<AppointmentReadDTO> appointments = context.Appointments
                .Where(x => x.IsDeleted == false)
                .Select(x => x.AppointmentToAppointmentRead());

            if (appointmentFilter.WorkerId != null)
                appointments = appointments.Where(x => x.WorkerId == appointmentFilter.WorkerId);

            appointments = appointments.Skip((appointmentFilter.PageNumber - 1) * appointmentFilter.PageSize).Take(appointmentFilter.PageSize);

            int totalRecords = context.Appointments.Where(x => x.IsDeleted == false).Count();

            return PaginationResponse<IEnumerable<AppointmentReadDTO>>.Create(appointmentFilter.PageNumber, appointmentFilter.PageSize, totalRecords, appointments);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return PaginationResponse<IEnumerable<AppointmentReadDTO>>.Create(
                pageNumber: appointmentFilter.PageNumber,
                pageSize: appointmentFilter.PageSize,
                totalRecords: 0,
                data: Enumerable.Empty<AppointmentReadDTO>()
            );
        }
    }

    public bool UpdateAppointment(AppointmentUpdateDTO appointmentUpdateDTO)
    {
        try
        {
            Appointment? appointment = context.Appointments.FirstOrDefault(x => x.Id == appointmentUpdateDTO.Id && x.IsDeleted == false);
            if (appointment == null) return false;

            appointment.AppointmentUpdate(appointmentUpdateDTO);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }
}
