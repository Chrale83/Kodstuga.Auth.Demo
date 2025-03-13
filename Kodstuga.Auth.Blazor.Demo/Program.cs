using Blazored.LocalStorage;
using Kodstuga.Auth.Blazor.Demo;
using Kodstuga.Auth.Blazor.Demo.Interfaces;
using Kodstuga.Auth.Blazor.Demo.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAuthenticationStateProvider, AuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient("backend", options =>
{
    options.BaseAddress = new Uri("https://localhost:7080/");
});

await builder.Build().RunAsync();
