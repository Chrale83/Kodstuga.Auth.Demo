namespace Kodstuga.Auth.Blazor.Demo.Events;

public static class AuthPublisher
{
    public static event Action AuthenticationStateChanged;

    public static async Task OnAuthenticationStateChange()
    {
        AuthenticationStateChanged?.Invoke();
    }
}