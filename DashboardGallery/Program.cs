using Bsn.DependencyResolverServices;
using Bsn.Utilities.Constants;
using DashboardGallery;
using DashboardGallery.Shared.Services.JsFunctions;
using DashboardGallery.Shared.Services.JsFunctions.Interfaces;
using Dashiell.Front.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HttpClient httpClient = new() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => httpClient);
using HttpResponseMessage response = await httpClient.GetAsync(Constant.AppSetting_Json);
using Stream? stream = await response.Content.ReadAsStreamAsync();
IConfigurationBuilder configBuilder = builder.Configuration.AddJsonStream(stream);
IConfigurationRoot config = configBuilder.Build();

builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<IJsFunctions, JsFunctions>();
builder.Services.AddI18nText(options => options.PersistanceLevel = Toolbelt.Blazor.I18nText.PersistanceLevel.SessionAndLocal);
builder.Services.AddKeys();
builder.Services.AddCustomHttpClient(config);
builder.Services.AddDataServices();
builder.Services.AddUtilities();
builder.Services.AddAuth();
await builder.Build().RunAsync();
