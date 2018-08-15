using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.DAL.Infrastructure.ErrorHandler;
using Blog.DL.Models;
using Blog.DL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _Service;
        private readonly IErrorHandler _ErrorHandler;

        public BlogController(IBlogService service, IErrorHandler errorHandler)
        {
            _Service = service;
            _ErrorHandler = errorHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<BlogResponseModel>> Get()
        {
            return await _Service.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<BlogResponseModel> GetById(int id)
        {
            return await _Service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<BlogResponseModel> Post(int id)
        {
            return await _Service.GetByIdAsync(id);
        }
    }
}