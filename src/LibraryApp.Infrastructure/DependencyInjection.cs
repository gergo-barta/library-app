using LibraryApp.Domain.Interfaces;
using LibraryApp.Infrastructure.Data;
using LibraryApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=library.db";

        services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IReaderRepository, ReaderRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        return services;
    }
}
