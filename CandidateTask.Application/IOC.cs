using CandidateTask.Application.IServices;
using CandidateTask.Application.Services;
using CandidateTask.Application.Validators;
using CandidateTask.Core.Dtos;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateTask.Application
{
    public static class IOC
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IValidator<CandidateDto> , CandidateValidator>();

            return services;
        }
    }
}
