public class WorkerCreateDTO
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Profession { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string WorkPlace { get; set; } =null!;
}

public class WorkerUpdateDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Profession { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
    public string WorkPlace { get; set; } =null!;
}

public class WorkerReadDTO:WorkerUpdateDTO
{
    
}

