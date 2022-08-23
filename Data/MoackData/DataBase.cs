using Data.MoackData.Models;
using Entity.Authorization;
using Entity.Base;
using System.Collections.Generic;

namespace Data.MoackData
{
    public class DataBase : IDataBase
    {
        private readonly Dictionary<string, object> _context = new();

        public DataBase()
        {
            var users = new List<User>
            {
                new User { Id = 1, UserName = "user1", Password = "password1", Email = "user1@user.com", Role = CustomRoles.Admin },
                new User { Id = 2, UserName = "user2", Password = "password2", Email = "user2@user.com", Role = CustomRoles.User },
                new User { Id = 3, UserName = "user3", Password = "password3", Email = "user3@user.com", Role = CustomRoles.User },
                new User { Id = 4, UserName = "user4", Password = "password4", Email = "user4@user.com", Role = CustomRoles.User },
            };

            _context.Add(nameof(User), users);
        }

        public List<T>? SetContext<T>() where T : BaseEntity
        {
            if (_context.TryGetValue(typeof(T).Name, out object? value))
            {
                return value as List<T>;
            }

            return null;
        }
    }
}
