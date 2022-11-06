using CandidateTask.Core.Entities;
using CandidateTask.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Infrastructure.RepositoriesEFCore
{
    public class CandidateRepository : ICandidateRepository
    {
        public void Add(Candidate entity)
        {
            throw new NotImplementedException();
        }

        public List<Candidate> GetAll()
        {
            throw new NotImplementedException();
        }

        public Candidate GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void Update(Candidate entity)
        {
            throw new NotImplementedException();
        }
    }
}
