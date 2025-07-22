namespace ApiDashboard.Models;

// Models/UserModel.cs
public class UserModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string Email => $"{Name}_{Family}@mail.com";
}

// Models/LogModel.cs
public class LogModel
{
    public bool IsCompleted { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Message { get; set; } = string.Empty;
}