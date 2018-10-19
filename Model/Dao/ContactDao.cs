using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        OnlineShopDbContext db = null;
        public ContactDao()
        {
            db = new OnlineShopDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.SingleOrDefault(x => x.Status == true);
        }

        public long InsertFeedback(Feedback feedback)
        {
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            return feedback.ID;
        }
    }
}
