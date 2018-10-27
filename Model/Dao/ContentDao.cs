using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }
        /// <summary>
        /// Get contents of admin side
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> GetAllContent(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get Contents of client side
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> GetAllContent(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Content> GetAllContentByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             ID = a.ID,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreateDate,
                             CreatedBy = a.CreateBy

                         }).AsEnumerable().Select(x => new Content()
                         {
                             ID = x.ID,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreateBy = x.CreatedBy,
                             CreateDate = x.CreatedDate
                         });
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public long Create(Content content)
        {
            // resolve alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            content.CreateDate = DateTime.Now;
            content.ViewCount = 0;
            db.Contents.Add(content);
            db.SaveChanges();

            // process tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tagName in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tagName);
                    var isExistedtag = this.CheckTag(tagId);
                    if (!isExistedtag)
                    {
                        this.InsertToTagTable(tagId, tagName);
                    }
                    // Insert new tag to ContentTag
                    this.InsertToContentTag(content.ID, tagId);
                }
            }

            return content.ID;
        }
        /// <summary>
        /// Edit-Update Content to SQL
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public long Edit(Content content)
        {
            var contentEdit = db.Contents.Find(content.ID);
            // resolve alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                contentEdit.MetaTitle = StringHelper.ToUnsignString(content.Name);
            }
            try
            {
                contentEdit.ModifiedDate = DateTime.Now;
                contentEdit.Name = content.Name;
                contentEdit.Description = content.Description;
                contentEdit.Image = content.Image;
                contentEdit.CategoryID = content.CategoryID;
                contentEdit.Details = content.Details;
                contentEdit.MetaKeywords = content.MetaKeywords;
                contentEdit.MetaDescription = content.MetaDescription;
                contentEdit.Status = content.Status;
                contentEdit.ModifiedBy = content.ModifiedBy;
                contentEdit.Language = content.Language;
                contentEdit.ViewCount = content.ViewCount;
                contentEdit.Language = content.Language;
                contentEdit.Tags = content.Tags;
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
            // process tag
            if (!string.IsNullOrEmpty(contentEdit.Tags))
            {
                this.RemoveAllContentTag(contentEdit.ID);
                string[] tags = contentEdit.Tags.Split(',');
                foreach (var tagName in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tagName);
                    var isExistedtag = this.CheckTag(tagId);
                    if (!isExistedtag)
                    {
                        this.InsertToTagTable(tagId, tagName);
                    }
                    // Insert new tag to ContentTag
                    this.InsertToContentTag(contentEdit.ID, tagId);
                }
            }

            return contentEdit.ID;
        }

        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }

        /// <summary>
        /// Insert new tag to Tag table in SQL server
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void InsertToTagTable(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        /// <summary>
        /// Insert new tag to content Tag
        /// </summary>
        /// <param name="contentID"></param>
        /// <param name="tagID"></param>
        public void InsertToContentTag(long contentID, string tagID)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentID;
            contentTag.TagID = tagID;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public List<Tag> GetListTagByContentId(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).ToList().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name,
                         });
            return model.ToList();
        }
    }
}
