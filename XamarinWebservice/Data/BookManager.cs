using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinWebservice.Model;

namespace XamarinWebservice.Data
{
    public class BookManager : IServiceManager  
    {
        //service url
        const string Url = "http://xam150.azurewebsites.net/api/books/";
        //auth olmak gerekiyor
        private string authorizationKey = "9319ce02-5f6e-4989-9c24-98439c1be00f";
        HttpClient client;
        public BookManager()
        {
            client = GetClient();
        }
        public HttpClient GetClient()
        {
            //client için gerekli ayarlar
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<ObservableCollection<Book>> GetAll(){
            //isteği atıp deseralize ederek istediğimiz yapıda geri döndüryoruz
            var response =await client.GetStringAsync(Url);
            var json = JsonConvert.DeserializeObject<ObservableCollection<Book>>(response);
            return json;
        }
        public async Task Add(Book item) {
            //itemi serilaze edip json formatına çekip service atıyoruz
            var jsonObject = JsonConvert.SerializeObject(item);
            var httpContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(Url, httpContent);
            Debug.WriteLine(result.IsSuccessStatusCode.ToString());    
        }

        public async Task Update(Book item)
        {
                //aynı item idsne put ile basıyoruz
                var jsonObject = JsonConvert.SerializeObject(item);
                var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                await client.PutAsync(Url + "/" + item.isbn,content);
        }
        public async Task Delete(string isbn)
        {
                //item id üzerinden siliyoruz
                await client.DeleteAsync(Url + "/" +isbn);
        }

    }
}
