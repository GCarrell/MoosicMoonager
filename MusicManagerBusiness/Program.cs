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

        public Tab Tab
        {
            get { return _tab; }
            set { _tab = value; }
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string AddUser(string userName, string password)
        {
            using (var db = new MusicManagerContext())
            {
                var findUser =
                    db.Users.Where(c => c.UserName == userName);
                if (findUser.Count() > 0)
                {
                    return "This username is already in use";
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
                    return "User Successfully Created";
                }
                
            }
        }

        public void AddTab(string tabName, string bandName, string instrument, string tabUrl, User uploader)
        {
            using (var db = new MusicManagerContext())
            {
                var newTab = new Tab
                {
                    TabName = tabName,
                    BandName = bandName,
                    Instrument = instrument,
                    TabUrl = tabUrl,
                    TabCreator = uploader.UserId
                };
                db.SaveChanges();
            }
        }

        public List<Tab> RetrieveAllTabs()
        {
            using (var db = new MusicManagerContext())
            {
                return db.Tabs.ToList();
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
