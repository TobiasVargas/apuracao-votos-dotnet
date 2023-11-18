using Frontend;
using Frontend.Clients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configuration =  builder.Configuration;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44334") });
builder.Services.AddHttpClient<JogadoresClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44334");
});

builder.Services.AddHttpClient<VotoClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44336");
});

builder.Services.AddHttpClient<ApuracaoClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44368");
});

await builder.Build().RunAsync();
