using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlazorCakeApp.Server.Interfaces;
using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Shared.Dto;
using Humanizer;
using Microsoft.AspNetCore.Http.Connections;
using MongoDB.Bson;
using MongoDB.Driver;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorCakeApp.Server.Repositories
	{
	public class CakeRepository : ICakeRepository
	{
		private readonly IMongoCollection<Cake> _cakeCollection;
		public CakeRepository()
			{
				var settings = MongoClientSettings.FromConnectionString("mongodb://user:cake00@ac-loy4kvk-shard-00-00.jbvety0.mongodb.net:27017,ac-loy4kvk-shard-00-01.jbvety0.mongodb.net:27017,ac-loy4kvk-shard-00-02.jbvety0.mongodb.net:27017/?ssl=true&replicaSet=atlas-ycrbv3-shard-0&authSource=admin&retryWrites=true&w=majority");

			var client = new MongoClient(settings);
			var database = client.GetDatabase("cakeDb");
			_cakeCollection =
				database.GetCollection<Cake>("cakes",
					new MongoCollectionSettings() { AssignIdOnInsert = true });

			}

		public async Task<List<CakeDto>> GetAllCakes()
		{
			var cakes = await _cakeCollection.FindAsync(_ => true);

		var cakeDtos = cakes.ToEnumerable().Select(c => new CakeDto()
{
			Id = c.Id.ToString(),
			Available = c.Available,
			CakeName = c.CakeName,
			Ingredients = c.Ingredients,
			Allergens = c.Allergens,
			Description = c.Description,
			Price = c.Price
			});
			return cakeDtos.ToList();
			}


		public async Task<CakeDto> CakeById(string id)
			{
			var objectId = new ObjectId(id);
			var filter = Builders<Cake>.Filter.Eq(c => c.Id, objectId);
			var cake = await _cakeCollection.Find(filter).FirstOrDefaultAsync();

			if (cake != null)
				{
				var cakeDto = new CakeDto()
					{
					Id = cake.Id.ToString(),
					Available = cake.Available,
					CakeName = cake.CakeName,
					Ingredients = cake.Ingredients,
					Allergens = cake.Allergens,
					Description = cake.Description,
					Price = cake.Price
					};

				return cakeDto;
				}

			return null;
			}

		public async Task UpdateCake(string id, CakeDto cake)
			{
			var objectId = new ObjectId(id);
			var filter = Builders<Cake>.Filter.Eq("Id", objectId);

			var update = Builders<Cake>.Update

				.Set("CakeName", cake.CakeName)
				.Set("Description", cake.Description)
				.Set("Price", cake.Price)
				.Set("Allergens", cake.Allergens)
				.Set("Available", cake.Available);

			await _cakeCollection.UpdateOneAsync(filter, update);

			}


		public async Task AddNewCake(CakeDto cake)
		{
			var newCake = new Cake
			{
				Id = ObjectId.GenerateNewId(),
				CakeName = cake.CakeName,
				Description = cake.Description,
				Price = cake.Price,
				Allergens = cake.Allergens,
				Available = cake.Available
			};

			await _cakeCollection.InsertOneAsync(newCake);
		}


		public async Task DeleteCake(string id)
		{
			var objectId = new ObjectId(id);
			var filter = Builders<Cake>.Filter.Eq("Id", objectId);

			await _cakeCollection.FindOneAndDeleteAsync(filter);

		}




		//Skit i denna
		public async Task<IEnumerable<Cake>> SearchCakeByName(string name)
		{
			//var filter = Builders<Cake>.Filter.Regex("Name", name);

			//var filter = Builders<Cake>.Filter.Eq("Name", name);


			//var filter = Builders<Cake>.Filter.AnyEq("Name", name);

			//var filter = Builders<Cake>.Filter.AnyIn("Name", new List<string> { name });


			//var filter = Builders<Cake>.Filter.Regex("Name", new BsonRegularExpression(name, "i"));

			//var queryExpression = new BsonRegularExpression(new Regex(name, RegexOptions.None));

			//var filter = Builders<Cake>.Filter.Regex("Name", "^" + name + ".*");

			//var cake = await _cakeCollection.FindAsync(filter);

			var filter = await _cakeCollection.FindAsync(_ => true);
			//test.ToEnumerable().Where(x => x.CakeName == name).ToList();

			var cake = filter.ToEnumerable().Where(x => x.CakeName == name).ToList();

			return cake;

		}

		public async Task CakeStatus(Object id, bool available)
		{

			var filter = Builders<Cake>.Filter
				.Eq("Id", id);

			var update = Builders<Cake>.Update
				.Set("Available", available);

await _cakeCollection.UpdateOneAsync(filter, update, new UpdateOptions());

		}

		}
	}
	
