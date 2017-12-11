using API_LETA.DAL;
using API_LETA.Interfaces;
using API_LETA.Models;
using System.Collections.Generic;
using System.Linq;

namespace API_LETA.Repositories
{
    public class TagsLinkRecordRepository : ITagsLinkRecordRepository
    {
        private readonly LinkRecordContext context;

        public TagsLinkRecordRepository(LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var tagsLinkRecord = GetById(id);

            if (tagsLinkRecord != null)
            {
                context.TagsLinkRecords.Remove(tagsLinkRecord);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public IQueryable<TagsLinkRecord> GetByIdLink(int linkRecordId)
        {
            if (linkRecordId <= 0) { return null; }

            IQueryable<TagsLinkRecord> list = context
                .TagsLinkRecords
                //.Include(c => c.LinkRecord)
                //.Include(c => c.Tag)
                .Where(c => c.LinkRecordId == linkRecordId);

            return list;
        }

        public TagsLinkRecord GetById(int id)
        {
            return context
                .TagsLinkRecords
                //.Include(c => c.LinkRecord)
                //.Include(c => c.Tag)
                .SingleOrDefault(c => c.Id == id);
        }
        public bool Update(List<TagsLinkRecord> tagsLinkRecord)
        {
            if (tagsLinkRecord.Count > 0)
            {
                var listTagsForRemove = GetByIdLink(tagsLinkRecord[0].LinkRecordId);

                if (listTagsForRemove.Count() > 0)
                {
                    context.TagsLinkRecords.RemoveRange(listTagsForRemove);
                    context.SaveChanges();
                }
                context.TagsLinkRecords.AddRange(tagsLinkRecord);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public bool Insert(TagsLinkRecord tagsLinkRecord)
        {
            //  var ifExist = GetByIdLink(tagsLinkRecord.LinkRecordId).SingleOrDefault(c => c.TagId == tagsLinkRecord.TagId);

            if (tagsLinkRecord != null
                && (tagsLinkRecord.LinkRecordId > 0)
                && (tagsLinkRecord.TagId > 0)
                && GetByIdLink(tagsLinkRecord.LinkRecordId).SingleOrDefault(c => c.TagId == tagsLinkRecord.TagId) == null)
            {
                context.TagsLinkRecords.Add(tagsLinkRecord);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

    }
}
