﻿using API_LETA.DAL;
using API_LETA.Interfaces;
using API_LETA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LinkRecordContext context;

        public CategoryRepository(LinkRecordContext context)
        {
            this.context = context;
        }

        public bool Delete(int id)
        {
            var category = GetById(id);

            if (category != null)
            {
                context.Categories.Remove(category);
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }

        public Category GetById(int id)
        {
            return (id > 0) ? context.Categories.Find(id) : null;
        }

        public Category GetByName(string name)
        {
            if (name != null && name.Trim().Length > 0)
            {
                return context.Categories.FirstOrDefault(c => c.CategoryName == name);
            }
            return null;
        }

        public bool Insert(string categoryName)
        {
            if (categoryName != null
                && (categoryName.Trim().Length > 0)
                && GetByName(categoryName) == null)
            {
                context.Categories.Add(new Category { CategoryName = categoryName });
                return (context.SaveChanges() > 0) ? true : false;
            }

            return false;
        }
#warning почисти зайве CategoryRepository 
        //public bool Insert(Category category)
        //{
        //    int res = -1;
        //    if (category != null
        //        && (category.CategoryName != null )
        //        && GetByName(category.CategoryName) == null)
        //    {
        //        category.Id = 0;
        //        context.Categories.Add(category);
        //        res = context.SaveChanges();
        //    }

        //    return (res > 0) ? true : false;
        //}

        public bool Update(Category category)
        {
            if (category != null  //у випадку коли category == null то все інше не буде перпвірятись
                && (category.CategoryName != null)
                && (GetById(category.Id) != null)
                && (GetByName(category.CategoryName) == null))
            {
                context.Categories.Update(category);
                return (context.SaveChanges() > 0) ? true : false;
            }
            return false;
        }
    }
}