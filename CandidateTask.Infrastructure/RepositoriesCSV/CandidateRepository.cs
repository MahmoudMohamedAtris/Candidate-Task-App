using CandidateTask.Core.CSVEntityMapping;
using CandidateTask.Core.Entities;
using CandidateTask.Core.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CandidateTask.Infrastructure.RepositoriesCSV
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly string filePath = Path.Combine(Environment.CurrentDirectory, "CandidateDataFile.csv");

        public List<Candidate> GetAll()
        {
            List<Candidate> candidates = new List<Candidate>();
            try
            {
                var configs = new CsvConfiguration()
                {
                    CultureInfo = CultureInfo.InvariantCulture,
                };

                using (var streamReader = new StreamReader(filePath))
                using (var reader = new CsvReader(streamReader, configs))
                {
                    reader.Configuration.RegisterClassMap<CandidateClassMap>();
                    candidates = reader.GetRecords<Candidate>().ToList();
                }
            }
            catch (CsvReaderException ex)
            {
                if (ex.Message == "No header record was found.")
                {
                    //if file is empty and does not has header
                    AddFileHeader();
                }
            }
            return candidates;
        }

        public Candidate GetByEmail(string email)
        {
            IEnumerable<Candidate> candidates = GetAll();
            Candidate candidate = candidates.Where(c => c.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return candidate;
        }

        public void Add(Candidate entity)
        {
            var configs = new CsvConfiguration()
            {
                HasHeaderRecord = false,
                CultureInfo = CultureInfo.InvariantCulture,
            };

            using (var stream = File.Open(filePath, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, configs))
            {
                csv.Configuration.RegisterClassMap<CandidateClassMap>();
                csv.WriteRecord(entity);
            }
        }

        public void Update(Candidate entity)
        {
            List<Candidate> candidates = GetAll();

            var configs = new CsvConfiguration()
            {
                HasHeaderRecord = true,
                CultureInfo = CultureInfo.InvariantCulture,
            };

            var index = candidates.FindIndex(i => i.Email == entity.Email);
            candidates[index] = entity;

            using (var stream = File.Open(filePath, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, configs))
            {
                csv.Configuration.RegisterClassMap<CandidateClassMap>();
                csv.WriteHeader<Candidate>();
                csv.WriteRecords(candidates);
            }
        }

        #region Private Methods
        private void AddFileHeader()
        {
            var configs = new CsvConfiguration()
            {
                HasHeaderRecord = true,
                CultureInfo = CultureInfo.InvariantCulture,
            };

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, configs))
            {
                csv.Configuration.RegisterClassMap<CandidateClassMap>();
                csv.WriteHeader<Candidate>();
            }
        }
        #endregion
    }
}
