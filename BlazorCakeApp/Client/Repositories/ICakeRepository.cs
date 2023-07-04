using BlazorCakeApp.Shared.Dto;

namespace BlazorCakeApp.Client.Services
{
    public interface ICakeRepository
    {

        List<CakeDto> Cakes { get; set; }
        CakeDto CakeById { get; set; }


		Task GetAllCakes();

		Task GetCakeById(string id);

		Task DeleteCake(string id);

		Task UpdateCake(string id, CakeDto cake);

		Task AddNewCake(CakeDto cake);



		//Task<IEnumerable<CakeDto>> SearchCakeByName(string name);




		//Task<IEnumerable<Cake>> SearchCake(ObjectId id);

		//Task CakeStatus(Object id, bool available);

		}
	}
