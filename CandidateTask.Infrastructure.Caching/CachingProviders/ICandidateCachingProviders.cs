using CandidateTask.Core.Entities;
using System.Threading.Tasks;

namespace CandidateTask.Infrastructure.Caching.CachingProviders
{
    public interface ICandidateCachingProvider
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task AddCandidateToCacheAsync(Candidate candidate);
        Task UpdateCandidateAtCacheAsync(Candidate candidate);
    }
}
