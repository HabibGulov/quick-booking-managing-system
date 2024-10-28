public class Appointment : BaseEntity
{
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus Status { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int WorkerId { get; set; }
    public Worker Worker { get; set; } = null!;

    public int ServiceId { get; set; } 
    public Service Service { get; set; } = null!;
}