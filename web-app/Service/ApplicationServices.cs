

using Microsoft.Extensions.DependencyInjection;
using Bizpack.Data.Configuration;
using Bizpack.Service.Account;
using Bizpack.Service.ApiResponse;

namespace Bizpack.Service {

    public static class ServiceInvoker {
        public static void InvokeApplicationServices (this IServiceCollection services) {
            // Can add application services here. Ex:
            // services.AddTransient<ICheckUser, CheckUser>();

            services.AddSingleton<IAppConfiguration, AppConfiguration> ();
            services.AddScoped<ValidationFilterAttribute> ();
            services.AddScoped<ISeederService, SeederService> ();
            services.AddScoped<IJwtValidatorService, JwtValidatorService> ();
            services.AddScoped<IUserVerificationService, UserVerificationService> ();
            services.AddScoped<IJwtService, JwtService> ();

        }
    }
}