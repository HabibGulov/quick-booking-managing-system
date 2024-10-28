public record ServiceFilter : BaseFilter
{
    public int? WorkerId { get; set; }
    public string? Name { get; set; }   
}