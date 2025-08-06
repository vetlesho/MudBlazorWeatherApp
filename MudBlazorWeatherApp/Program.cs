using MudBlazor.Services;
using MudBlazorWeatherApp.Components;
using MudBlazorWeatherApp.Services;
using MudBlazorWeatherApp.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Register server-side WeatherService for API calls
builder.Services.AddScoped<WeatherService>();
builder.Services.AddHttpClient<WeatherService>();

// Register client-side WeatherService for component injection
builder.Services.AddScoped<ClientWeatherService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.Configuration["BaseAddress"] ?? "https://localhost:5071"}") });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapControllers();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MudBlazorWeatherApp.Client._Imports).Assembly);

app.Run();
