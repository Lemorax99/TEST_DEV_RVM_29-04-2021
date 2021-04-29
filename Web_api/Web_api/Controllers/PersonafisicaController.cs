using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Web_api.Contexts;
using Web_api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonafisicaController : ControllerBase
    {
        public AppDbContext context { get; }

        public PersonafisicaController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<PersonafisicaController>
        [HttpGet]
        public IEnumerable<Persona_fisica> Get()
        {
            return context.Tb_PersonasFisicas.ToList();
        }

        // GET api/<PersonafisicaController>/5
        [HttpGet("{id}")]
        public Persona_fisica Get(int id)
        {
            var persona = context.Tb_PersonasFisicas.FirstOrDefault(p => p.IdPersonaFisica==id);
            return persona;
        }

        // POST api/<PersonafisicaController>
        [HttpPost]
        public async Task Post([FromBody] Persona_fisica persona_fisica)
        {
            var parametros = new List<object>
                {
                    new SqlParameter("@Nombre",persona_fisica.Nombre),
                    new SqlParameter("@ApellidoPaterno",persona_fisica.ApellidoPaterno),
                    new SqlParameter("@ApellidoMaterno",persona_fisica.ApellidoMaterno),
                    new SqlParameter("@RFC",persona_fisica.RFC),
                    new SqlParameter("@FechaNacimiento",persona_fisica.FechaNacimiento),
                    new SqlParameter("@UsuarioAgrega",persona_fisica.UsuarioAgrega)
                };
            await context.Procedure_Post(parametros.ToArray());
        }

        // PUT api/<PersonafisicaController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Persona_fisica persona_fisica)
        {
            if (id == persona_fisica.IdPersonaFisica)
            {
                Console.WriteLine(persona_fisica);
                var parametros = new List<object>
                { 
                    new SqlParameter("@IdPersonaFisica",persona_fisica.IdPersonaFisica),
                    new SqlParameter("@Nombre",persona_fisica.Nombre),
                    new SqlParameter("@ApellidoPaterno",persona_fisica.ApellidoPaterno),
                    new SqlParameter("@ApellidoMaterno",persona_fisica.ApellidoMaterno),
                    new SqlParameter("@RFC",persona_fisica.RFC),
                    new SqlParameter("@FechaNacimiento",persona_fisica.FechaNacimiento),
                    new SqlParameter("@UsuarioAgrega",persona_fisica.UsuarioAgrega)
                };
                await context.Procedure_Put(parametros.ToArray());
            }
            
        }

        // DELETE api/<PersonafisicaController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var parametros = new List<object>
            {
                new SqlParameter("@IdPersonaFisica",id)
            };
            await context.Procedure_Del(parametros.ToArray());
        }
    }
}
