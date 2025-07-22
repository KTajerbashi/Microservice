using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC_Solution.Models;

namespace MVC_Solution.Controllers;

public class HomeController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IHttpClientFactory httpClientFactory, ILogger<HomeController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }


    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var viewModel = new DashboardViewModel();

        try
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            // Get users
            var usersResponse = await client.GetAsync("user", cancellationToken);
            if (usersResponse.IsSuccessStatusCode)
            {
                viewModel.Users = await usersResponse.Content.ReadFromJsonAsync<List<UserModel>>(cancellationToken) ?? new();
            }

            // Get logs
            var logsResponse = await client.GetAsync("user/readlogs", cancellationToken);
            if (logsResponse.IsSuccessStatusCode)
            {
                viewModel.Logs = await logsResponse.Content.ReadFromJsonAsync<List<LogModel>>(cancellationToken) ?? new();
            }
        }
        catch (OperationCanceledException)
        {
            viewModel.OperationCancelled = true;
            viewModel.ErrorMessage = "Request was cancelled";
        }
        catch (Exception ex)
        {
            viewModel.ErrorMessage = $"Error: {ex.Message}";
            _logger.LogError(ex, "Error fetching data from API");
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel model, CancellationToken cancellationToken)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.PostAsJsonAsync("user", model, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "User created successfully";
            }
            else
            {
                TempData["ErrorMessage"] = $"Error creating user: {response.StatusCode}";
            }
        }
        catch (OperationCanceledException)
        {
            TempData["ErrorMessage"] = "User creation was cancelled";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error: {ex.Message}";
            _logger.LogError(ex, "Error creating user");
        }

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
