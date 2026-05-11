using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LibraryApp.Reader;
using LibraryApp.Reader.Services;
using LibraryApp.Reader.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException("ApiBaseUrl is not configured.");

builder.Services.AddHttpClient("LibraryApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddScoped<ReaderSessionState>();
builder.Services.AddScoped<IReaderPublicApiClient, ReaderPublicApiClient>();
builder.Services.AddScoped<IBookPublicApiClient, BookPublicApiClient>();
builder.Services.AddScoped<ILoanPublicApiClient, LoanPublicApiClient>();
builder.Services.AddScoped<IReviewPublicApiClient, ReviewPublicApiClient>();

await builder.Build().RunAsync();
