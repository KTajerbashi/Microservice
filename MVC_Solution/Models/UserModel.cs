namespace MVC_Solution.Models;

public class UserModel : BaseModel
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string Email => $"{Name}_{Family}@mail.com";
}
