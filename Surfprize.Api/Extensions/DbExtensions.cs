using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Surfprize.DAL;

namespace Surfprize.Api.Extensions
{
    internal static class DbExtensions
    {
        internal static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SurfprizeDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
