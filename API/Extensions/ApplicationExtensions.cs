using Application.Features.Users.Services;
using Application.Features.Users.Validators;
using FluentValidation;

namespace API.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();

            return services;
        }
    }
}
