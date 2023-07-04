using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Shared;
using MongoDB.Bson;


namespace BlazorCakeApp.Server.Interfaces
	{
	public interface ICustomerRepository
	{
		Task<IEnumerable<Customer>> AllCustomers();
		Task<IEnumerable<Customer>> CustomerByEmail(string email);
		Task AddCustomer(Customer customer);
		Task UpdateCustomer(ObjectId id, Customer customer);

	}
	}
