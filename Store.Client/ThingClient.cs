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
        // GET ALL
        public async Task<Thing[]?> GetThingsAsync(string? filter)
        {
            // Sending the filter to the API as query
            return await HttpClient.GetFromJsonAsync<Thing[]>($"api/things?filter={filter}");
        }
        // GET one
        public async Task<Thing> GetThingAsync(int id)
        {
            return await  HttpClient.GetFromJsonAsync<Thing>($"api/things/{id}") 
                ?? throw new Exception("Didnt find the Thing you are looking for");

        }
        // POST 
        public async Task AddThingAsync(Thing thing)
        {
            await HttpClient.PostAsJsonAsync("api/thing",thing);
        }
        // PUT 
        public async Task UpdateThingASync(Thing UpdatedThing)
        {
            await HttpClient.PutAsJsonAsync($"api/thing", UpdatedThing);
        }
        // DELETE
        public async Task DeleteThingAsync(int Id)
        {
            await HttpClient.DeleteAsync($"api/thing/{Id}");
        }

    }
}