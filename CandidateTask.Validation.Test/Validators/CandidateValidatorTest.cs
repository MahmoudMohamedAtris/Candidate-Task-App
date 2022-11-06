using CandidateTask.Application.Validators;
using CandidateTask.Core.Dtos;
using FluentValidation.TestHelper;
using Xunit;

namespace CandidateTask.Validation.Test.Validators
{
    public class CandidateValidatorTest
    {
        private readonly CandidateValidator validator;
        public CandidateValidatorTest()
        {
            validator = new CandidateValidator();
        }

        [Fact]
        public void Should_have_errors_when_required_fields_is_null()
        {
            var candidateDto = new CandidateDto()
            {
                Email  = null,
                FirstName = null,
                LastName = null,
                Comment = null,
            };

            var result = validator.TestValidate(candidateDto);
            result.ShouldHaveValidationErrorFor(candidate => candidate.Email);
            result.ShouldHaveValidationErrorFor(candidate => candidate.FirstName);
            result.ShouldHaveValidationErrorFor(candidate => candidate.LastName);
            result.ShouldHaveValidationErrorFor(candidate => candidate.Comment);
        }  
        
        [Fact]
        public void Should_have_errors_when_required_fields_is_empty()
        {
            var candidateDto = new CandidateDto()
            {
                Email = "",
                FirstName = "",
                LastName = "",
                Comment = "",
            };

            var result = validator.TestValidate(candidateDto);
            result.ShouldHaveValidationErrorFor(candidate => candidate.Email);
            result.ShouldHaveValidationErrorFor(candidate => candidate.FirstName);
            result.ShouldHaveValidationErrorFor(candidate => candidate.LastName);
            result.ShouldHaveValidationErrorFor(candidate => candidate.Comment);
        }

        [Fact]
        public void Should_not_have_errors_when_required_fields_has_value()
        {
            var candidateDto = new CandidateDto()
            {
                Email  = "test@test",
                FirstName = "firstName",
                LastName = "lastName",
                Comment = "comment"
            };

            var result = validator.TestValidate(candidateDto);
            result.ShouldNotHaveValidationErrorFor(candidate => candidate.Email);
            result.ShouldNotHaveValidationErrorFor(candidate => candidate.FirstName);
            result.ShouldNotHaveValidationErrorFor(candidate => candidate.LastName);
            result.ShouldNotHaveValidationErrorFor(candidate => candidate.Comment);
        }
    }
}
