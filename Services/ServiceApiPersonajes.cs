using System.Net.Http.Headers;

namespace MvcPracticaPersonajes.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiPersonajes(IConfiguration config)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = config.GetValue<string>
                ("ApiUrls:ApiPersonajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

    }
}
