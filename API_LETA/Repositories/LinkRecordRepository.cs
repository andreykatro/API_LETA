using API_LETA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_LETA.Models;
using API_LETA.DAL;
using Microsoft.EntityFrameworkCore;

namespace API_LETA.Repositories
{
    public class LinkRecordRepository : ILinkRecordRepository
    {
        private readonly LinkRecordContext context;
        public LinkRecordRepository(LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var linkRecord = GetById(id);

            if (linkRecord != null)
            {
                var tagsList = context.TagsLinkRecords.Where(c => c.LinkRecordId == id);

                context.TagsLinkRecords.RemoveRange(tagsList);
                if (context.SaveChanges() > 0)
                {
                    context.LinkRecords.Remove(linkRecord);
                    return (context.SaveChanges() > 0) ? true : false;
                }
            }

            return false;
        }

        public LinkRecord GetById(int id)
        {
            var item = context.LinkRecords
                //.Include(c => c.Category)
                //.Include(c => c.Language)
                //.Include(c => c.OriginalUrl)
                //.Include(c => c.Type)
                .SingleOrDefault(c => c.Id == id);

            return item;
        }

        public IQueryable<LinkRecord> GetAll()
        {
            var list = context.LinkRecords
                //.Include(c => c.Category)
                //.Include(c => c.Language)
                //.Include(c => c.OriginalUrl)
                //.Include(c => c.Type)
                ;

            return list;
        }

        public bool Insert(LinkRecord linkRecord)
        {
            if (linkRecord != null 
                && GetAll().SingleOrDefault(c => c.Url == linkRecord.Url) == null)
            {
                context.LinkRecords.Add(linkRecord);
                return (context.SaveChanges() > 0) ? true : false;
            }
            return false;
        }

        public bool Update(LinkRecord linkRecord)
        {
            if (linkRecord != null && GetById(linkRecord.Id) != null)
            {
                context.LinkRecords.Update(linkRecord);
                return (context.SaveChanges() > 0) ? true : false;
            }
            return false;
        }
    }
}
