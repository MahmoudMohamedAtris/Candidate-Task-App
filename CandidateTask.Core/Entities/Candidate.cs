﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Core.Entities
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime TimeInterval { get; set; }
        public string LinkedInProfileURL { get; set; }
        public string GitHubProfileURL { get; set; }
        public string Comment{ get; set; }
    }
}
