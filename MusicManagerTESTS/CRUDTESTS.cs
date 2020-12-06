using NUnit.Framework;
using System.Linq;
using MusicManager;
using MusicManagerBusiness;

namespace MusicManagerTESTS
{
    public class CRUDTests
    {
        CRUDManager _crudManager = new CRUDManager();
        [SetUp]
        public void Setup()
        {
            using (var db = new MusicManagerContext())
            {
                var selectedUsers =
                    from c in db.Users
                    where c.UserName == "Padge"
                    select c;
                var selectedTabs =
                    from t in db.Tabs
                    where t.TabName == "Through The Looking Glass"
                    select t;
                db.Tabs.RemoveRange(selectedTabs);
                db.Users.RemoveRange(selectedUsers);
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new MusicManagerContext())
            {
                var selectedUsers =
                    from c in db.Users
                    where c.UserName == "Padge"
                    select c;
                var selectedTabs =
                    from t in db.Tabs
                    where t.TabName == "Through The Looking Glass"
                    select t;
                db.Users.RemoveRange(selectedUsers);
                db.Tabs.RemoveRange(selectedTabs);
                db.SaveChanges();
            }
        }


        [Test]
        public void WhenANewUserIsAdded_NumberOfUsersIncreasedBy1()
        {
            using (var db = new MusicManagerContext())
            {
                int initialusercount = db.Users.Count();
                var returnMessage = _crudManager.AddUser("Padge", "Padge123");
                Assert.AreEqual(db.Users.Count(), initialusercount + 1);
                Assert.AreEqual(returnMessage.returnMessage, "User Successfully Created.");
                Assert.AreEqual(returnMessage.passOrFail, "pass");
            }
        }

        //[Test]
        //public void CannotAddAUserWhenUsernameAlreadyInUse()
        //{
        //    using (var db = new MusicManagerContext())
        //    {
               
        //        int initialusercount = db.Users.Count();
        //        var newUser = new User
        //        {
        //            UserName = "Padge",
        //            Password = "Padge"
        //        };
        //        db.Users.Add(newUser);

        //        //var returnMessage = _crudManager.AddUser("Padge", "Padge123");
        //        Assert.AreEqual(initialusercount, db.Users.Count());
                
        //        //Assert.AreEqual(returnMessage.returnMessage, "This username is already in use.");
        //        //Assert.AreEqual(returnMessage.passOrFail, "fail");
        //    }
        //}


        [Test]
        public void NewTabAddedToUser()
        {
            using (var db = new MusicManagerContext())
            {
                var newUser = new User
                {
                    UserName = "Padge",
                    Password = "Padge123"
                };
                db.Users.Add(newUser);
                _crudManager.AddTab("Through The Looking Glass", "Dream Theater", "Guitar", "www.sfgsgs.agsg", newUser);
                var selectedUsers =
                    db.Tabs.Where(c => c.TabCreator == newUser.UserId);
                foreach (var item in selectedUsers)
                {
                    Assert.AreEqual("Through The Looking Glass", item.TabName);
                    Assert.AreEqual("Dream Theater", item.BandName);
                    Assert.AreEqual("Guitar", item.Instrument);
                    Assert.AreEqual("www.sfgsgs.agsg", item.TabUrl);
                }
            }
        }
    }
}