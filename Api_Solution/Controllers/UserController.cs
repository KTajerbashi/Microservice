using Api_Solution.Context;
using Api_Solution.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api_Solution.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(UserModel model, CancellationToken cancellationToken)
    {
        DateTime startDate = DateTime.Now;
        try
        {
            // Simulate processing time (check cancellation)
            await Task.Delay(1000, cancellationToken);

            // Check cancellation before DB operation
            cancellationToken.ThrowIfCancellationRequested();
            var result = DatabaseContext.Create(model);

            // Simulate DB time (check cancellation)
            await Task.Delay(1000, cancellationToken);

            // Check cancellation before logging
            cancellationToken.ThrowIfCancellationRequested();
            DatabaseContext.Logger(new LogModel
            {
                IsCompleted = true,
                StartDate = startDate,
                EndDate = DateTime.Now,
                Message = "User created successfully"
            });

            return Ok(result);
        }
        catch (OperationCanceledException)
        {
            // Log cancellation
            DatabaseContext.Logger(new LogModel
            {
                IsCompleted = false,
                StartDate = startDate,
                EndDate = DateTime.Now,
                Message = "User creation cancelled"
            });

            return StatusCode(499, "Operation cancelled by user");
        }
    }


    [HttpGet]
    public async Task<IActionResult> Read(int delay, CancellationToken cancellationToken)
    {
        DateTime startDate = DateTime.Now;
        try
        {
            // Simulate processing time (check cancellation)
            await Task.Delay(delay*1000, cancellationToken);

            // Check cancellation before DB operation
            cancellationToken.ThrowIfCancellationRequested();
            var result = DatabaseContext.ReadAll();

            DatabaseContext.Logger(new LogModel
            {
                IsCompleted = true,
                StartDate = startDate,
                EndDate = DateTime.Now,
                Message = "Users Read successfully"
            });

            return Ok(result);
        }
        catch (OperationCanceledException ex)
        {
            var item = cancellationToken;
            // Log cancellation
            DatabaseContext.Logger(new LogModel
            {
                IsCompleted = false,
                StartDate = startDate,
                EndDate = DateTime.Now,
                Message = $"User Read cancelled {ex.Message} "
            });

            return StatusCode(499, "Operation cancelled by user");
        }
    }

    [HttpGet("ReadLogs")]
    public async Task<IActionResult> ReadLogs(CancellationToken cancellationToken)
    {
        try
        {
            // Simulate processing time
            await Task.Delay(500, cancellationToken);
            return Ok(DatabaseContext.ReadLogs());
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, "Log read operation cancelled");
        }
    }
}


