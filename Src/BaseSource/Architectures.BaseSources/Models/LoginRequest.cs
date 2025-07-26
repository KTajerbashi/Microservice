namespace Architectures.BaseSources.Models;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ClientId { get; set; }
    public string GrantType { get; set; }
    public string ReturnUrl { get; set; }
}

public class LoginSSOResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ReturnUrl { get; set; }
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; }
    public string Scope { get; set; }   // Scopes for Server1 & Server2
}