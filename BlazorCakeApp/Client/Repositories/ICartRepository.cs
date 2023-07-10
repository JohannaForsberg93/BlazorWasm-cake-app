using BlazorCakeApp.Shared.Dto;

namespace BlazorCakeApp.Client.Repositories
	{
	public interface ICartRepository
	{
		event Action OnChange;
		Task AddToCart( CakeDto cake);
	}
	}
