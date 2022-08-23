using Entity.Base;
using System;

namespace Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T? GetById(int id);
        T? Find(Func<T, bool> predicate);
    }
}
