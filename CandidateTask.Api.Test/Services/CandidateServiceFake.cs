using CandidateTask.Application.IServices;
using CandidateTask.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Api.Test.Services
{
    public class CandidateServiceFake : ICandidateService
    {
        private readonly List<CandidateDto> _candidatMockList;

        public CandidateServiceFake()
        {
            _candidatMockList = new List<CandidateDto>()
            {
                new CandidateDto()
                {
                    Email = "test@test.com",
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                    PhoneNumber = "12345678",
                    TimeInterval = DateTime.Now,
                    GitHubProfileURL = "https://www.google.com/",
                    LinkedInProfileURL = "https://www.google.com/",
                    Comment = "test comment text"
                }
            };
        }

        public Task<CandidateDto> SaveAsync(CandidateDto candidateDto)
        {
            int index = _candidatMockList.FindIndex(c => c.Email == candidateDto.Email);
            if(index == -1)
            {
                _candidatMockList.Add(candidateDto);
            }
            else
            {
                _candidatMockList[index] = candidateDto;
            }
            return Task.FromResult(candidateDto);
        }
    }
}
