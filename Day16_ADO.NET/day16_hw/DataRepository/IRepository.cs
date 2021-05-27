using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
    public interface IRepository<T> where T:class
    {
        int Insert(T obj);
        int Delete(int id);
        int Update(T obj);
        T GetById(int Id);
        IEnumerable<T> GetAll();
    }
}
