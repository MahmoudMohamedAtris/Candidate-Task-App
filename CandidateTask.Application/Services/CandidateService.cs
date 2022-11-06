using CandidateTask.Application.IServices;
using CandidateTask.Core.Dtos;
using CandidateTask.Core.Entities;
using CandidateTask.Core.Interfaces;
using CandidateTask.Infrastructure.Caching.CachingProviders;
using System;
using System.Threading.Tasks;

namespace CandidateTask.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateCachingProvider _candidateCachingProvider;

        public CandidateService(ICandidateRepository candidateRepository, ICandidateCachingProvider CandidateCachingProvider)
        {
            _candidateRepository = candidateRepository;
            _candidateCachingProvider = CandidateCachingProvider;
        }

        public async Task<CandidateDto> SaveAsync(CandidateDto candidateDto)
        {
            var oldentity = await _candidateCachingProvider.GetCandidateByEmailAsync(candidateDto.Email);

            if (oldentity == null)
            {
                // create new one
                var newCandidateEntity = await Create(candidateDto);
                //Add new to cache
                await _candidateCachingProvider.AddCandidateToCacheAsync(newCandidateEntity);
            }
            else
            {
                // update old one 
                Candidate updatedCandidateEntity = await Update(oldentity, candidateDto);
                //update cache
                await _candidateCachingProvider.UpdateCandidateAtCacheAsync(updatedCandidateEntity);
            }
            return candidateDto;
        }

        #region Private Methods
        private async Task<Candidate> Create(CandidateDto candidateDto)
        {
            var entity = new Candidate
            {
                Id = Guid.NewGuid(),
                Email = candidateDto.Email,
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                PhoneNumber = candidateDto.PhoneNumber,
                GitHubProfileURL = candidateDto.GitHubProfileURL,
                LinkedInProfileURL = candidateDto.LinkedInProfileURL,
                TimeInterval = candidateDto.TimeInterval,
                Comment = candidateDto.Comment,
            };
       
            await Task.Run(() => _candidateRepository.Add(entity));
            return entity;
        }

        private async Task<Candidate> Update(Candidate oldCandidate, CandidateDto newCandidateDto)
        {
            oldCandidate.Email = newCandidateDto.Email;
            oldCandidate.FirstName = newCandidateDto.FirstName;
            oldCandidate.LastName = newCandidateDto.LastName;
            oldCandidate.PhoneNumber = newCandidateDto.PhoneNumber;
            oldCandidate.GitHubProfileURL = newCandidateDto.GitHubProfileURL;
            oldCandidate.LinkedInProfileURL = newCandidateDto.LinkedInProfileURL;
            oldCandidate.TimeInterval = newCandidateDto.TimeInterval;
            oldCandidate.Comment = newCandidateDto.Comment;

            await Task.Run(() => _candidateRepository.Update(oldCandidate));
            return oldCandidate;
        }
        #endregion
    }
}
