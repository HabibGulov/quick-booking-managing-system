public class ServiceCreateDTO
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public int WorkerId { get; set; }
}

public class ServiceReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public int WorkerId { get; set; }
}

public class ServiceUpdateDTO:ServiceReadDTO
{

}