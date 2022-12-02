using WMS_Inventory_API_Client.Helpers;
using WMS_Inventory_API_Client.Models;
using WMS_Inventory_API_Client.Services.Interfaces;

namespace WMS_Inventory_API_Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/Accounts/";
        public AccountService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<Account>> FindAll()
        {
            var responseGet = await _client.GetAsync(BasePath);
            var response = await responseGet.ReadContentAsync<List<Account>>();
            return response;
        }
        public async Task<Account> FindOne(int id)
        {
            var request = BasePath + id.ToString();
            var responseGet = await _client.GetAsync(request);
            var response = await responseGet.ReadContentAsync<Account>();
            var account = new Account(response.Id, response.Name, response.Address1, response.Address2, response.City, response.State, response.ZipCode, response.Email, response.Password);
            return account;
        }

        public async Task<Account> FindEmail(string email)
        {
            Account? account = null;
            var request = BasePath + "Email/" + email;
            var responseGet = await _client.GetAsync(request);
            if (responseGet.IsSuccessStatusCode)
            {
                var response = await responseGet.ReadContentAsync<Account>();
                account = new Account(response.Id, response.Name, response.Address1, response.Address2, response.City, response.State, response.ZipCode, response.Email, response.Password);
                return account;
            }

            return account;
        }
    }
}