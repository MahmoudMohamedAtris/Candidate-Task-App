using CandidateTask.Core.Entities;
using CandidateTask.Core.Interfaces;
using CandidateTask.Infrastructure.Caching.CachingProviders;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CandidateTask.Infrastructure.Caching.ICachingProviders
{
    public class CandidateCachingProviders : ICandidateCachingProvider
    {
        private readonly IMemoryCache _cache;
        private readonly ICandidateRepository _candidateRepository;

        /// <summary>
        ///we use thread lock to help us control the number of threads that can access a resource concurrently. 
        /// </summary>
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);

        public CandidateCachingProviders(IMemoryCache cache, ICandidateRepository candidateRepository)
        {
            _cache = cache;
            _candidateRepository = candidateRepository;
        }
        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            List<Candidate> candidates = await GetAllCandidates();
            return candidates?.Where(c => c.Email == email).FirstOrDefault();
        }
        public async Task AddCandidateToCacheAsync(Candidate candidate)
        {
            List<Candidate> candidates = await GetAllCandidates();

            if (!candidates.Any(c => c.Email == candidate.Email))
            {
                //add new candidate 
                candidates.Add(candidate);
                await AddCandidatesToCache(candidates);
            }
        }
        public async Task UpdateCandidateAtCacheAsync(Candidate candidate)
        {
            List<Candidate> candidates = await GetAllCandidates();
            int index = candidates.FindIndex(c => c.Email == candidate.Email);
            if (index > -1)
            {
                //update candidate 
                candidates[index] = candidate;
                await AddCandidatesToCache(candidates);
            }
        }


        private async Task<List<Candidate>> GetAllCandidates()
        {
            List<Candidate> candidates;
            if (_cache.TryGetValue(CachKeyConfigs.CandidatesKey, out candidates))
            {
                // get from cache
                return candidates;
            }
            else
            {
                //get from repository
                candidates = _candidateRepository.GetAll();
                // add to cache
                await AddCandidatesToCache(candidates);
                return candidates;
            }
        }
        private async Task AddCandidatesToCache(List<Candidate> candidates)
        {
            try
            {
                //lock thread
                await GetUsersSemaphore.WaitAsync();

                // add data to cache ememory
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMilliseconds(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(CachKeyConfigs.CandidatesKey, candidates, cacheEntryOptions);
            }
            finally
            {
                // release lock
                GetUsersSemaphore.Release();
            }
        }
    }

}
