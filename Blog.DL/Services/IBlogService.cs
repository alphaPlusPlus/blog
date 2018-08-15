using Blog.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DL.Services
{
    public interface IBlogService
    {

        void AddOrUpdate(BlogResponseModel entry);
        Task<IEnumerable<BlogResponseModel>> GetAsync();
        Task<BlogResponseModel> GetByIdAsync(int id);
        void Remove(int id);
        IEnumerable<BlogResponseModel> Where(Expression<Func<DAL.Models.Blog, bool>> exp);
    }
}
