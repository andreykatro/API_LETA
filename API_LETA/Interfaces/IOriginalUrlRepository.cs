﻿using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
#warning Видалити зайве interface IOriginalUrlRepository

    interface IOriginalUrlRepository
    {
        //IQueryable<OriginalUrl> GetAll();
        OriginalUrl GetById(int id);
        OriginalUrl GetByName(string name);
        bool Delete(int id);
        bool Update(OriginalUrl originalUrl);
        bool Insert(string originalUrl);
    }
}
