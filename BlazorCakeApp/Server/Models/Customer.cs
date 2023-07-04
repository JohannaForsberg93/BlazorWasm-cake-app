using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace BlazorCakeApp.Server.Models
	{
	public class Customer
		{
		[BsonId]
			public ObjectId Id { get; set; }
			[BsonElement]
		public string FirstName { get; set; } = string.Empty;
		[BsonElement]
		public string LastName { get; set; } = string.Empty;
		[BsonElement, EmailAddress]
		public string Email { get; set; } = string.Empty;

		[BsonElement, Phone] 
		public string Phone { get; set; } = string.Empty;

		[BsonElement]
		public string Address { get; set; } = string.Empty;
		[BsonElement]

		public DateTime Created = DateTime.Now;

		}
	}
