using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Surfprize.Api.Extensions
{
    internal static class AutoMapperExtensions
    {
        internal static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(new Assembly[] { typeof(AssemblyReference).Assembly });
        }
    }
}
