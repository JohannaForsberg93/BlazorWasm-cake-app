using BlazorCakeApp.Server.Data;
using BlazorCakeApp.Server.Interfaces;
using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Shared;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlazorCakeApp.Server.Repositories
	{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly IMongoCollection<Customer> _customerCollection;

		public CustomerRepository()
		{
			var settings = MongoClientSettings.FromConnectionString(
				"mongodb://user:cake00@ac-loy4kvk-shard-00-00.jbvety0.mongodb.net:27017,ac-loy4kvk-shard-00-01.jbvety0.mongodb.net:27017,ac-loy4kvk-shard-00-02.jbvety0.mongodb.net:27017/?ssl=true&replicaSet=atlas-ycrbv3-shard-0&authSource=admin&retryWrites=true&w=majority");
			var client = new MongoClient(settings);
			var database = client.GetDatabase("cakeDb");
			_customerCollection = database.GetCollection<Customer>("customers",
				new MongoCollectionSettings() { AssignIdOnInsert = true });

		}

		public async Task<IEnumerable<Customer>> AllCustomers()
		{
			var customers = await _customerCollection.FindAsync(_ => true);
			return customers.ToList();
			}

		public async Task AddCustomer(Customer customer)
			{
				await _customerCollection.InsertOneAsync(customer);
			}

		public async Task UpdateCustomer(ObjectId id, Customer customer)
		{
			var filter = Builders<Customer>.Filter.Eq("Id", id);

			var update = Builders<Customer>.Update
				.Set("FirstName", customer.FirstName)
				.Set("LastName", customer.LastName)
				.Set("Email", customer.Email)
				.Set("Phone", customer.Phone)
				.Set("Address", customer.Address);

			await _customerCollection.UpdateOneAsync(filter, update);
			}

		public async Task<IEnumerable<Customer>> CustomerByEmail(string email)
		{
			var filter = Builders<Customer>.Filter.Eq("Email", email);
			var customer = await _customerCollection.FindAsync(filter);
			return customer.ToList();
			}



		}

	}

