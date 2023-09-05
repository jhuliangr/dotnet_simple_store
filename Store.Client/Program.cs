using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Store.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Setting the API's URL
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7294") });

// Injecting ThingClient as Scoped
builder.Services.AddScoped<ThingClient>();

await builder.Build().RunAsync();
