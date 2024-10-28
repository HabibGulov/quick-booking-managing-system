public record AppointmentFilter : BaseFilter
{
    public int? WorkerId { get; set; }
    public int? ClientId { get; set; }
}