using System;
using System.Linq;
using MusicManager;
using System.Collections.Generic;

namespace MusicManagerBusiness
{
    public class CRUDManager
    {
        private User _user;
        private Tab _tab;

        public Tab CurrentTab
        {
            get { return _tab; }
            set { _tab = value; }
        }


        public void SetTab(object sender)
        {
            CurrentTab = (Tab)sender;
        }
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string DisplayTabCreator()
        {
            using (var db = new MusicManagerContext())
            {
                var tabCreator =
                (   
                    from u in db.Users
                    join t in db.Tabs on u.UserId equals t.TabCreator
                    where t.TabCreator == CurrentTab.TabCreator
                    select u.UserName
                ).FirstOrDefault();
                return tabCreator;
            }
        }
        public string LikeDislikeTab(int rating)
        {
            using (var db = new MusicManagerContext())
            {
                var findExistingLike = db.Ratings.Where(c => c.UserId == User.UserId && c.TabId == CurrentTab.TabId).FirstOrDefault();

                if (findExistingLike is null)
                {
                    var newRating = new Rating
                    {
                        TabId = CurrentTab.TabId,
                        UserId = User.UserId,
                        Rating1 = rating
                    };
                    db.Ratings.Add(newRating);
                    db.SaveChanges();
                    switch (rating)
                    {
                        case 0:
                            return "disliked";
                        case 100:
                            return "liked";
                    }
                }
                if (findExistingLike.Rating1 != rating)
                {
                    findExistingLike.Rating1 = rating;
                    db.SaveChanges();
                    switch (rating)
                    {
                        case 0:
                            return "disliked";
                        case 100:
                            return "liked";
                    }
                }             
                return "failed";
            }
        }


        public (string, string) AddTabToFavourites()
        {
            using (var db = new MusicManagerContext())
            {
                var findFavourite = db.Favourites.Where(c => c.UserId == User.UserId && c.TabId == CurrentTab.TabId);
                if (findFavourite.Count() < 1)
                {
                    var newFavourite = new Favourite
                    {
                        TabId = CurrentTab.TabId,
                        UserId = User.UserId
                    };
                    db.Favourites.Add(newFavourite);
                    db.SaveChanges();
                    return ("Tab added to favourites", "pass");
                }
                
                return ("You have already added this tab to your favourites.", "fail");
            }
        }

        public (string returnMessage, string passOrFail) AddUser(string userName, string password)
        {
            using (var db = new MusicManagerContext())
            {
                var validationResult =  inputValidation(userName, password);

                if (validationResult.passOrFail == "fail")
                {
                    return validationResult;
                }

                var findUser =
                    db.Users.Where(c => c.UserName == userName);

                if (findUser.Count() > 0)
                {
                    return ("This username is already in use.", "fail");
                }
                
                else
                {
                    var newUser = new User
                    {
                        UserName = userName,
                        Password = password,
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return ("User Successfully Created.", "pass");
                }
                
            }
        }
        public (string returnMessage, string passOrFail) Login(string userName, string password)
        {
            using (var db = new MusicManagerContext())
            {
                var validationResult = inputValidation(userName, password);
                if (validationResult.passOrFail == "fail")
                {
                    return validationResult;
                }
                var findUser = db.Users.Where(c => c.UserName == userName).FirstOrDefault();
                if (findUser is null)
                {
                    return ("Username not recognised.", "fail");
                }
                else if (findUser.UserName == userName && findUser.Password == password)
                {
                    User = findUser;
                    return ("Login successful.", "pass");
                }
                else
                {                   
                    return ("Username and Password were not recognised.", "fail");
                }

            }
        }

        public (string returnMessage, string passOrFail) inputValidation(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return ("Username field cannot be empty.", "fail");
            }
            if (userName.Length < 5)
            {
                return ("Username must be at least 5 characters long.", "fail");
            }
            if (password.Length < 6)
            {
                return ("Password must be at least 6 characters long.", "fail");
            }
            if (String.IsNullOrEmpty(password))
            {
                return ("Password field cannot be empty.", "fail");
            }
            else
            {
                return ("Pass", "pass");
            }
        }

        public (string returnMessage, string passOrFail) inputValidation(string tabName, string bandName, string instrument, string tabUrl, User uploader)
        {
            if (String.IsNullOrEmpty(tabName))
            {
                return ("Tab name field cannot be empty.", "fail");
            }
            if (String.IsNullOrEmpty(bandName))
            {
                return ("Artist field cannot be empty.", "fail");
            }
            if (String.IsNullOrEmpty(instrument))
            {
                return ("Instrument field cannot be empty.", "fail");
            }
            if (String.IsNullOrEmpty(tabUrl))
            {
                return ("Download link must be provided.", "fail");
            }
            if (uploader is null)
            {
                return ("How did you manage that?", "fail");
            }
            else
            {
                return ("Pass", "pass");
            }
        }



        public (string returnMessage, string passOrFail) AddTab(string tabName, string bandName, string instrument, string tabUrl, User uploader)
        {
            using (var db = new MusicManagerContext())
            {
                var validationResult = inputValidation(tabName, bandName, instrument, tabUrl, uploader);
                if (validationResult.passOrFail == "fail")
                {
                    return validationResult;
                }

                var newTab = new Tab
                {
                    TabName = tabName,
                    BandName = bandName,
                    Instrument = instrument,
                    TabUrl = tabUrl,
                    TabCreator = uploader.UserId
                };
                db.Tabs.Add(newTab);
                db.SaveChanges();
                return ("Tab uploaded Successfully", "Pass");
            }
        }

        public List<Tab> RetrieveAllTabs()
        {
            using (var db = new MusicManagerContext())
            {
                return db.Tabs.ToList();
            }
        }
        public List<Tab> RetrieveUserTabs()
        {
            using (var db = new MusicManagerContext())
            {
                return db.Tabs.Where(c => c.TabCreator == User.UserId).ToList();
            }
        }
        public List<Tab> RetrieveAllFavouriteTabs()
        {
            using (var db = new MusicManagerContext())
            {

                var favouriteList =
                (
                    from t in db.Tabs
                    join f in db.Favourites on t.TabId equals f.TabId
                    join u in db.Users on f.UserId equals u.UserId
                    where f.UserId == User.UserId
                    select t).ToList() /*as List<Tab>*/ ;
                
                return favouriteList;
            }
        }
        public string RetrieveTabRating()
        {
            using (var db = new MusicManagerContext())
            {
                var tabRating = db.Ratings.Where(c => c.TabId == CurrentTab.TabId);
                if (tabRating.Count() > 0)
                {
                    var averageRating = tabRating.Average(c => c.Rating1);
                    return $"{(Math.Round(averageRating, 0)).ToString()}%";
                }
                return "Not yet rated";
            }
        }
        public void DeleteFavouriteTab()
        {
            using (var db = new MusicManagerContext())
            {
                var faveForDeletion = db.Favourites.Where(c => c.TabId == CurrentTab.TabId && c.UserId == User.UserId);
                db.Favourites.RemoveRange(faveForDeletion);
                db.SaveChanges();
            }
        }

        public void DeleteTab()
        {
            using (var db = new MusicManagerContext())
            {
                var listOfAssosciatedLikes = db.Ratings.Where(c => c.TabId == CurrentTab.TabId);
                db.Ratings.RemoveRange(listOfAssosciatedLikes);
                var listOfAssosciatedFavourites = db.Favourites.Where(c => c.TabId == CurrentTab.TabId);
                db.Favourites.RemoveRange(listOfAssosciatedFavourites);
                var theTab = db.Tabs.Where(c => c.TabId == CurrentTab.TabId && c.TabCreator == User.UserId);
                db.Tabs.RemoveRange(theTab);
                db.SaveChanges();
            }
        }
        public List<Tab> RetrieveGuitarTabs()
        {
            using (var db = new MusicManagerContext())
            {
                return db.Tabs.Where(c => c.Instrument == "Guitar").ToList();
            }
        }
        public List<Tab> RetrieveBassTabs()
        {
            using (var db = new MusicManagerContext())
            {
                return db.Tabs.Where(c => c.Instrument == "Bass").ToList();
            }
        }
        public List<Tab> RetrieveDrumTabs()
        {
            using (var db = new MusicManagerContext())
            {
                return db.Tabs.Where(c => c.Instrument == "Drums").ToList();
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
