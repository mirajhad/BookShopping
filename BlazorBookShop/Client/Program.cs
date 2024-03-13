using BlazorBookShop.Client;
using BlazorBookShop.Client.Auth;
using BlazorBookShop.Client.Helpers;
using BlazorBookShop.Client.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
builder.Services.AddScoped<IUtilityService, UtilityService>();


builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<HelperAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, HelperAuthenticationProvider>(
    options=>options.GetRequiredService<HelperAuthenticationProvider>());

builder.Services.AddScoped<IAuthenticationService, HelperAuthenticationProvider>(
    options => options.GetRequiredService<HelperAuthenticationProvider>());

await builder.Build().RunAsync();
