public class Worker : User
{
    public string Profession { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
    public List<Appointment> Appointments { get; set; } = [];
    public List<Service> Services { get; set; } = [];
    public string WorkPlace { get; set; } =null!;
}