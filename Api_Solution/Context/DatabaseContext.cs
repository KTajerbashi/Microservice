using Api_Solution.Models;

namespace Api_Solution.Context;

public static class DatabaseContext
{
    private static List<UserModel> UserModels { get; set; } = new List<UserModel>();
    private static List<LogModel> Logs { get; set; } = new List<LogModel>();
    public static List<UserModel> Create(UserModel entity)
    {
        entity.Id = IdGenerator.GenerateId();
        UserModels.Add(entity);
        return UserModels;
    }
    public static UserModel ReadById(long id)
        => UserModels.Where(item => item.Id == id).FirstOrDefault()!;

    public static List<UserModel> ReadAll()
        => UserModels;

    public static void Logger(LogModel entity)
    { 
        entity.Id = IdGenerator.GenerateId();
        Logs.Add(entity); 
    }

    public static List<LogModel> ReadLogs()
        => Logs;

}
