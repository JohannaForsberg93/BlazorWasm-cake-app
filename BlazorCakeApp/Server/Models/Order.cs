using BlazorCakeApp.Shared.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorCakeApp.Server.Models
{
    public class Order
		{
		[BsonId]
public ObjectId Id { get; set; }

		public DateTime Created = DateTime.Now;

		[BsonElement] 
		public string CustomerEmail { get; set; } = string.Empty;
		[BsonElement]
		public ICollection<OrderDto> Cakes { get; set; } = new List<OrderDto>();
		}
	}
