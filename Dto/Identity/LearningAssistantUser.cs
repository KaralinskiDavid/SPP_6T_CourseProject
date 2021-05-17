using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace Dto.Identity
{
    public class LearningAssistantUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Column("IsDeleted", TypeName = "Bit")]
        public bool? IdDeleted { get; set; }
    }
}
