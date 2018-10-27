using Model.EF;
using Model.ViewModel;
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

        public List<string> GetProductName(string name)
        {
            return db.Products.Where(x => x.Name.Contains(name)).Select(x => x.Name).ToList();
        }

        public List<Product> GetRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID.Value).ToList();
        }

        /// <summary>
        /// Get List Products By It's CategoryID
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<ProductViewModel> GetAllProductByCategoryId(long categoryId, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryId).Count();
            var model = from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == categoryId
                        select new ProductViewModel
                        {
                            Metatitle = a.MetaTitle,
                            CateMetatitle = b.MetaTitle,
                            CateName = b.Name,
                            CreatedDate = a.CreateDate,
                            ID = a.ID,
                            Images = a.Image,
                            Name = a.Name,
                            Price = a.Price
                        };
            model = model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             Metatitle = a.MetaTitle,
                             CateMetatitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             Price = a.Price
                         }).ToList().Select(x => new ProductViewModel()
                         {
                             Metatitle = x.Metatitle,
                             CateMetatitle = x.CateMetatitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             Price = x.Price
                         });
            model = model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}
