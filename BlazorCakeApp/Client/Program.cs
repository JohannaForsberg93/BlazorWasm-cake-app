using BlazorCakeApp.Client;
using BlazorCakeApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorCakeApp.ServerAPI",
	client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

////Gör att man måste vara inloggad för att se sidan
//.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorCakeApp.ServerAPI"));

builder.Services.AddScoped<ICakeRepository, CakeRepository>();

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
