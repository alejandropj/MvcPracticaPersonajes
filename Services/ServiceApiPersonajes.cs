using MvcPracticaPersonajes.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        public async Task<List<PersonajesSeries>>
            GetPersonajesSeriesAsync()
        {
            string request = "api/personajes";
            List<PersonajesSeries> data = await this.CallApiAsync
                <List<PersonajesSeries>>(request);
            return data;
        }           
        public async Task<List<string>>
            GetSeriesAsync()
        {
            string request = "api/personajes/series";
            List<string> data = await this.CallApiAsync
                <List<string>>(request);
            return data;
        }        
        public async Task<PersonajesSeries>
            FindPersonajeSerieAsync(int id)
        {
            string request = "api/personajes/"+id;
            PersonajesSeries data = await this.CallApiAsync
                <PersonajesSeries>(request);
            return data;
        }        
        public async Task<List<PersonajesSeries>>
            FindPersonajesBySerieAsync(string serie)
        {
            string request = "api/personajes/personajesseries/"+serie;
            List<PersonajesSeries> data = await this.CallApiAsync
                <List<PersonajesSeries>>(request);
            return data;
        }

        public async Task CreatePersonajeSerieAsync
            (int id, string nombre, string imagen, string serie)
        {
            string request = "api/personajes/insertpersonaje";
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                PersonajesSeries personajesSeries = new PersonajesSeries();
                personajesSeries.IdPersonaje = id;
                personajesSeries.Nombre = nombre;
                personajesSeries.Imagen = imagen;
                personajesSeries.Serie = serie;
                string json = JsonConvert.SerializeObject(personajesSeries);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }        
        public async Task UpdatePersonajeSerieAsync
            (int id, string nombre, string imagen, string serie)
        {
            string request = "api/personajes/updatepersonaje";
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                PersonajesSeries personajesSeries = new PersonajesSeries();
                personajesSeries.IdPersonaje = id;
                personajesSeries.Nombre = nombre;
                personajesSeries.Imagen = imagen;
                personajesSeries.Serie = serie;
                string json = JsonConvert.SerializeObject(personajesSeries);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }        
        public async Task DeletePersonajeSerieAsync
            (int id)
        {
            string request = "api/personajes/deletepersonaje/"+id;
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

    }
}
