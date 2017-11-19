using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerOverflowWindowsApp.DataModels
{
    [Table("User")]
    public class User
    {
        [Key, MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        public virtual ICollection<UsersRatingToBar> UserRatings { get; set; }
    }
}
