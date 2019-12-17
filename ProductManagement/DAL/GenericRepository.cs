using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductManagement.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ProductEntities db;
        private IDbSet<T> dbEntity;

        public GenericRepository()
        {
            db = new ProductEntities();
            dbEntity = db.Set<T>();
        }

        public IEnumerable<T> GetAllData()
        {
           // System.Diagnostics.Debugger.NotifyOfCrossThreadDependency();
            return dbEntity.ToList();
        }

        public T GetDataById(int id)
        {
            return dbEntity.Find(id); 
        }

        public void InsertData(T model)
        {
            dbEntity.Add(model);
        }

        public void UpdateData(T model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public void DeleteData(int id)
        {
            T model = dbEntity.Find(id);
            dbEntity.Remove(model);
        }

        public void SaveData()
        {
            db.SaveChanges();
        }

       
    }
}