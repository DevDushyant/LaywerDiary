using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface IExternalAPIReadingService
    {
        Task<T> GetAPIData<T>(string url, string method, string authheader);
    }
}
