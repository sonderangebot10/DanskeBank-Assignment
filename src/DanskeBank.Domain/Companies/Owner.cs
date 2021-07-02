using System;

namespace DanskeBank.Domain.Companies
{
    public class Owner : IEntity
    {
        public string Key { get; protected set; }
        public string Value { get; set; }
    }
}
