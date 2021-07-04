using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanskeBank.Infrastructure.Data.Sql.Companies.Entities
{
    [Table("owners")]
    public class Owner
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("ssn")]
        public string SSN { get; set; }
    }
}
