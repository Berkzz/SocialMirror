using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRofl.Models.Database
{
    public class User : IdentityUser<int>
    {
        [MaxLength(64)]
        public string FirstName { get; set; } = null!;
        [MaxLength(64)]
        public string LastName { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        [Column(TypeName="date")]
        public DateTime? Birth { get; set; }
        public Photo? MainPhoto { get; set; }
        public ICollection<Photo> Photos { get; set; } = null!;
        public ICollection<User> Following { get; set; } = null!;
        public ICollection<User> SubscribedTo { get; set; } = null!;
    }
}
