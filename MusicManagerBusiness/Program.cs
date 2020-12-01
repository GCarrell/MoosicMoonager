using System;
using System.Linq;
using MusicManager;
using System.Collections.Generic;

namespace MusicManagerBusiness
{
    public class CRUDManager
    {
        public void AddUser(string userName, string password)
        {
            using (var db = new MusicManagerContext())
            {
                var newUser = new User
                {
                    UserName = userName,
                    Password = password,
                };
                db.Users.Add(newUser);
                db.SaveChanges();
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

                static void Main(string[] args)
        {
        }
    }
}
