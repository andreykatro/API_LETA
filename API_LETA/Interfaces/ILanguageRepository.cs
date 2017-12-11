using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
#warning Видалити зайве interface ILanguageRepository

    interface ILanguageRepository
    {
        //IQueryable<Language> GetPart(int number);
        Language GetById(int id);
        Language GetByName(string name);
        bool Delete(int id);
        bool Update(Language language);
        //bool Insert(Language language);
        bool Insert(string language);
    }
}
