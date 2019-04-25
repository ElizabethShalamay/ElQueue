using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ElQueue.Web.Filters;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ElQueue.Web.Configuration
{
    public class SwaggerConfig
    {
        internal static Action<SwaggerGenOptions> Register()
        {
            return c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ElQueue API", Version = "v1" });
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey",
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            };
        }
    }
}
