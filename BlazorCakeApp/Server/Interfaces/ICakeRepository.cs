using BlazorCakeApp.Server.Models;
using BlazorCakeApp.Shared.Dto;
using MongoDB.Bson;

namespace BlazorCakeApp.Server.Interfaces
	{
	public interface ICakeRepository
		{
			Task<List<CakeDto>> GetAllCakes();
			Task<CakeDto> CakeById(string id);

			Task AddNewCake(CakeDto cake);
			Task DeleteCake(string id);
			Task UpdateCake(string id, CakeDto cake);

			Task<IEnumerable<Cake>> SearchCakeByName(string name);
			Task CakeStatus(Object id, bool available);


		}
	}
