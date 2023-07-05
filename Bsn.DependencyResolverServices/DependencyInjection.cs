using Bsn.Authentication;
using Bsn.Authentification;
using Bsn.DataServices;
using Bsn.DataServices.Interfaces;
using Bsn.MapperService;
using Bsn.RestServices;
using Bsn.Utilities.Configuration;
using Bsn.Utilities.Configuration.Interfaces;
using Bsn.Utilities.Constants;
using Bsn.Utilities.LocalStorage;
using Bsn.Utilities.LocalStorage.Interfaces;
using Bsn.Utilities.Navigation;
using Bsn.Utilities.Navigation.Interfaces;
using Bsn.Utilities.Token;
using Bsn.Utilities.Token.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bsn.DependencyResolverServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, IConfigurationRoot config)
        {

            services.AddHttpClient<IRest, ApiRestService>(client =>
            {
                client.BaseAddress = new Uri(config[Constant.ApiUrl]);
                client.DefaultRequestHeaders.Add(Constant.Header_Key, config[Constant.ApiKey]);

            });
            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddSingleton(MapperConfigProfiles.GetProfileConfig().CreateMapper());
            services.AddScoped<IAuthServices,AuthService>();
            services.AddScoped<IImageFileServicies,ImageFileServicies>();
            services.AddScoped<IAlbumServices, AlbumServicies>();
            return services;
        }

        public static IServiceCollection AddUtilities(this IServiceCollection services)
        {
            services.AddScoped<INavigationService, NavigationService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddScoped<IConfigurationKeys,ConfigurationKeys>();
            services.AddScoped<IKeys,Keys>();   
            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddScoped<AuthentificationStateService>();
            services.AddScoped<AuthenticationStateProvider, AuthentificationStateService>(
                provider => provider.GetRequiredService<AuthentificationStateService>());
            services.AddScoped<IAuthentificationStateService, AuthentificationStateService>(
                provider => provider.GetRequiredService<AuthentificationStateService>());
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
        public static IServiceCollection AddKeys(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationKeys, ConfigurationKeys>();
            services.AddScoped<IKeys, Keys>();
            return services;
        }
    }
}
