using BlazorCakeApp.Client;
using BlazorCakeApp.Client.Repositories;
using BlazorCakeApp.Client.Services;
using Blazored.LocalStorage;
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

//Dependency injections
builder.Services.AddScoped<ICakeRepository, CakeRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddApiAuthorization();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
