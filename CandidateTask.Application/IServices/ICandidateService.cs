﻿using CandidateTask.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Application.IServices
{
    public interface ICandidateService
    {
        Task<CandidateDto> SaveAsync(CandidateDto candidateDto);
    }
}
