using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class RealEstateCategoryDao
    {
        bdsWebContext db = null;
        public RealEstateCategoryDao()
        {
            db = new bdsWebContext();
        }
        public List<RealEstateCategory> ListAll()
        {
            return db.RealEstateCategories.Where(x => x.Status == true).ToList();
        }
        public RealEstateCategory GetByID(long id)
        {
            return db.RealEstateCategories.Find(id);
        }
        public long Insert(RealEstateCategory entity)
        {
            db.RealEstateCategories.Add(entity);
            db.SaveChanges();
            return entity.CateID;
        }
        public bool Update(RealEstateCategory entity)
        {
            try
            {
                var category = db.RealEstateCategories.Find(entity.CateID);
                category.Name = entity.Name;
                category.MetaKeywords = entity.MetaKeywords;

                category.UpdateBy = entity.UpdateBy;
                category.UpdateDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
        public IEnumerable<RealEstateCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<RealEstateCategory> model = db.RealEstateCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public RealEstateCategory ViewDetail(int id)
        {
            return db.RealEstateCategories.Find(id);
        }


        public bool Delete(int id)
        {
            try
            {
                var category = db.RealEstateCategories.Find(id);
                db.RealEstateCategories.Remove(category);
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
