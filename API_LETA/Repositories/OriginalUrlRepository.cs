using API_LETA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_LETA.Models;
using API_LETA.DAL;

namespace API_LETA.Repositories
{
    public class OriginalUrlRepository : IOriginalUrlRepository
    {
        private readonly LinkRecordContext context;
        public OriginalUrlRepository(LinkRecordContext context)
        {
            this.context = context;
        }
        public bool Delete(int id)
        {
            var originalUrl = GetById(id);

            if (originalUrl != null)
            {
                context.OriginalUrls.Remove(originalUrl);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
        
        public OriginalUrl GetById(int id)
        {
            return (id > 0) ? context.OriginalUrls.Find(id) : null;
        }

        public OriginalUrl GetByName(string name)
        {
            if (name != null && name.Trim().Length > 0)
            {
                return context.OriginalUrls.FirstOrDefault(c => c.OriginalUrlValue == name);
            }
            return null;
        }

        public bool Insert(string originalUrl)
        {
            if (originalUrl != null
                && (originalUrl.Trim().Length > 0)
                && GetByName(originalUrl) == null)
            {
                context.OriginalUrls.Add(new OriginalUrl { OriginalUrlValue = originalUrl });
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public bool Update(OriginalUrl originalUrl)
        {
            if (originalUrl != null
                &&(originalUrl.OriginalUrlValue != null)
                && (GetById(originalUrl.Id) != null)
                && (GetByName(originalUrl.OriginalUrlValue) == null))
            {
                context.OriginalUrls.Update(originalUrl);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
    }
}
