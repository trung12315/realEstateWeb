using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        bdsWebContext db = null;
        public UserDao()
        {
            db = new bdsWebContext();
        }
        //public List<UserDao> ListAll()
        //{
        //    return db.Users.Where(x => x.Status == true).ToList();
        //}
        public long Insert(Custommer entity)
        {
            db.Custommers.Add(entity);
            db.SaveChanges();
            return entity.UserID;
        }
        public string TongUser3()
        {
            DateTime dt = DateTime.Now;
            string TongBaiDang;
            var nextDay = dt.AddDays(-2);
            var date = nextDay.AddDays(1);

            TongBaiDang = db.Custommers.Where(x => x.CreateDate >= nextDay && x.CreateDate < date).Count().ToString();
            return TongBaiDang;
        }
        public string TongUser2()
        {
            DateTime dt = DateTime.Now;
            var date = Convert.ToDateTime(dt).Date;
            string TongBaiDang;
            var nextDay = date.AddDays(-1);
            
            TongBaiDang = db.Custommers.Where(x => x.CreateDate >= nextDay && x.CreateDate < date).Count().ToString();
            return TongBaiDang;
        }
        public string TongUser1()
        {
            DateTime dt = DateTime.Now;
            string TongBaiDang;

            var date = Convert.ToDateTime(dt).Date;
            var nextDay = date.AddDays(1);
            TongBaiDang = db.Custommers.Where(x => x.CreateDate >= date && x.CreateDate < nextDay).Count().ToString();                     
            return TongBaiDang;
        }
        public string TongUser(string txtNgayThangNamSinh)
        {
            string TongBaiDang;
            if (!string.IsNullOrEmpty(txtNgayThangNamSinh))
            {
                var date = Convert.ToDateTime(txtNgayThangNamSinh).Date;
                var nextDay = date.AddDays(1);
                TongBaiDang = db.Custommers.Where(x => x.CreateDate >= date && x.CreateDate < nextDay).Count().ToString();
            }
            else
            {
                TongBaiDang = db.Custommers.Count().ToString();
            }
               
            return TongBaiDang;
            
           
        }
        public bool Update(Custommer entity, int userid)
        {
            try
            {
                var user = db.Custommers.Find(userid);
                user.Name = entity.Name;
                //if (!string.IsNullOrEmpty(entity.Password))
                //{
                //    user.Password = entity.Password;
                //}
                user.Phone = entity.Phone;
                user.Email = entity.Email;         

                user.ModifiedDate = DateTime.Now;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //Logging

                return false;
            }

        }
        public IEnumerable<Custommer> ListAllPaging(string searchString, int page, int pageSize=6)
        {
            IQueryable<Custommer> model = db.Custommers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public Custommer GetById(string userName)
        {
            return db.Custommers.SingleOrDefault(x => x.Username == userName);
        }

        public Custommer ViewDetail1(int id)
        {
            var result = db.Custommers.SingleOrDefault(x => x.UserID == id);
            return result;
        }

        public Custommer ViewDetail(int id)
        {
            return db.Custommers.Find(id);
        }
        public Custommer profileUser(string userName)
        {
            var result = db.Custommers.SingleOrDefault(x => x.Username == userName);
            return result;
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Custommers.SingleOrDefault(x => x.Username == userName);
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
                    if (result.Password == passWord && result.UserTyPeID == 1)
                        return 1;
                    else
                        return -2;
                }
            }
        }
        public int LoginUser(string userName, string passWord)
        {
            var result = db.Custommers.SingleOrDefault(x => x.Username == userName);
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
                    if (result.Password == passWord && result.UserTyPeID == 2||result.UserTyPeID==1)
                        return 1;
                    else
                        return -2;
                }
            }
        }

        public bool changeStatus(long id)
        {
            var user = db.Custommers.Find(id);

            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Custommers.Find(id);
                db.Custommers.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckUserName(string userName)
        {
            return db.Custommers.Count(x => x.Username == userName) > 0;
        }
        public bool CheckPhone(string phone)
        {
            return db.Custommers.Count(x => x.Phone == phone) > 0;
        }
            public bool CheckEmail(string email)
        {
            return db.Custommers.Count(x => x.Email == email) > 0;
        }
        public bool isEmail(string email)
        {
            email = email ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(email))
                return true;
            else
                return false;
        }
    }
}
