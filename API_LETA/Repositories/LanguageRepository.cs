using API_LETA.Interfaces;
using System.Linq;
using API_LETA.Models;
using API_LETA.DAL;

namespace API_LETA.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly LinkRecordContext context;
        public LanguageRepository(LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var language = GetById(id);

            if (language != null)
            {
                context.Languages.Remove(language);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public Language GetById(int id)
        {
            return (id > 0) ? context.Languages.Find(id) : null;
        }

        public Language GetByName(string name)
        {
            if (name != null && name.Trim().Length > 0)
            {
                return context.Languages.FirstOrDefault(c => c.LanguageName == name);
            }
            return null;
        }

        public bool Insert(string languageNmae)
        {
            int res = -1;

            if (languageNmae != null
                && (languageNmae.Trim().Length > 0)
                && GetByName(languageNmae) == null)
            {
                context.Languages.Add(new Language { LanguageName = languageNmae });
                res = context.SaveChanges();
            }
            return (res > 0) ? true : false;
        }

        public bool Update(Language language)
        {
            int res = -1;
            if (language != null
                && (language.LanguageName != null)
                && (GetById(language.Id) != null)
                && (GetByName(language.LanguageName) == null))
            {
                context.Languages.Update(language);
                res = context.SaveChanges();
            }

            return (res > 0) ? true : false;
        }
    }
}
