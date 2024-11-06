using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string xmlFileName)
        {
            // Configuracion Swagger
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc(configuration["Swagger:Version"], new OpenApiInfo
                {
                    Title = configuration["Swagger:Title"],
                    Version = configuration["Swagger:Version"]
                });

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);

                doc.AddSecurityDefinition(configuration["Swagger:SecurityName"], new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,                    
                    In = ParameterLocation.Header,
                    Name = configuration["Swagger:HeaderName"],
                    Description = configuration["Swagger:DescriptionToken"],
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });


                doc.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                   }
                });
            });

            return services;
        }

        public static IServiceCollection AddCorsApp(this IServiceCollection services)
        {
            // Configuracion CORS
            services.AddCors(options =>
            {
                options.AddPolicy("ApiCors", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    // Esto no va en produccion, sólo local
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Authorization"); // Expone el token para que las Apps lo puedan ver
                    // .AllowCredentials()
                });
            });

            return services;
        }

        public static IServiceCollection AddElasticLogging(this IServiceCollection services, IConfiguration configuration, string environment)
        {
            ConfigureLogging(configuration, environment);

            return services;
        }

        public static void ConfigureLogging(IConfiguration configuration, string environment)
        {
            // Configurar Serilog
            Log.Logger = new LoggerConfiguration()
                //.Enrich.WithElasticApmCorrelationInfo()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Debug()
                //.MinimumLevel.Verbose()
                .WriteTo.Debug()
                .WriteTo.Console(new ElasticsearchJsonFormatter())
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration)
        {
            string urlElastic = configuration["ElasticConfig:Url"];
            string indexFormat = configuration["ElasticConfig:IndexFormat"];
            //string templateName = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}";

            return new ElasticsearchSinkOptions(new Uri(urlElastic))
            {
                AutoRegisterTemplate = true,
                IndexFormat = indexFormat,
                //IndexFormat = templateName,
                NumberOfReplicas = 1,
                NumberOfShards = 2,
                //CustomFormatter = new EcsTextFormatter()
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
            };
        }
    }
}