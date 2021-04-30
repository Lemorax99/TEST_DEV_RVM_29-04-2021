 using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_api.Models;

namespace Web_api.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Persona_fisica> Tb_PersonasFisicas { get; set; }

        public async Task Procedure_Put(object[] parametros)
        {
            await Database.ExecuteSqlCommandAsync(@"exec sp_ActualizarPersonaFisica @IdPersonaFisica,@Nombre,@ApellidoPaterno,@ApellidoMaterno,@RFC,@FechaNacimiento,@UsuarioAgrega", parametros);
        }
        public async Task Procedure_Post(object[] parametros)
        {
            await Database.ExecuteSqlCommandAsync(@"exec [sp_AgregarPersonaFisica]@Nombre,@ApellidoPaterno,@ApellidoMaterno,@RFC,@FechaNacimiento,@UsuarioAgrega", parametros);
        }
        public async Task Procedure_Del(object[] parametros)
        {
            await Database.ExecuteSqlCommandAsync(@"exec [sp_EliminarPersonaFisica] @IdPersonaFisica",parametros);
        }
    }
}
