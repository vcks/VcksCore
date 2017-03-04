using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VcksCore.DAL.Entities
{
    public class VcksUserProfile
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public bool Deleted { get; set; }
        public virtual Avatar Avatar { get; set; }
    }
}
