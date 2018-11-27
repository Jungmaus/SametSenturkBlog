using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SametSenturkBlog.Business.Services.Base.Abstract
{
    public interface IBaseService<T>
    {
        List<T> GetAll();
        List<T> GetAllWithPassives();
        List<T> GetAllWithQuery(Expression<Func<T, bool>> lambda);

        T Insert(T entity);
        T Update(T entity);
        bool Delete(int? id);

        T FirstOrDefault(Expression<Func<T, bool>> lambda);
        T GetbyIdWithQuery(int? id);

        bool AnyWithQuery(Expression<Func<T, bool>> lambda);

        int Count(Expression<Func<T, bool>> lambda);
    }
}
