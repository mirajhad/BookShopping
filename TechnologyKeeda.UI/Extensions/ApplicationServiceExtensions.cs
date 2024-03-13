using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Repositories;
using TechnologyKeeda.Repositories.Implementations;
using TechnologyKeeda.Repositories.Interfaces;

namespace TechnologyKeeda.UI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
           services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
             b => b.MigrationsAssembly("TechnologyKeeda.UI")));

            //services.AddScoped<ICountryRepo, CountryRepo>();
            services.AddScoped<ICountryRepo, CountryRepository>();
           // services.AddScoped<IStateRepo, StateRepo>();
            services.AddScoped<IStateRepo, StateRepository>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<ISkillRepo, SkillRepo>();
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;

        }

    }
}
