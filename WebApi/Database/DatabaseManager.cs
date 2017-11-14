using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp.Database
{
    public class DatabaseManager
    {
        public void SaveBarRating(BarData barToRate, int rating)
        {
            using (var db = new BarsDatabase())
            {              
                var userInDb = db.Users.FirstOrDefault(user => user.Username == Program.defaultUser.Username);
                if (userInDb == null)
                    userInDb = new User { Username = Program.defaultUser.Username };
                var barInDb = db.Bars.FirstOrDefault(bar => bar.BarId == barToRate.BarId);
                if (barInDb != null)
                {
                    var barRating = db.UserRatings.Find(barInDb.BarId, userInDb.Username);
                    if (barRating != null)
                    {
                        barRating.Rating = rating;
                    }
                    else
                    {
                        db.UserRatings.Add(new UsersRatingToBar(barInDb, userInDb, rating));
                    }
                }
                else
                {
                    db.UserRatings.Add(new UsersRatingToBar(barToRate, userInDb, rating));
                }
                db.SaveChanges();              
            }
        }

        public BarDataModel GetAllBarData(BarDataModel localBars)
        {
            using (var db = new BarsDatabase())
            {
                localBars.ForEach(bar => bar.Ratings = db.UserRatings.Where(x => x.BarId == bar.BarId).Select(x => x.Rating).ToList());
            }
            return localBars;
        }

        public List<int> GetBarRatings(BarData bar)
        {
            List<int> list;
            using (var db = new BarsDatabase())
            {
                list = db.UserRatings.Where(x => x.BarId == bar.BarId).Select(x => x.Rating).ToList();
            }
            return list;
        }
    }
}
