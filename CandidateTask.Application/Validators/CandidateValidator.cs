using CandidateTask.Core.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Application.Validators
{
    public class CandidateValidator : AbstractValidator<CandidateDto>
    {
        public CandidateValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("FirstName is madatory");
            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("LastName is madatory");
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email is madatory");
            RuleFor(x => x.Comment)
                .NotEmpty().NotNull().WithMessage("Comment is madatory");

        }
    }
}
