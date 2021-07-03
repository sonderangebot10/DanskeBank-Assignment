using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanskeBank.Infrastructure.Data.Sql.Companies.Entities
{
    [Table("companies")]
    public class Company
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("owners")]
        public List<Owner> Owners { get; set; }
    }
}
