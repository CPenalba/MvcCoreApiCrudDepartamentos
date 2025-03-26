using MvcCoreApiCrudDepartamentos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MvcCoreApiCrudDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceDepartamentos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiDepartamentos");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApisAsync<T>(string request)
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

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                List<Departamento> data = await this.CallApisAsync<List<Departamento>>(request);
                return data;
            }
        }

        public async Task<Departamento> FindDepartamentoAsync(int idDepartamento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos/" + idDepartamento;
                Departamento data = await this.CallApisAsync<Departamento>(request);
                return data;
            }
        }

        //LOS METODOS DE ACCION QUE RECIBEN OBJETOS PUEDEN SER GENERICOS Y RECIBIR T
        //SI LOS METODOS RECIBEN LA INFORMACION POR URL SI QUE NO SUELEN SER GENERICOS
        public async Task InsertDepartamentoAysnc(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //DEBEMOS CREAR NUESTRO MODEL A ENVIAR
                Departamento dept = new Departamento();
                dept.IdDepartamento = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;
                //CONVERTIMOS NUESTRO MODEL A JSON
                string json = JsonConvert.SerializeObject(dept);
                //PARA EVITAR DATOS SE UTILIZA STRINGCONTENT DONDE DEBEMOS ENVIAR EL JSON, EL FORMATO Y EL TIPO
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAysnc(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Departamento dept = new Departamento();
                dept.IdDepartamento = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;
                string json = JsonConvert.SerializeObject(dept);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteDepartamentoAsync(int idDepartamento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos/" + idDepartamento;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.DeleteAsync(request);

            }
        }
    }
}
