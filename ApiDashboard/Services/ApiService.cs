using ApiDashboard.Models;
using System.Net.Http.Json;

namespace ApiDashboard.Services;

// Services/ApiService.cs
public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserModel>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<UserModel>>("user", cancellationToken)
               ?? new List<UserModel>();
    }

    public async Task<List<LogModel>> GetLogsAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<LogModel>>("user/readlogs", cancellationToken)
               ?? new List<LogModel>();
    }

    public async Task<string> CreateUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("user", user, cancellationToken);
        return response.IsSuccessStatusCode
            ? "User created successfully"
            : $"Error: {response.StatusCode}";
    }

    public async Task<string> TestCancellationAsync(int delaySeconds, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"user?delay={delaySeconds}", cancellationToken);
            return await response.Content.ReadAsStringAsync();
        }
        catch (OperationCanceledException)
        {
            return "Request was cancelled";
        }
    }
}