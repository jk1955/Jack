using WMS_Inventory_API_Client.Models;
namespace WMS_Inventory_API_Client.Services.Interfaces
{
    public interface IContainerService
    {
        Task<IEnumerable<Container>> FindAll();
        Task<IEnumerable<Container>> Account(int id);
        Task<Container> FindOne(int id);
    }
}