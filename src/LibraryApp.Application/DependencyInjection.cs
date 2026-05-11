using LibraryApp.Application.Configuration;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LateFeeOptions>(configuration.GetSection(LateFeeOptions.SectionName));

        services.AddScoped<ILateFeeService, LateFeeService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IReaderService, ReaderService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
