namespace MVC_Solution.Models;

public class LogModel : BaseModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; }
    public string Message { get; set; }
}
