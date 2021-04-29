using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Front_web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Front_web.Controllers
{
    public class DatosController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        DatosModel persona_fisica = new DatosModel();
        List<DatosModel> lista_datos = new List<DatosModel>();
        UsuarioModel usuario = new UsuarioModel();
        public DatosController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<List<DatosModel>> GetAllData()
        {
            lista_datos = new List<DatosModel>();
            var Token = PostToken();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                using (var response = await httpClient.GetAsync("https://api.toka.com.mx/candidato/api/customers/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lista_datos = JsonConvert.DeserializeObject<List<DatosModel>>(apiResponse);
                    return lista_datos;
                }
            }
        }
        [HttpPost]
        public async Task<ErrorViewModel> PostToken()
        {
            var token = new ErrorViewModel();
            usuario = new UsuarioModel();
            usuario.usuario = "ucand0021";
            usuario.password = "yNDVARG80sr@dDPc2yCT!";

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://api.toka.com.mx/candidato/api/login/authenticate", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject <ErrorViewModel>(apiResponse);
                }
            }

            return token;
        }
    }
}
