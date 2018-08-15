using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.DAL.Models;
using Blog.DL.Models;

namespace Blog.DL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBaseService<DAL.Models.Blog> _Service;
        private readonly IMapper _Mapper;

        public BlogService(IBaseService<DAL.Models.Blog> service, IMapper mapper)
        {
            _Service = service;
            _Mapper = mapper;
        }

        public async Task<IEnumerable<BlogResponseModel>> GetAsync()
        {
            var results = await _Service.GetAsync();
            return results.Select(b => _Mapper.Map<DAL.Models.Blog, BlogResponseModel>(b));
        }

        public async Task<BlogResponseModel> GetByIdAsync(int id)
        {
            return _Mapper.Map<DAL.Models.Blog, BlogResponseModel>(await _Service.GetByIdAsync(id));
        }

        public void AddOrUpdate(BlogResponseModel entry)
        {
            _Service.AddOrUpdate(_Mapper.Map<BlogResponseModel, DAL.Models.Blog>(entry));
        }

        public IEnumerable<BlogResponseModel> Where(Expression<Func<DAL.Models.Blog, bool>> exp)
        {
            return _Service.Where(exp).Select(b => _Mapper.Map<DAL.Models.Blog, BlogResponseModel>(b));
        }

        public void Remove(int id)
        {
            _Service.Remove(id);
        }
    }
}
