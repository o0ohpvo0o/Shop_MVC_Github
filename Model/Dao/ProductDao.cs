using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> GetNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> GetFeatureProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Where(x => x.TopHot != null && x.TopHot > DateTime.Now).Take(top).ToList();
        }

        public Product GetProductById(long id)
        {
            return db.Products.Find(id);
        }

        public List<Product> GetRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID.Value).ToList();
        }
    }
}
