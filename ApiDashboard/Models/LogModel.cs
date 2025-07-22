namespace ApiDashboard.Models;

// Models/LogModel.cs
public class LogModel
{
    public long Id { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Message { get; set; } = string.Empty;
}