using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BatchMasivoServisioMDM.Models;

namespace BatchMasivoServisioMDM
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var builderContext = new DbContextOptionsBuilder<IntegracionsContext>();
            builderContext.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            var context = new IntegracionsContext(builderContext.Options);

            DateTime inicio = new DateTime(2019, 01, 09, 08, 43, 13);
            DateTime fin = new DateTime(2019, 05, 15, 08, 43, 14);

            new ProcesarError(context, configuration).IniciarProceso(inicio, fin);
        }
    }
}
