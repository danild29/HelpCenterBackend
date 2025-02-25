using Microsoft.OpenApi.Models;
using Serilog;

namespace HelpCenter.Api;

public static class ProgramConfigurations
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        string hostingDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "prod";
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // Create the Serilog logger
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Map(le => le.Timestamp.Date, (date, lc) => lc.File(
                Path.Combine(hostingDirectory, "serilog", $"{date:yyyy-MM-dd}", $"{currentEnv}-trace-.log"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] <{RequestId}> ({SourceContext}) {Method}#{LineNumber} > {Message:lj}{NewLine}{Exception}",
                shared: true,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
                retainedFileCountLimit: 24))
            .WriteTo.Map(le => le.Timestamp.Date, (date, lc) => lc.File(
                 Path.Combine(hostingDirectory, "serilog", $"{date:yyyy-MM-dd}", $"{currentEnv}-.log"),
                 rollingInterval: RollingInterval.Day,
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] <{RequestId}> ({SourceContext}) {Method}#{LineNumber} > {Message:lj}{NewLine}{Exception}",
                 shared: true,
                 restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information))
            .WriteTo.Console()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(async c =>
        {
            //c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //{
            //    Title = (string.IsNullOrEmpty(this.Configuration["SwaggerTitle"]) ? "Mobile back-end" : this.Configuration["SwaggerTitle"]),
            //    Version = "v1",
            //    Description = ThisAssembly.AppVersion
            //});
            //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath);
            //c.SchemaFilter<EnumSchemaFilter>();
            //c.OperationFilter<HandleHeaderAttribute>();
            //c.OperationFilter<ApiVersionOperationFilter>();

            // c.DescribeAllEnumsAsStrings();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                    new List<string>()
                    }
                });
            //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath);

            await Task.CompletedTask;
        });

        return services;
    }
}
