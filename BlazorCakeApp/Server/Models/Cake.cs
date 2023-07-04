using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorCakeApp.Server.Models
	{
	public class Cake
		{
		[BsonId]
		public ObjectId Id { get; set; }
			[BsonElement]
			public string CakeName { get; set; } = string.Empty;
			[BsonElement]
			public string Description { get; set; } = string.Empty;
			[BsonElement] 
			public string Ingredients { get; set; } = string.Empty;
		public int Price { get; set; }
			[BsonElement] 
			public string Allergens { get; set; } = string.Empty;
		[BsonElement]
		public bool Available { get; set; }
		}
	}
