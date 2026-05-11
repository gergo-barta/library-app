using LibraryApp.API.Auth;
using LibraryApp.API.Middleware;
using LibraryApp.Application;
using LibraryApp.Application.Services;
using LibraryApp.Infrastructure;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.Configure<AdminPasswordOptions>(builder.Configuration.GetSection(AdminPasswordOptions.SectionName));
builder.Services.AddSingleton<IPasswordVerifier, PasswordHasher>();
builder.Services.AddSingleton<IAdminPasswordProvider, OptionsAdminPasswordProvider>();

const string CorsPolicyName = "LibraryAppCors";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
var allowedHeaders = builder.Configuration.GetSection("Cors:AllowedHeaders").Get<string[]>() ?? new[] { "Content-Type", "X-Admin-Password" };

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .WithHeaders(allowedHeaders)
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
