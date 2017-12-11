using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
#warning Видалити зайве interface ITypeRepository

    interface ITypeRepository
    {
        //IQueryable<Models.Type> GetAll();
        Models.Type GetById(int id);
        Models.Type GetByName(string name);
        bool Delete(int id);
        bool Update(Models.Type obj);
        bool Insert(string typeName);
    }
}
