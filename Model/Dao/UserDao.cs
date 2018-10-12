using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class UserDao
    {
        private OnlineShopDbContext db = null;

        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        //PAGE LISTING
        public IEnumerable<User> GetAllUsers(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        //GET USER BY ID
        public User GetUserById(int id)
        {
            return db.Users.Find(id);
        }

        // GET USER BY Username
        public User GetUserByUsername(string userName)
        {
            return db.Users.SingleOrDefault(x => x.Username == userName);
        }

        //UPDATE USER DETAILS
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Password = entity.Password;
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Write Log here
                return false;
            }
        }

        //  REMOVE USER ACCOUNT
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //CHECK ACCOUNT VALIDATE
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }

        //CHANGE USER STATUS
        public bool ChangeStatus(long id)
        {
            var userFind = db.Users.Find(id);
            userFind.Status = !userFind.Status;
            db.SaveChanges();
            return userFind.Status;
        }
    }
}