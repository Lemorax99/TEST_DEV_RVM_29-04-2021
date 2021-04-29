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
    public class PersonaFisicaController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        PersonaFisicaModel persona_fisica = new PersonaFisicaModel();
        List<PersonaFisicaModel> list_persona_fisica = new List<PersonaFisicaModel>();

        public PersonaFisicaController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<List<PersonaFisicaModel>> GetAll()
        {
            list_persona_fisica = new List<PersonaFisicaModel>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Personafisica"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    list_persona_fisica = JsonConvert.DeserializeObject<List<PersonaFisicaModel>>(apiResponse);
                    return list_persona_fisica;
                }
            }
            
        }
        [HttpGet]
        public async Task<PersonaFisicaModel> GetById(int IdPersonaFisica)
        {
            persona_fisica = new PersonaFisicaModel();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/Personafisica" + IdPersonaFisica))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    persona_fisica = JsonConvert.DeserializeObject<PersonaFisicaModel>(apiResponse);
                }
            }

            return persona_fisica;
        }
        [HttpPost]
        public async Task<PersonaFisicaModel> AddUpdatePersonaFisica(PersonaFisicaModel PersonaFisica)
        {
            persona_fisica = new PersonaFisicaModel();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(PersonaFisica), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:5000/api/Personafisica", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    persona_fisica = JsonConvert.DeserializeObject<PersonaFisicaModel>(apiResponse);
                }
            }

            return persona_fisica;
        }
        [HttpDelete]
        public async Task<string> Delete(int IdPersonaFisica)
        {
            string message = "";

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5000/api/Personafisica" + IdPersonaFisica))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }

            return message;
        }
        public IActionResult PersonaFisica()
        {
            return View();
        }
        public IActionResult Lista()
        {
            return View();
        }
    }
}
