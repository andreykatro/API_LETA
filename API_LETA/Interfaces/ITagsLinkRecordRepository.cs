using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Interfaces
{
    interface ITagsLinkRecordRepository
    {
        IQueryable<TagsLinkRecord> GetByIdLink(int linkRecordId);
        TagsLinkRecord GetById(int id);
        bool Delete(int id);
        bool Update(List<TagsLinkRecord> tagsLinkRecord);
        bool Insert(TagsLinkRecord tagsLinkRecord);
    }
}
