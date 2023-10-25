using MudNet8.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddSingleton<ThemeStateProvider>();
builder.Services.AddSingleton<WeatherForecastService>();

await builder.Build().RunAsync();
