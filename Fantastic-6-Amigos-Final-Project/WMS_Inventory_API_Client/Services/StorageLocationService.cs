using WMS_Inventory_API_Client.Helpers;
using WMS_Inventory_API_Client.Models;
using WMS_Inventory_API_Client.Services.Interfaces;

namespace WMS_Inventory_API_Client.Services
{
    public class StorageLocationService : IStorageLocationService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/StorageLocations/";

        public StorageLocationService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<StorageLocation>> FindAll()
        {
            var responseGet = await _client.GetAsync(BasePath);

            var response = await responseGet.ReadContentAsync<List<StorageLocation>>();

            return response;
        }

        public async Task<IEnumerable<StorageLocation>> Account(int id)
        {
            var request = BasePath + "Account/" + id.ToString();
            var responseGet = await _client.GetAsync(request);
            var response = await responseGet.ReadContentAsync<List<StorageLocation>>();
            return response;
        }

        public async Task<StorageLocation> FindOne(int id)
        {
            var request = BasePath + id.ToString();
            var responseGet = await _client.GetAsync(request);

            var response = await responseGet.ReadContentAsync<StorageLocation>();

            var storageLocation = new StorageLocation(response.Id, response.LocationName, response.Address1, response.Address2, response.City, response.State, response.ZipCode, response.Longitude, response.Latitude, response.AccountId, response.container);

            return storageLocation;
        }
    }
}