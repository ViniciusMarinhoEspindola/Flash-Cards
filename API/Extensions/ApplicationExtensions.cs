using Application.Features.Cards.Services;
using Application.Features.Cards.Validators;
using Application.Features.Decks.Services;
using Application.Features.Decks.Validators;
using Application.Features.Languages.Services;
using Application.Features.Study.Services;
using Application.Features.Users.Services;
using Application.Features.Users.Validators;
using Application.Features.Workspaces.Services;
using Application.Features.Workspaces.Validators;
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
            services.AddScoped<WorkspaceService>();
            services.AddScoped<DeckService>();
            services.AddScoped<CardService>();
            services.AddScoped<LanguageService>();
            services.AddScoped<StudyService>();

            return services;
        }
    }
}
