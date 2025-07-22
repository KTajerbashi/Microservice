namespace Api_Solution.Models;

public interface IBaseModel
{
    long Id { get; set; }
}
public abstract class BaseModel : IBaseModel
{
    public long Id { get; set; }
}
public class UserModel: BaseModel
{
    public string Name { get; set; }
    public string Family { get; set; }
}
public class LogModel : BaseModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; }
    public string Message { get; set; }
}