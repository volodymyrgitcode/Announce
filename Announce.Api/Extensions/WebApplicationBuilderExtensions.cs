using Announce.Infrastructure;
using Announce.Application;
using Microsoft.OpenApi.Models;

namespace Announce.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //

       /* o =>
        {
            o.AddSecurityDefinition("bearerAuthentication", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuthentication"
                        }
                    },
                    []
                }
            });
        }*/

        builder.Services.AddAuthentication();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("all",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

    }
}
