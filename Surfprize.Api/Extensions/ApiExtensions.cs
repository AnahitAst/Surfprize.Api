using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Surfprize.Api.Extensions
{
    internal static class ApiExtensions
    {
        internal static void ConfigureApi(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
            }).AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                options.LocalizationEnabled = false;
                //options.RegisterValidatorsFromAssemblyContaining(typeof(Surfprize.));
            });
        }
    }
}
