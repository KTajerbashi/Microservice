namespace Api_Solution.Context;

public static class IdGenerator
{
    private static readonly HashSet<long> _ids = new(); // Faster lookups
    private static readonly Random _random = new();
    private static readonly object _lock = new();

    // Configurable options
    public static int MaxAttempts { get; set; } = 1000;
    public static long MinValue { get; set; } = 1;
    public static long MaxValue { get; set; } = long.MaxValue;

    public static long GenerateId()
    {
        lock (_lock)
        {
            for (int i = 0; i < MaxAttempts; i++)
            {
                long id = GetRandomLong(MinValue, MaxValue);

                if (_ids.Add(id)) // Returns true if added (wasn't present)
                    return id;
            }

            throw new Exception($"Failed to generate unique ID after {MaxAttempts} attempts");
        }
    }

    private static long GetRandomLong(long min, long max)
    {
        byte[] buf = new byte[8];
        _random.NextBytes(buf);
        long longRand = BitConverter.ToInt64(buf, 0);
        return Math.Abs(longRand % (max - min)) + min;
    }

    public static bool IdExists(long id) => _ids.Contains(id);
    public static int GeneratedCount => _ids.Count;
    public static void ClearIds() => _ids.Clear();
}