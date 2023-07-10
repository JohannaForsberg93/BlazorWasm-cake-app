using BlazorCakeApp.Client.Services;
using BlazorCakeApp.Shared.Dto;
using Blazored.LocalStorage;

namespace BlazorCakeApp.Client.Repositories
	{
	public class CartRepository : ICartRepository
	{
		private readonly ILocalStorageService _localStorage;
		private readonly ICakeRepository _cakeRepository;
		public CartRepository(ILocalStorageService localStorage, ICakeRepository cakeRepository)
		{
			_localStorage = localStorage;
			_cakeRepository = cakeRepository;
		}

			public event Action? OnChange;
			public async Task AddToCart(CakeDto cake)
			{
			var cart = await _localStorage.GetItemAsync<List<OrderDto>>("cart");

			if (cart == null)
			{
				cart = new List<OrderDto>();
				cart.Add(new OrderDto { Cake = cake.CakeName, Amount = 1, Price = cake.Price });
				await _localStorage.SetItemAsync("cart", cart);
			}
			else
			{
				cart.Add(new OrderDto { Cake = cake.CakeName, Amount = 1, Price = cake.Price });
				await _localStorage.SetItemAsync("cart", cart);
			}
			OnChange.Invoke();
			}
		}
	}
