using API_LETA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_LETA.Models;
using API_LETA.DAL;

namespace API_LETA.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly LinkRecordContext context;

        public TagRepository (LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var tag = GetById(id);

            if (tag != null)
            {
                context.Tags.Remove(tag);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        //public IQueryable<Tag> GetAll()
        //{
        //    var list = context.Tags;
        //    return list;
        //}

        public Tag GetById(int id)
        {
            return (id > 0) ? context.Tags.Find(id) : null;
        }

        public Tag GetByName(string name)
        {
            if (name != null && name.Trim().Length > 0)
            {
                return context.Tags.FirstOrDefault(c => c.TagName == name);
            }
            return null;
        }

        public bool Insert(string tagName)
        {
            if (tagName != null 
                &&(tagName.Trim().Length > 0)
                && GetByName(tagName) == null)
            {
                context.Tags.Add(new Tag { TagName = tagName});
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public bool Update(Tag tag)
        {
            if (tag != null  
                && (tag.TagName != null)
                && (GetById(tag.Id) != null)
                && (GetByName(tag.TagName) == null))
            {
                context.Tags.Update(tag);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
    }
}
