namespace Kodstuga.Auth.Blazor.Demo.Interfaces;

public interface IAuthenticationStateProvider
{
    Task<bool> IsAuthenticatedAsync(string token);
    Task<string> GetRoleAsync(string token);
}