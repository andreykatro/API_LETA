using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
    interface ILinkRecordRepository
    {
        IQueryable<LinkRecord> GetAll();
        LinkRecord GetById(int id);
        bool Delete(int id);
        bool Update(LinkRecord linkRecord);
        bool Insert(LinkRecord linkRecord);
    }
}
