using SametSenturkBlog.Data.Models.ORM.Context;
using SametSenturkBlog.Business.Services.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace SametSenturkBlog.Business.Services.Base
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : SametSenturkBlog.Data.Models.ORM.Entities.Base
    {
        protected SimgeContext db;
        protected DbSet<TEntity> dbcontext;
        public BaseService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SimgeContext>();
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-0RRQ1NO\SQLEXPRESS;Database=SametSenturkBlogDB;Integrated Security=SSPI;");
            db = new SimgeContext(optionsBuilder.Options);
            dbcontext = db.Set<TEntity>();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }


        public virtual List<TEntity> GetAll() => dbcontext.Where(x => x.isDeleted == false && x.isActive == true).ToList();

        public virtual List<TEntity> GetAllWithPassives() => dbcontext.Where(x => x.isDeleted == false).ToList();

        public virtual List<TEntity> GetAllWithQuery(Expression<Func<TEntity, bool>> lambda) => dbcontext.Where(x => x.isDeleted == false).Where(lambda).ToList();

        public virtual List<TEntity> GetListWithGroupBy(Expression<Func<TEntity, bool>> lambda) => dbcontext.GroupBy(lambda).Select(group => group.Last()).ToList();

        public virtual TEntity Insert(TEntity entity)
        {
            if (entity != null)
            {
                entity.AddDate = DateTime.Now;
                entity.isDeleted = false;
                entity.isActive = true;
                dbcontext.Add(entity);
                SaveChanges();
                return entity;
            }
            else
                return null;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity != null)
            {
                var _entity = dbcontext.Find(entity.ID);
                entity.AddDate = _entity.AddDate;
                entity.isDeleted = _entity.isDeleted;
                db.Entry(_entity).CurrentValues.SetValues(entity);
                SaveChanges();
                return entity;
            }
            else
                return null;
        }

        public virtual bool Delete(int? id)
        {
            if (id != null)
            {
                var entity = dbcontext.Find(id);
                entity.isDeleted = true;
                entity.DeletedDate = DateTime.Now;
                SaveChanges();
                return true;
            }
            else
                return false;
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda) => dbcontext.Where(x => !x.isDeleted).FirstOrDefault(lambda);

        public virtual TEntity GetbyIdWithQuery(int? id)
        {
            if (id != null)
            {
                var entity = dbcontext.Find(id);
                return entity;
            }
            else
                return null;
        }

        public virtual bool AnyWithQuery(Expression<Func<TEntity, bool>> lambda) => dbcontext.Where(x => !x.isDeleted).Any(lambda);

        public virtual int Count(Expression<Func<TEntity, bool>> lambda) => dbcontext.Where(x => x.isDeleted == false).Count(lambda);

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(db);
        }

    }
}
