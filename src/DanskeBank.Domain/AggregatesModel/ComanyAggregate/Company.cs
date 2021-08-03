using DanskeBank.Domain.OwnerAggregate;
using DanskeBank.Domain.SeedWork;
using System.Collections.Generic;

namespace DanskeBank.Domain.CompanyAggregate
{
    public class Company : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        // Should ideally just return _owners, but for easier testing...
        public List<Owner> Owners { get; set; }

        private readonly List<Owner> _owners;

        public Company(string name, string country, string phoneNumber) 
            : this(name, country, phoneNumber, new List<Owner>())
        {

        }

        public Company(string name, string country, string phoneNumber, List<Owner> owners)
        {
            if (owners == null)
            {
                _owners = new List<Owner>();
            }
            else
            {
                _owners = owners;
            }

            Name = name;
            Country = country;
            PhoneNumber = phoneNumber;
        }

        public Company()
        {
            Name = string.Empty;
            Country = string.Empty;
            PhoneNumber = string.Empty;
            _owners = new List<Owner>();
        }

        public void AddOwner(Owner newOwner)
        {
            _owners.Add(newOwner);
        }
    }
}