using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        bdsWebContext db = null;
        public CategoryDao()
        {
            db = new bdsWebContext();
        }
        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }
        public Category GetByID(int id)
        {
            return db.Categories.Find(id);
        }
        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.CatID;
        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.CatID);
                //category.Name = entity.Name;
                //category.MetaKeywords = entity.MetaKeywords;

                //category.UpdateBy = entity.UpdateBy;
                //category.UpdateDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
        //public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<CategoryDao> model = db.Categories;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
        //    }
        //    return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        //}

        //public Category ViewDetail(long id)
        //{
        //    return db.Categories.Find(id);
        //}


        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}

