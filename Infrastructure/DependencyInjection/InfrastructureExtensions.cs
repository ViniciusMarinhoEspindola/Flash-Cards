using Application.Contracts.AI;
using Application.Contracts.Common;
using Domain.Interfaces;
using Infraestructure.ExternalServices;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

            // Repositories
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ILanguage, LanguageRepository>();
            services.AddScoped<IWorkspace, WorkspaceRepository>();
            services.AddScoped<IDeck, DeckRepository>();
            services.AddScoped<ICard, CardRepository>();
            services.AddScoped<ICardExample, CardExampleRepository>();
            services.AddScoped<ICardProgress, CardProgressRepository>();
            services.AddScoped<IGrammarBook, GrammarBookRepository>();
            services.AddScoped<IGrammarChapter, GrammarChapterRepository>();
            services.AddScoped<IGrammarSection, GrammarSectionRepository>();

            // External services
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
            services.AddScoped<IAiService, OpenAiService>();

            return services;
        }
    }
}
