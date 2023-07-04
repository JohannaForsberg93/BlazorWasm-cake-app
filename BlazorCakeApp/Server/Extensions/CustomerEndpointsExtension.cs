using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Server.Repositories;
using BlazorCakeApp.Shared;
using MongoDB.Bson;

namespace BlazorCakeApp.Server.Extensions
	{
	public static class CustomerEndpointsExtension
		{
			public static WebApplication CustomerEndpoints(this WebApplication app)
			{
				app.MapGet("/customer/all", async (CustomerRepository repo) =>
				{
					var response = await repo.AllCustomers();
					return Results.Ok(response);
				});

				app.MapPost("/customer/new", async (CustomerRepository repo, Customer customer) =>
				{
					await repo.AddCustomer(customer);
					return Results.Ok("Lyckades lägga till en customer!");
				});

				app.MapPut("/customer/update/{id}", async (CustomerRepository repo, ObjectId id, Customer customer) =>
				{
					await repo.UpdateCustomer(id, customer);
					return Results.Ok("Lyckades ändra customer!");
				});

				app.MapGet("/customer/search/{email}", async (CustomerRepository repo, string email) =>
				{
					var response = await repo.CustomerByEmail(email);
					return Results.Ok(response);
				});

				return app;
			}
		}
	}
