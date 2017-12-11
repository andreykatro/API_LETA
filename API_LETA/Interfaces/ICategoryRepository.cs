using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
#warning Видалити зайве interface ICategoryRepository
    interface ICategoryRepository
    {


       // IQueryable<Category> GetPart(int number);
        Category GetById(int id);
        Category GetByName(string name);
        bool Delete(int id);
        bool Update(Category category);
        //bool Insert(Category category);
        bool Insert(string category);
    }
}
