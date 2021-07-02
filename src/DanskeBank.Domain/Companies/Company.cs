
namespace DanskeBank.Domain.Companies
{
    public class Company : IEntity
    {
        public string Key { get; protected set; }
        public string Value { get; set; }
    }
}
