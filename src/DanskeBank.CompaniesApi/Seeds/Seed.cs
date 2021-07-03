using System;
using System.Collections.Generic;

namespace DanskeBank.Infrastructure.Data.Sql.Companies.Seeds
{
    public static class Seed
    {
        public static void SeedData (this Context context)
        {
            var companies = new List<Entities.Company>
            {
                new Entities.Company
                {
                    Name = "Microsoft",
                    Country = "United States of America",
                    PhoneNumber = "+61651564874",
                    Owners = new List<Entities.Owner> 
                    {
                        new Entities.Owner
                        {
                            SSN = "111-22-1212",
                            Name = "John"
                        },
                        new Entities.Owner
                        {
                            SSN = "575-27-5243",
                            Name = "Albert"
                        },
                        new Entities.Owner
                        {
                            SSN = "141-25-1755",
                            Name = "Anthony"
                        }
                    }
                },
                new Entities.Company
                {
                    Name = "Google",
                    Country = "United States of America",
                    PhoneNumber = "+12165454844651",
                    Owners = new List<Entities.Owner> 
                    {
                        new Entities.Owner
                        {
                            SSN = "742-22-4554",
                            Name = "Jeff"
                        },
                        new Entities.Owner
                        {
                            SSN = "575-87-5667",
                            Name = "Mandar"
                        },
                        new Entities.Owner
                        {
                            SSN = "225-41-3369",
                            Name = "Kevin"
                        }
                    }
                },
                new Entities.Company
                {
                    Name = "Danske Bank",
                    Country = "Denmark",
                    PhoneNumber = "+26654564789",
                    Owners = new List<Entities.Owner>
                    {
                        new Entities.Owner
                        {
                            SSN = "722-97-3654",
                            Name = "Barry"
                        }
                    }
                }
            };

            context.Companies.AddRange(companies);

            context.SaveChanges();
        }
    }
}
