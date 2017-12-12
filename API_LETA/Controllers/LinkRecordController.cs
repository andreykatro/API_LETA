using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API_LETA.Repositories;
using API_LETA.Interfaces;
using API_LETA.DAL;

namespace API_LETA.Models
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LinkRecordController : Controller
    {
        private readonly ILinkRecordRepository linkRecordRepository;
        private readonly ITagsLinkRecordRepository tagsLinkRecordRepository;
        private readonly ITagRepository tagRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IOriginalUrlRepository originalUrlRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly ITypeRepository typeRepository;

        public LinkRecordController(LinkRecordContext context)
        {
            linkRecordRepository = new LinkRecordRepository(context);
            tagsLinkRecordRepository = new TagsLinkRecordRepository(context);
            tagRepository = new TagRepository(context);
            categoryRepository = new CategoryRepository(context);
            originalUrlRepository = new OriginalUrlRepository(context);
            languageRepository = new LanguageRepository(context);
            typeRepository = new TypeRepository(context);

        }

        // GET: api/controller/getnotes?url="user_value"
        [HttpGet("{url}")]
        [Route("GetNotes")]
        public IQueryable<dynamic> GetNotes(string url)
        {
            var list = linkRecordRepository
                .GetAll()
                .Where(w => w.Url == url)
                .Select(s => new {
                Id = s.Id,
                CreateTime = s.CreateTime,
                Url = s.Url,
                OriginalUrl = s.OriginalUrl.OriginalUrlValue,
                Title = s.Title,
                Note = s.Note,
                Language = s.Language.LanguageName,
                Category = s.Category.CategoryName,
                Tags = s.TagsLinkRecords.Select(c => c.Tag.TagName),
                Type = s.Type.TypeName
            });

            return list;
        }

        // GET: api/values
        [HttpGet]
        public IQueryable<dynamic> Get()
        {
            var list = linkRecordRepository.GetAll()
                .Select(c => new
                {
                    Id = c.Id,
                    CreateTime = c.CreateTime,
                    Url = c.Url,
                    OriginalUrl = c.OriginalUrl.OriginalUrlValue,
                    Title = c.Title,
                    Note = c.Note,
                    Language = c.Language.LanguageName,
                    Category = c.Category.CategoryName,
                    Tags = c.TagsLinkRecords.Select(s => s.Tag.TagName),
                    Type = c.Type.TypeName
                });
            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            var list = linkRecordRepository
                .GetAll()
                .Where(w => w.Id == id)
                .Select(c => new
                {
                    Id = c.Id,
                    CreateTime = c.CreateTime,
                    Url = c.Url,
                    OriginalUrl = c.OriginalUrl.OriginalUrlValue,
                    Title = c.Title,
                    Note = c.Note,
                    Language = c.Language.LanguageName,
                    Category = c.Category.CategoryName,
                    Tags = c.TagsLinkRecords.Select(s => s.Tag.TagName),
                    Type = c.Type.TypeName
                });
            return list;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]JsonLinkRecord jsonLinkRecord)
        {
            LinkRecord linkRecord = new LinkRecord
            {
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"),
                Url = jsonLinkRecord.Url,
                Title = jsonLinkRecord.Title,
                Note = jsonLinkRecord.Note,
                CategoryId = GetIdCategory(jsonLinkRecord.Category),
                OriginalUrlId = GetIdOriginalUrl(jsonLinkRecord.OriginalUrl),
                LanguageId = GetIdLanguage(jsonLinkRecord.Language),
                TypeId = GetIdType(jsonLinkRecord.Type),
            };

            if (linkRecordRepository.Insert(linkRecord))
            {
                var idNewLinkRecord = linkRecordRepository.GetAll().Single(c => c.Url == jsonLinkRecord.Url).Id;

                AddCollectionTags(jsonLinkRecord.Tags, idNewLinkRecord);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]JsonLinkRecord jsonLinkRecord)
        {

            var linkRecord = linkRecordRepository.GetById(id);
            linkRecord.Url = jsonLinkRecord.Url;
            linkRecord.Title = jsonLinkRecord.Title;
            linkRecord.Note = jsonLinkRecord.Note;
            linkRecord.CategoryId = GetIdCategory(jsonLinkRecord.Category);
            linkRecord.OriginalUrlId = GetIdOriginalUrl(jsonLinkRecord.OriginalUrl);
            linkRecord.LanguageId = GetIdLanguage(jsonLinkRecord.Language);
            linkRecord.TypeId = GetIdType(jsonLinkRecord.Type);

            if (linkRecordRepository.Update(linkRecord))
            {
                foreach (var item in jsonLinkRecord.Tags)
                {
                    if (tagRepository.GetByName(item) == null)
                    {
                        tagRepository.Insert(item);
                    }
                }

                var newTagsForLink = jsonLinkRecord.Tags.Select(s => new TagsLinkRecord { TagId = tagRepository.GetByName(s).Id, LinkRecordId = id }).ToList();
                tagsLinkRecordRepository.Update(newTagsForLink);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            linkRecordRepository.Delete(id);
        }

        private int GetIdType(string name)
        {
            if (name == null && name.Trim().Length < 0) { return -1; }// програма не злетить але треба подумати щоб це значеннє не записалось в базу

            var type = typeRepository.GetByName(name);

            if (type == null)
            {
                typeRepository.Insert(name);
                type = typeRepository.GetByName(name);
            }
            return type.Id;
        }
        private int? GetIdLanguage(string name)
        {
            if (name == null && name.Trim().Length < 0) { return null; }
            var language = languageRepository.GetByName(name);
            if (language == null)
            {
                languageRepository.Insert(name);
            }
            return language?.Id;
        }
        private int GetIdOriginalUrl(string name)
        {
            if (name == null && name.Trim().Length < 0) { return -1; }// програма не злетить але треба подумати щоб це значеннє не записалось в базу

            var originalUrl = originalUrlRepository.GetByName(name);

            if (originalUrl == null)
            {
                originalUrlRepository.Insert(name);
                originalUrl = originalUrlRepository.GetByName(name);
            }
            return originalUrl.Id;
        }
        private int? GetIdCategory(string name)
        {
            if (name == null && name.Trim().Length < 0) { return null; }

            var category = categoryRepository.GetByName(name);

            if (category == null)
            {
                categoryRepository.Insert(name);
                category = categoryRepository.GetByName(name);
            }
            return category?.Id;
        }
        private void AddCollectionTags(List<string> listTags, int idLinkRecord)
        {
            if (listTags != null)
            {
                foreach (var item in listTags)
                {
                    tagRepository.Insert(item);
                }

                foreach (var item in listTags)
                {
                    tagsLinkRecordRepository.Insert(new TagsLinkRecord { TagId = tagRepository.GetByName(item).Id, LinkRecordId = idLinkRecord });
                }
            }
        }
    }
}
