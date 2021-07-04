
using System;
using System.Collections.Generic;
using System.Linq;

namespace DanskeBank.Domain.Companies
{
    public class Company : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public IReadOnlyCollection<Owner> Owners => _owners.ToList();

        private readonly List<Owner> _owners;

        public Company(string name, string country, string phoneNumber)
            : this(Guid.Empty, name, country, phoneNumber, null)
        {

        }

        public Company(Guid id, string name, string country, string phoneNumber) 
            : this(id, name, country, phoneNumber, null)
        {

        }

        public Company(Guid id, string name, string country, string phoneNumber, List<Owner> owners)
        {
            if (owners == null)
            {
                _owners = new List<Owner>();
            }
            else
            {
                _owners = owners;
            }

            Id = id;
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