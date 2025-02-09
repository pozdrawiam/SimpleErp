using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Se.Web.Client;
using Se.Web.Gui;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = builder.HostEnvironment.BaseAddress;

#if DEBUG

baseAddress = "https://localhost:5443";

#endif

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<ISeWebApiClient, SeWebApiClient>();

await builder.Build().RunAsync();
