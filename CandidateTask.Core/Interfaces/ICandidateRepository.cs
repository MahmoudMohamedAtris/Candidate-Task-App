using CandidateTask.Core.CSVEntityMapping;
using CandidateTask.Core.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTask.Core.Interfaces
{
    public interface ICandidateRepository
    {
        List<Candidate> GetAll();
        Candidate GetByEmail(string email);
        void Add(Candidate entity);
        void Update(Candidate entity);
    }
}
