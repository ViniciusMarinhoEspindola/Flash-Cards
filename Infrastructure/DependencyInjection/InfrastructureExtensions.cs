using Domain.Interfaces;
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

            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ILanguage, LanguageRepository>();
            services.AddScoped<IUserLanguage, UserLanguageRepository>();
            services.AddScoped<ICard, CardRepository>();
            services.AddScoped<ICardExample, CardExampleRepository>();
            services.AddScoped<ICardProgress, CardProgressRepository>();
            services.AddScoped<IGrammarBook, GrammarBookRepository>();
            services.AddScoped<IGrammarChapter, GrammarChapterRepository>();
            services.AddScoped<IGrammarSection, GrammarSectionRepository>();

            return services;
        }
    }
}
