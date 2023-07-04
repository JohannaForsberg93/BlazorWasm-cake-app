using BlazorCakeApp.Server.Repositories;
using BlazorCakeApp.Shared.Dto;
using MongoDB.Bson;

namespace BlazorCakeApp.Server.Extensions
{
    public static class OrderEndpointsExtension
		{
			public static WebApplication OrderEndpoints(this WebApplication app)
			{

				app.MapGet("/order/{email}", async (OrderRepository repo, string email) =>
				{
					var response = await repo.FindOrder(email);
					return Results.Ok(response);
				});

				app.MapPost("/order/new/{email}", async (OrderRepository repo, string email, List<OrderDto> order, string orderName) =>
				{
					await repo.AddOrder(email, order, orderName);
					return Results.Ok("Lyckades lägga till en order!");
				});

				app.MapPut("/order/add/{id}", async (OrderRepository repo, ObjectId id, OrderDto order) =>
				{
					await repo.AddCakeToOrder(id, order);
					return Results.Ok("Lyckades lägga till en tårta i din order!");
				});

				return app;
			}
		}
	}
