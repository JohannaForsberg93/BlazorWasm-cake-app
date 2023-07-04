using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Shared.Dto;
using MongoDB.Bson;

namespace BlazorCakeApp.Server.Interfaces
{
    public interface IOrderRepository
	{
		Task AddOrder(string email, List<OrderDto> order, string orderName);
		Task<IEnumerable<Order>> FindOrder(string email);
		Task AddCakeToOrder(ObjectId id, OrderDto order);
	}
	}
