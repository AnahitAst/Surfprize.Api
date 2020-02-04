using Microsoft.Extensions.DependencyInjection;
using Surfprize.DAL;
using Surfprize.Service.Classes;
using Surfprize.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surfprize.Api.Extensions
{
    internal static class ServiccExtensions
    {
        internal static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();           
        }
    }
}
