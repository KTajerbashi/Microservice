namespace MVC_Solution.Models;

// Models/DashboardViewModel.cs
public class DashboardViewModel
{
    public List<UserModel> Users { get; set; } = new();
    public List<LogModel> Logs { get; set; } = new();
    public string? ErrorMessage { get; set; }
    public bool OperationCancelled { get; set; }
}