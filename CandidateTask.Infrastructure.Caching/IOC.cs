using CandidateTask.Core.Dtos;
using CandidateTask.Infrastructure.Caching.CachingProviders;
using CandidateTask.Infrastructure.Caching.ICachingProviders;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Infrastructure.Caching
{
    public static class IOC
    {
        public static IServiceCollection AddCache(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddScoped<ICandidateCachingProvider, CandidateCachingProviders>();

            return services;
        }
    }
}
