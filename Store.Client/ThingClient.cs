using Store.Client.Models;
using System.Net.Http.Json;

namespace Store.Client
{
    public class ThingClient
    {
        private readonly HttpClient HttpClient;

        public ThingClient(HttpClient Http)
        {
            this.HttpClient = Http;
        }

        public async Task<Thing[]?> GetThingsAsync()
        {
            return await HttpClient.GetFromJsonAsync<Thing[]>("api/things");
        }

        public async Task<Thing> GetThingAsync(int id)
        {
            return await  HttpClient.GetFromJsonAsync<Thing>($"api/things/{id}") 
                ?? throw new Exception("Didnt find the Thing you are looking for");

        }

        public async Task AddThingAsync(Thing thing)
        {
            await HttpClient.PostAsJsonAsync("api/thing",thing);
        }

        public async Task UpdateThingASync(Thing UpdatedThing)
        {
            await HttpClient.PutAsJsonAsync($"api/thing", UpdatedThing);
        }

        public async Task DeleteThingAsync(int Id)
        {
            await HttpClient.DeleteAsync($"api/thing/{Id}");
        }

    }
}