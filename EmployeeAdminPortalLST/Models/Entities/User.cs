using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAdminPortalLST.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        public string Family { get; set; }

        [NotMapped]
        public string FullName { get; set; }
    }
}
