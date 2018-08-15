using Blog.DAL;
using Blog.DAL.Infrastructure.ErrorHandler;
using Blog.DAL.Repositories;
using Blog.DL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure
{
    public class Installer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddTransient<IBaseRepository<DAL.Models.Blog>, BaseRepository<DAL.Models.Blog>>();
            services.AddTransient<IBaseService<DAL.Models.Blog>, BaseService<DAL.Models.Blog>>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IErrorHandler, ErrorHandler>();
        }

    }
}
