using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL
{
    interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAllData();
        T GetDataById(int id);
        void InsertData(T model);
        void DeleteData(int id);
        void UpdateData(T model);
        void SaveData();

    }
}
