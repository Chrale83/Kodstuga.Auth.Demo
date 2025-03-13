using System.Net.Http.Headers;
using System.Net.Http.Json;
using Kodstuga.Auth.Blazor.Demo.Dtos;
using Kodstuga.Auth.Blazor.Demo.Interfaces;

namespace Kodstuga.Auth.Blazor.Demo.Services;

public class AuthenticationStateProvider : IAuthenticationStateProvider
{
    private readonly HttpClient _httpClient;


    public AuthenticationStateProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("backend");
    }
    public async Task<bool> IsAuthenticatedAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("manage/info");

        return response.IsSuccessStatusCode;
    }

    public async Task<string> GetRoleAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("roles");

        if (!response.IsSuccessStatusCode)
        {
            return String.Empty;
        }

        var role = await response.Content.ReadFromJsonAsync<List<GetRolesDto>>();

        if (role is null || !role.Any())
        {
            return "";
        }

        return role[0].Value;
    }

}

