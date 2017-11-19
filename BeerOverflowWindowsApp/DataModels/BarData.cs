using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerOverflowWindowsApp.DataModels
{
    [Table("Bar")]
    public class BarData : IEquatable<BarData>
    {
        public BarData() { }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Key, MaxLength(50)]
        public string BarId { get; set; }

        public float AvgRating { get; set; }

        [NotMapped]
        public int UserRating { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public virtual ICollection<UsersRatingToBar> UserRatings { get; set; }

        [NotMapped]
        public double DistanceToCurrentLocation { get; set; }

        public bool Equals(BarData other)
        {
            return this.Title == other.Title ? true : false;
        }
    }
}
