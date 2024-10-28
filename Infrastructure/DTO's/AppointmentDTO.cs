public class AppointmentCreateDTO
{
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus Status { get; set; }
    public int WorkerId { get; set; }
    public int ClientId { get; set; }
    public int ServiceId { get; set; } 
}

public class AppointmentReadDTO
{
    public int Id{get; set;}
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus Status { get; set; }
    public int WorkerId { get; set; }
    public int ClientId { get; set; }
    public int ServiceId { get; set; } 
}
public class AppointmentUpdateDTO:AppointmentReadDTO
{

}