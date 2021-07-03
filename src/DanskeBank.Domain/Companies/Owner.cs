using System;

namespace DanskeBank.Domain.Companies
{
    public class Owner : IEntity
    {
        public Owner(string name, string ssn)
        {
            Name = name;
            SSN = ssn;
        }

        public Owner()
        {
            Name = string.Empty;
            SSN = string.Empty;
        }

        public string Name { get; set; }
        public string SSN { get; set; }
    }
}
