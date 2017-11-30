using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EntityFramework.BLL.Test
{
    public class TestRepository : IDisposable
    {
        #region System
        public TestdbEntities db;
        private bool _disposed = false;
        public TestRepository(TestdbEntities context = null)
        {
            if (context == null) this.db = new TestdbEntities();
            else this.db = context;
        }

        public void Save()
        {
            db.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (db != null)
                        db.Dispose();
                }
                db = null;
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region User
        public List<User> GetUsers()
        {
            var res = new List<User>();
            res = db.Users.ToList();
            return res;
        }

        public User GetUser(int ID)
        {
            var res = new User();
            res = db.Users.SingleOrDefault(x => x.User_id == ID);
            return res;
        }
        public int SaveUser(User item)
        {
            if (item.User_id == 0)
            {
                db.Users.Add(item);
                db.SaveChanges();
            }
            else
            {
                db.Users.Attach(db.Users.Single(x => x.User_id == item.User_id));

                //((IObjectContextAdapter)db.Users).ObjectContext.ApplyCurrentValues(item);
                db.SaveChanges();
            }
            return item.User_id;
        }
        public bool DeleteUser(int id)
        {
            bool res = false;
            var item = db.Users.FirstOrDefault(x => x.User_id == id);
            if (item != null)
            {
                db.Users.Remove(item);
                db.SaveChanges();
                res = true;
            }
            return res;
        }
    }
}
