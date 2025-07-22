namespace ApiDashboard.Models;

// Models/UserModel.cs
public class UserModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string Email => $"{Name}_{Family}@mail.com";
}


public class CallAction
{
    public string ActionName { get; set; }
    public bool Repeatly { get; set; }
    public int RepeatlyCount { get; set; }
}
