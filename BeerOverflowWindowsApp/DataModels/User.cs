using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerOverflowWindowsApp.DataModels
{
    [Table("User")]
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UsersRatingToBar> UserRatings { get; set; }
    }
}
