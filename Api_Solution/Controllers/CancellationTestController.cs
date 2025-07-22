using Api_Solution.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Solution.Controllers;

public class CancellationTestController : BaseController
{
    private readonly UserController _userController;
    private readonly IHttpClientFactory _httpClientFactory;

    public CancellationTestController(UserController userController, IHttpClientFactory httpClientFactory)
    {
        _userController = userController;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("test-create-cancellation")]
    public async Task<IActionResult> TestCreateCancellation(int delayBeforeCancel = 2)
    {
        // Create a cancellation token that cancels after specified delay
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(delayBeforeCancel));

        var testModel = new UserModel { /* initialize with test data */ };

        try
        {
            // Directly call the controller method with cancellation
            var result = await _userController.Create(testModel, cts.Token);
            return Ok(new { Status = "Completed", Result = result });
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, new { Status = "Cancelled", Message = $"Operation cancelled after {delayBeforeCancel} seconds" });
        }
    }

    [HttpGet("test-readlogs-cancellation")]
    public async Task<IActionResult> TestReadLogsCancellation(int delayBeforeCancel = 2)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(delayBeforeCancel));

        try
        {
            var result = await _userController.ReadLogs(cts.Token);
            return Ok(new { Status = "Completed", Result = result });
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, new { Status = "Cancelled", Message = $"Operation cancelled after {delayBeforeCancel} seconds" });
        }
    }

    [HttpGet("simulate-client-cancellation")]
    public async Task<IActionResult> SimulateClientCancellation(string endpoint, int cancelAfterSeconds = 2)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(cancelAfterSeconds));

        try
        {
            var url = endpoint.ToLower() switch
            {
                "create" => "api/user",
                "readlogs" => "api/user/readlogs",
                _ => throw new ArgumentException("Invalid endpoint")
            };

            var response = await client.PostAsJsonAsync(url, new UserModel(), cts.Token);
            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                Status = "Completed",
                Response = content,
                StatusCode = response.StatusCode
            });
        }
        catch (TaskCanceledException)
        {
            return Ok(new
            {
                Status = "ClientCancelled",
                Message = $"Client cancelled request after {cancelAfterSeconds} seconds"
            });
        }
    }

    [HttpGet("test-long-operation/{seconds}")]
    public async Task<IActionResult> TestLongOperation(int seconds, CancellationToken cancellationToken)
    {
        try
        {
            for (int i = 0; i < seconds; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(1000, cancellationToken);
            }
            return Ok($"Completed {seconds}-second operation");
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, $"Cancelled after {seconds} seconds");
        }
    }

    [HttpGet("test-parallel-operations")]
    public async Task<IActionResult> TestParallelOperations(CancellationToken cancellationToken)
    {
        try
        {
            var tasks = new List<Task<string>>();

            for (int i = 1; i <= 3; i++)
            {
                int taskId = i;
                tasks.Add(Task.Run(async () =>
                {
                    await Task.Delay(taskId * 1000, cancellationToken);
                    return $"Task {taskId} completed";
                }, cancellationToken));
            }

            var results = await Task.WhenAll(tasks);
            return Ok(results);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, "Parallel operations cancelled");
        }
    }

}


