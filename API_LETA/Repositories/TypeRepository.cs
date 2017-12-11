using API_LETA.DAL;
using API_LETA.Interfaces;
using API_LETA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly LinkRecordContext context;

        public TypeRepository(LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var type = GetById(id);

            if (type != null)
            {
                context.Types.Remove(type);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public Models.Type GetById(int id)
        {
            return (id > 0) ? context.Types.Find(id) : null;
        }

        public Models.Type GetByName(string name)
        {
            if (name != null && name.Trim().Length > 0)
            {
                return context.Types.FirstOrDefault(c => c.TypeName == name);
            }
            return null;
        }

        public bool Insert(string typeName)
        {
            if (typeName != null
                && (typeName.Trim().Length > 0)
                && GetByName(typeName) == null)
            {
                context.Types.Add(new Models.Type { TypeName = typeName});
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
        
        public bool Update(Models.Type type)
        {
            if (type != null  
                && (type.TypeName != null)
                && (GetById(type.Id) != null)
                && (GetByName(type.TypeName) == null))
            {
                context.Types.Update(type);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
    }
}
