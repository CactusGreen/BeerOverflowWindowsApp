using WebApi.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.DataModels
{
    [Table("Bar")]
    public class BarData : IEquatable<BarData>
    {
        public BarData() { }

        public string Title { get; set; }
        [Key]
        public string BarId { get; set; }
        public List<int> Ratings { get; set; }
        public double Latitude { get; set; }
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
