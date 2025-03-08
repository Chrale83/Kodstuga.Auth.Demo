namespace Kodstuga.Auth.Blazor.Demo.Dtos;

public class LoginResponseDto
{


    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }


}