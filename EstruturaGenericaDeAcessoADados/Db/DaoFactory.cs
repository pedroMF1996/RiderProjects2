using System;

namespace Db
{
    public static class DaoFactory
    {
        public static T CreateDao<T>() where T:DAO, new()
        {
            return new T();
        }
    }
}