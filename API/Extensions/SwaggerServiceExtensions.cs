using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();

            return builder.Services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(); // API Documentation
            app.UseSwaggerUI();

            return app;
        }
    }
}