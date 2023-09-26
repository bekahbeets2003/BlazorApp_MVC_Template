using BlazorApp_MVC.Models;

namespace BlazorApp_MVC.Interfaces
{
    public interface IDbDapper
    {
        Task<athing> GetAthingAsync(int id);
        Task<IEnumerable<athing>> GetAthingsAsync();
    }
}