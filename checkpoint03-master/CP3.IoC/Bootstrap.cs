using CP3.Application.Services;
using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP3.IoC
{
    public class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBarcoRepository, BarcoRepository>();
            services.AddScoped<IBarcoApplicationService, BarcoApplicationService>();

            services.AddDbContext<ApplicationContext>(x => {
                x.UseOracle(configuration["Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=RM99491;Password=100205"]);
            });


        }

    }
}
