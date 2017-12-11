using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_LETA.Repositories;
using API_LETA.Models;
using API_LETA.Interfaces;
using API_LETA.DAL;
using Newtonsoft.Json;

#warning Видалити CategoryController якщо не треба буде

namespace API_LETA.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(LinkRecordContext context)
        {
            _categoryRepository = new CategoryRepository(context);
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return _categoryRepository.GetAll().Select(c=>c.CategoryName);
        //}

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            if (id <= 0) { return null; }

            return _categoryRepository.GetById(id)?.CategoryName;//return name or null
        }

        [HttpPost]
        public void Post([FromBody]string category)
        {
            if (category != null && category.Trim().Length > 0)
            {
                _categoryRepository.Insert(category);
                //_categoryRepository.Insert(new Category() { CategoryName = category });
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string category)
        {
            if (category != null && id > 0)
            {
                _categoryRepository.Update(new Category() { Id = id, CategoryName = category });
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id > 0)
            {
                _categoryRepository.Delete(id);
            }
        }
    }
}
