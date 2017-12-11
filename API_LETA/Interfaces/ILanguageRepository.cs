using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
    interface ILanguageRepository
    {
        Language GetById(int id);
        Language GetByName(string name);
        bool Delete(int id);
        bool Update(Language language);
        //bool Insert(Language language);
        bool Insert(string language);
    }
}
