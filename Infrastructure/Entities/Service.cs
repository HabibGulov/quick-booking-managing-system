public class Service:BaseEntity
{
    public string Name { get; set; } = null!;   
    public string Description { get; set; } = null!; 
    public decimal Price { get; set; } 
    public TimeSpan Duration { get; set; } 
    public int WorkerId { get; set; } 
}