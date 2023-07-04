using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Server.Repositories;
using BlazorCakeApp.Shared.Dto;
using MongoDB.Bson;

namespace BlazorCakeApp.Server.Extensions
	{
		public static class CakeEndpointsExtension
		{
			public static WebApplication CakeEndpoints(this WebApplication app)
			{
				app.MapGet("/cake/all", async (CakeRepository repo) =>
				{
					var response = await repo.GetAllCakes();
					return Results.Ok(response);
				});

				app.MapPost("/cake/add", async (CakeRepository repo, CakeDto cake) =>
				{
					await repo.AddNewCake(cake);
					return Results.Ok("Lyckades lägga till en ny tårta!");
				});

				app.MapDelete("/cake/remove/{id}", async (CakeRepository repo, string id) =>
				{

					await repo.DeleteCake(id);
					return Results.Ok("Lyckades ta bort en tårta!");

				});

				app.MapPut("/cake/update/{id}", async (CakeRepository repo, string id, CakeDto cake) =>
				{
					await repo.UpdateCake(id, cake);
					return Results.Ok("Lyckades uppdatera tårtan!");
				});

				app.MapGet("cake/{id}", async (CakeRepository repo, string id) =>
				{
					var response = await repo.CakeById(id);
					return Results.Ok(response);
				});

				app.MapGet("cake/search/name/{name}", async (CakeRepository repo, string name) =>
				{
					var response = await repo.SearchCakeByName(name);
					return Results.Ok(response);
				});


				app.MapPatch("cake/status/{id}", async (CakeRepository repo, ObjectId id, bool available) =>
				{
					await repo.CakeStatus(id, available);
					return Results.Ok("Lyckades ändra status för tårta!");

				});

				return app;
			}
		
	}
	}
