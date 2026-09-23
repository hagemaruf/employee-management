using System.Net.Http.Json;
using EmployeeClient.Models;

namespace EmployeeClient.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public string? AccessToken { get; private set; }

    public ApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080")
        };

        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<AuthResponse> LoginAsync(
        string username,
        string password)
    {
        var request = new LoginRequest
        {
            Username = username,
            Password = password
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/auth/login",
            request);

        response.EnsureSuccessStatusCode();

        var authResponse =
            await response.Content.ReadFromJsonAsync<AuthResponse>();

        if (authResponse is null)
        {
            throw new InvalidOperationException(
                "Login response is empty.");
        }

        AccessToken = authResponse.Token;

        return authResponse;
    }

    public void Logout()
    {
        AccessToken = null;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        if (string.IsNullOrEmpty(AccessToken))
        {
            throw new InvalidOperationException(
                "User is not authenticated.");
        }

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/employees");

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                AccessToken);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var employees =
            await response.Content.ReadFromJsonAsync<List<Employee>>();

        return employees ?? new List<Employee>();
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        if (string.IsNullOrEmpty(AccessToken))
        {
            throw new InvalidOperationException(
                "User is not authenticated.");
        }

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "/api/employees");

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                AccessToken);

        request.Content = JsonContent.Create(employee);

        var response = await _httpClient.SendAsync(request);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"HTTP {(int)response.StatusCode} " +
                $"{response.StatusCode}\n" +
                $"Response: {responseBody}");
        }

        var createdEmployee =
            System.Text.Json.JsonSerializer.Deserialize<Employee>(
                responseBody,
                new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (createdEmployee is null)
        {
            throw new InvalidOperationException(
                "Create employee response is empty.");
        }

        return createdEmployee;
    }

    public async Task<Employee> UpdateEmployeeAsync(
    long id,
    Employee employee)
    {
        if (string.IsNullOrEmpty(AccessToken))
        {
            throw new InvalidOperationException(
                "User is not authenticated.");
        }

        using var request = new HttpRequestMessage(
            HttpMethod.Put,
            $"/api/employees/{id}");

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                AccessToken);

        request.Content = JsonContent.Create(employee);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var updatedEmployee =
            await response.Content.ReadFromJsonAsync<Employee>();

        if (updatedEmployee is null)
        {
            throw new InvalidOperationException(
                "Update employee response is empty.");
        }

        return updatedEmployee;
    }

    public async Task DeleteEmployeeAsync(long id)
    {
        if (string.IsNullOrEmpty(AccessToken))
        {
            throw new InvalidOperationException(
                "User is not authenticated.");
        }

        using var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/api/employees/{id}");

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                AccessToken);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();
    }
}