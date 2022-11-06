using CandidateTask.Api.Controllers;
using CandidateTask.Api.Test.Services;
using CandidateTask.Application.IServices;
using CandidateTask.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CandidateTask.Api.Test.Controllers
{
    public class CandidatesControllerTest
    {
        private readonly CandidatesController _controller;
        private readonly ICandidateService _candidateService;
        public CandidatesControllerTest()
        {
            _candidateService = new CandidateServiceFake();
            _controller = new CandidatesController(_candidateService);
        }

        [Fact]
        public async Task Save_ValidObjectPassed_ReturnResponseOk()
        {
            // Arrange
            var testItem = new CandidateDto()
            {
                Email = "test2@test.com",
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                PhoneNumber = "12345678",
                TimeInterval = DateTime.Now,
                GitHubProfileURL = "https://www.google.com/",
                LinkedInProfileURL = "https://www.google.com/",
                Comment = "test2 comment text"
            };

            // Act
            var createdResponse = await _controller.Save(testItem);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public async Task Save_ValidObjectPassed_ReturneObject()
        {
            // Arrange
            var testItem = new CandidateDto()
            {
                Email = "test2@test.com",
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                PhoneNumber = "12345678",
                TimeInterval = DateTime.Now,
                GitHubProfileURL = "https://www.google.com/",
                LinkedInProfileURL = "https://www.google.com/",
                Comment = "test2 comment text"
            };


            // Act
            var respoonse = await _controller.Save(testItem) as OkObjectResult;
            var returnedCandidate = respoonse.Value as CandidateDto;

            // Assert
            Assert.IsType<CandidateDto>(returnedCandidate);
            Assert.Equal(testItem.Email, returnedCandidate.Email);
        }
    }
}
