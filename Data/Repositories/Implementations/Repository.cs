using Data.MoackData;
using Data.Repositories.Interfaces;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDataBase _context;
        private IList<T>? _dbSet;

        public Repository(IDataBase context)
        {
            _context = context;
            _dbSet = _context.SetContext<T>();
        }

        public T? GetById(int id)
        {
            return _dbSet?.FirstOrDefault(x => x.Id == id);
        }

        public T? Find(Func<T, bool> predicate)
        {
            return _dbSet?.FirstOrDefault(predicate);
        }
    }
}
