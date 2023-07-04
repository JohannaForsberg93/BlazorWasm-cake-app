using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BlazorCakeApp.Shared.Dto;
using Microsoft.AspNetCore.Components;
using MongoDB.Bson.IO;

namespace BlazorCakeApp.Client.Services
{
    public class CakeRepository : ICakeRepository
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManger;
        public CakeRepository(HttpClient http, NavigationManager navigationManger)
        {
            _http = http;
            _navigationManger = navigationManger;

        }

        public List<CakeDto> Cakes { get; set; } = new List<CakeDto>();
		public CakeDto CakeById { get; set; }



		public async Task GetAllCakes()
        {

	        var response = await _http.GetAsync($"cake/all");

	        if (response.IsSuccessStatusCode)
	        {
		        Cakes = await response.Content.ReadFromJsonAsync<List<CakeDto>>();
	        }
	        else if (response.StatusCode == HttpStatusCode.NotFound)
	        {
		        Console.WriteLine("Något gick fel");

				}


			}

        public async Task GetCakeById(string id)
        {
	        var response = await _http.GetAsync($"cake/{id}");

	        if (response.IsSuccessStatusCode)
	        {
		        CakeById = await response.Content.ReadFromJsonAsync<CakeDto>();
	        }
	        else if (response.StatusCode == HttpStatusCode.NotFound)
	        {
		        Console.WriteLine("Hittade ingen sån tårta");
	        }

            }

        public async Task DeleteCake(string id)
        {
			var response = await _http.DeleteAsync(($"cake/remove/{id}"));

			if (response.IsSuccessStatusCode)
			{
				Cakes.Remove(Cakes.First(c => c.Id == id));
			}
			else if (response.StatusCode == HttpStatusCode.NotFound)
			{
				Console.WriteLine("Något gick fel");

			}
			}

		public async Task UpdateCake(string id, CakeDto cake)
			{
			var json = JsonSerializer.Serialize(cake);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _http.PutAsync($"cake/update/{id}", content);

			if (response.IsSuccessStatusCode)
				{
				Console.WriteLine("Lyckades uppdatera tårta!");
				}
			else
				{
				Console.WriteLine("Något gick fel");

				}
			}


		public async Task AddNewCake(CakeDto cake)
		{

			var response =  await _http.PostAsJsonAsync($"cake/add", cake);

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Lyckades lägga till en ny tårta!");
			}
			else
			{
				Console.WriteLine("Något gick fel");

			}
			}

		}
	}
