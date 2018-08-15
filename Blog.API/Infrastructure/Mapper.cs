using AutoMapper;
using Blog.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<DAL.Models.Blog, BlogResponseModel>();
            CreateMap<BlogResponseModel, DAL.Models.Blog>();
        }
    }
}
