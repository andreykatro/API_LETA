using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
#warning Видалити зайве interface ITagRepository

    interface ITagRepository
    {
       // IQueryable<Tag> GetAll();
        Tag GetById(int id);
        Tag GetByName(string name);

        bool Delete(int id);
        bool Update(Tag tag);
        bool Insert(string tag);
    }
}
