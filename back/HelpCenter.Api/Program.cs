using AspNetCore.Identity.Extensions;
using HelpCenter.Api;
using HelpCenter.Api.Database;
using HelpCenter.Api.EndPoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

const string AllPolicyName = "AllPolicy";

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Services
        .ConfigureLogging()
        .ConfigureSwagger();

    builder.Services.AddSerilog(Log.Logger);

    builder.Services.AddCors(o => o.AddPolicy(AllPolicyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

    builder.Services.AddAuthentication()
        .AddBearerToken(IdentityConstants.BearerScheme);

    builder.Services.AddAuthorizationBuilder();

    builder.Services.AddIdentityCore<User>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddApiEndpoints();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

    WebApplication app = builder.Build();
    app.UseSerilogRequestLogging();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.ApplyMigrations();
        app.UseExceptionHandler("/Error");
    }

    //app.UseHttpsRedirection();
    app.MapIdentityApi<User>();

    app.AddEndpoints();
    _ = app.UseCors(AllPolicyName);
    Log.Logger.Information("App start");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Logger.Error(ex, "Error in program");
}
