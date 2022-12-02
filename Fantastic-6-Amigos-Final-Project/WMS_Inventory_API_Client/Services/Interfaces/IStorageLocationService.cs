using WMS_Inventory_API_Client.Models;
namespace WMS_Inventory_API_Client.Services.Interfaces
{
    public interface IStorageLocationService
    {
        Task<IEnumerable<StorageLocation>> FindAll();
        Task<IEnumerable<StorageLocation>> Account(int id);
        Task<StorageLocation> FindOne(int id);
    }
}