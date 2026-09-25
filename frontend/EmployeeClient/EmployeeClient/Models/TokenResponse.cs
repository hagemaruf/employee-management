namespace EmployeeClient.Models;

public class TokenResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public string TokenType { get; set; } = "Bearer";

    public long ExpiresIn { get; set; }
}