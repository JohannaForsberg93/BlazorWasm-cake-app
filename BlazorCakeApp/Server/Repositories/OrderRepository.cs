using BlazorCakeApp.Server.Interfaces;
using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Shared.Dto;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlazorCakeApp.Server.Repositories
{
    public class OrderRepository: IOrderRepository
		{
			private readonly IMongoCollection<Order> _orderCollection;
		public OrderRepository()
			{
			var settings = MongoClientSettings.FromConnectionString(
				"mongodb://user:cake00@ac-loy4kvk-shard-00-00.jbvety0.mongodb.net:27017,ac-loy4kvk-shard-00-01.jbvety0.mongodb.net:27017,ac-loy4kvk-shard-00-02.jbvety0.mongodb.net:27017/?ssl=true&replicaSet=atlas-ycrbv3-shard-0&authSource=admin&retryWrites=true&w=majority");
			var client = new MongoClient(settings);
			var database = client.GetDatabase("cakeDb");
			_orderCollection = database.GetCollection<Order>("orders",
				new MongoCollectionSettings() { AssignIdOnInsert = true });
			}


		public async Task<IEnumerable<Order>> FindOrder(string email)
		{
			var filter = Builders<Order>.Filter.Eq("CustomerEmail", email);
			var orders = await _orderCollection.FindAsync(filter);
			return orders.ToList();

			}

		public async Task AddOrder(string email, List<OrderDto> order, string orderName)
		{
			var newOrder = new Order();
			newOrder.CustomerEmail = email;
			newOrder.Cakes = order;

			await _orderCollection.InsertOneAsync(newOrder);      

		}

		public async Task AddCakeToOrder(ObjectId id, OrderDto order)
		{
			var orderCake = new OrderDto();
orderCake.Cake = order.Cake;
		orderCake.Amount = order.Amount;

			var filter = Builders<Order>.Filter.Eq("Id", id);
			var update = Builders<Order>.Update.Push("Cakes", orderCake);

			await _orderCollection.FindOneAndUpdateAsync(filter, update);


		}
		}
		
	}
