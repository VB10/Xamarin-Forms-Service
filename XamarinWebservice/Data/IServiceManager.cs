using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinWebservice.Model;

namespace XamarinWebservice.Data
{
    public interface IServiceManager
    {
        //gerekli metodlar
        HttpClient GetClient();
        Task<ObservableCollection<Book>> GetAll();
        Task Add(Book item);
        Task Update(Book item);
        Task Delete(string isbn);
    }
}
