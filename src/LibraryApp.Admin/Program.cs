using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LibraryApp.Admin;
using LibraryApp.Admin.Auth;
using LibraryApp.Admin.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException("ApiBaseUrl is not configured.");

builder.Services.AddScoped<AdminPasswordMessageHandler>();
builder.Services.AddScoped<AdminAuthService>();

builder.Services.AddHttpClient("LibraryApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AddHttpMessageHandler<AdminPasswordMessageHandler>();

builder.Services.AddScoped<IBookApiClient, BookApiClient>();
builder.Services.AddScoped<IReaderApiClient, ReaderApiClient>();
builder.Services.AddScoped<ILoanApiClient, LoanApiClient>();
builder.Services.AddScoped<IReviewApiClient, ReviewApiClient>();

await builder.Build().RunAsync();
