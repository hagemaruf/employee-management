using EmployeeClient.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EmployeeClient.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    private string? _accessToken;
    private string? _refreshToken;

    public ApiClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    // =========================
    // LOGIN
    // =========================

    public async Task<bool> LoginAsync(
        string username,
        string password)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/api/auth/login",
            new
            {
                username,
                password
            });

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var tokenResponse =
            await response.Content
                .ReadFromJsonAsync<TokenResponse>();

        if (tokenResponse == null)
        {
            return false;
        }

        _accessToken = tokenResponse.AccessToken;
        _refreshToken = tokenResponse.RefreshToken;

        return true;
    }

    // =========================
    // REFRESH TOKEN
    // =========================

    public async Task<bool> RefreshTokenAsync()
    {
        if (string.IsNullOrEmpty(_refreshToken))
        {
            return false;
        }

        var response = await _httpClient.PostAsJsonAsync(
            "/api/auth/refresh",
            new
            {
                refreshToken = _refreshToken
            });

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var tokenResponse =
            await response.Content
                .ReadFromJsonAsync<TokenResponse>();

        if (tokenResponse == null)
        {
            return false;
        }

        // Refresh Token Rotation
        _accessToken = tokenResponse.AccessToken;
        _refreshToken = tokenResponse.RefreshToken;

        return true;
    }

    // =========================
    // GET EMPLOYEES
    // =========================

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var response =
            await GetAsync("/api/employees");

        if (!response.IsSuccessStatusCode)
        {
            return new List<Employee>();
        }

        return await response.Content
            .ReadFromJsonAsync<List<Employee>>()
            ?? new List<Employee>();
    }

    // =========================
    // GENERIC GET
    // =========================

    public async Task<HttpResponseMessage> GetAsync(
        string endpoint)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            endpoint);

        if (!string.IsNullOrEmpty(_accessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _accessToken);
        }

        return await _httpClient.SendAsync(request);
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        if (string.IsNullOrEmpty(_accessToken))
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
                _accessToken);

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
        if (string.IsNullOrEmpty(_accessToken))
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
                _accessToken);

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
        if (string.IsNullOrEmpty(_accessToken))
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
                _accessToken);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();
    }

    public void Logout()
    {
        _accessToken = null;
    }
}
