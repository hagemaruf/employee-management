namespace EmployeeClient.Models;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;

    public string TokenType { get; set; } = string.Empty;

    public long ExpiresIn { get; set; }
}