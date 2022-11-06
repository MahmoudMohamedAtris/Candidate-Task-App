using CandidateTask.Core.Interfaces;
using CandidateTask.Infrastructure.RepositoriesCSV;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Infrastructure
{
    public static class IOC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            return services;
        }

    }
}
