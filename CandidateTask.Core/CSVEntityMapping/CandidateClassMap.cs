using CandidateTask.Core.Entities;
using CsvHelper.Configuration;
using System.Xml.Linq;

namespace CandidateTask.Core.CSVEntityMapping
{
    public class CandidateClassMap: CsvClassMap<Candidate>
    {
        public CandidateClassMap()
        {
            Map(classMappElement => classMappElement.Id).Name("id").Index(0);
            Map(classMappElement => classMappElement.Email).Name("email").Index(1);
            Map(classMappElement => classMappElement.FirstName).Name("first_name").Index(2);
            Map(classMappElement => classMappElement.LastName).Name("last_name").Index(3);
            Map(classMappElement => classMappElement.PhoneNumber).Name("phone_number").Index(4);
            Map(classMappElement => classMappElement.TimeInterval).Name("time_interval").Index(5);
            Map(classMappElement => classMappElement.LinkedInProfileURL).Name("linkedin_profile_url").Index(6);
            Map(classMappElement => classMappElement.GitHubProfileURL).Name("gitHub_profile_url").Index(7);
            Map(classMappElement => classMappElement.Comment).Name("comment").Index(8);
        }
    }
}
