using AutoMapper;
using Blog.API;
using Blog.DAL.Repositories;
using Blog.DL.Models;
using Blog.DL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Blog.DL.Tests
{
    public class BlogService_Unit_Test
    {
        private Mock<IBaseRepository<DAL.Models.Blog>> Repository { get; }

        private IBlogService Service { get; }

        private Mock<IMapper> mapper { get; }

        private List<DAL.Models.Blog> blogs;
        private List<BlogResponseModel> blogModels;

        /// <summary>
        /// test setup and creating mock object
        /// </summary>
        /// <param name="fixture"></param>
        public BlogService_Unit_Test()
        {
            Repository = new Mock<IBaseRepository<DAL.Models.Blog>>();

            blogs = new List<DAL.Models.Blog>
            {
                new DAL.Models.Blog
                {
                    Id = 1,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    Rating = 5,
                    Url = "https://samueleresca.net/"
                    
                },
                new DAL.Models.Blog
                {
                    Id = 2,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    Rating = 1,
                    Url = "https://yahoo.net/"

                }
            };

            blogModels = new List<BlogResponseModel>()
            {
                new BlogResponseModel
                {
                    Id = 1,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    Rating = 5,
                    Url = "https://samueleresca.net/"

                },
                new BlogResponseModel
                {
                    Id = 2,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now,
                    Rating = 1,
                    Url = "https://yahoo.net/"

                }
            };

            //GetAll
            Repository.Setup(x => x.GetAll())
               .ReturnsAsync(blogs);

            //GetById
            Repository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => blogs.Find(s => s.Id == id)));

            //Where
            Repository.Setup(x => x.Where(It.IsAny<Expression<Func<DAL.Models.Blog, bool>>>()))
                .Returns((Expression<Func<DAL.Models.Blog, bool>> exp) => blogs.AsQueryable().Where(exp));

            //Insert
            Repository.Setup(x => x.Insert(It.IsAny<DAL.Models.Blog>()))
                .Callback((DAL.Models.Blog label) => blogs.Add(label));

            //Update
            Repository.Setup(x => x.Update(It.IsAny<DAL.Models.Blog>()))
                .Callback((DAL.Models.Blog label) => blogs[blogs.FindIndex(x => x.Id == label.Id)] = label);

            //Delete
            Repository.Setup(x => x.Delete(It.IsAny<DAL.Models.Blog>()))
            .Callback((DAL.Models.Blog label) => blogs.RemoveAt(blogs.FindIndex(x => x.Id == label.Id)));

            //mock mapper
            mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<DAL.Models.Blog, BlogResponseModel>(It.IsAny<DAL.Models.Blog>())).Returns(blogModels[0]);
            mapper.Setup(x => x.Map<BlogResponseModel, DAL.Models.Blog>(It.IsAny<BlogResponseModel>())).Returns(blogs[0]);

            var baseService = new BaseService<DAL.Models.Blog>(Repository.Object);

            Service = new BlogService(baseService, mapper.Object);
        }

        [Fact]
        public void Can_Get_Single()
        {
            // Arrange
            var testId = 1;

            // Act
            var result = Service.GetByIdAsync(testId).Result;

            // Assert
            Repository.Verify(x => x.GetById(testId), Times.Once);
            Assert.Equal("https://samueleresca.net/", result.Url);
        }

        [Fact]
        public void Can_Get_All()
        {
            //arrange
            

            //act
            var entities = Service.GetAsync().Result;

            //assert
            Repository.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(2 , entities.Count());
        }

        [Fact]
        public void Can_Filter_Entities()
        {
            // Arrange
            var testid = 1;

            // Act
            var filteredEntities = Service.Where(s => s.Id == testid).First();

            // Assert
            Repository.Verify(x => x.Where(s => s.Id == testid), Times.Once);
            Assert.Equal(testid, filteredEntities.Id);
        }
        
        [Fact]
        public void Can_Insert_Entity()
        {
            // Arrange
            var entity = new DAL.Models.Blog
            {
                Id = 3,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now,
                Rating = 2,
                Url = "https://google.net/"
            };

            var model = new BlogResponseModel
            {
                Id = 3,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now,
                Rating = 2,
                Url = "https://google.net/"
            };

            mapper.Setup(x => x.Map<DAL.Models.Blog, BlogResponseModel>(entity)).Returns(model);
            mapper.Setup(x => x.Map<BlogResponseModel, DAL.Models.Blog>(model)).Returns(entity);

            // Act
            Service.AddOrUpdate(model);


            // Assert
            Repository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Repository.Verify(x => x.Insert(It.IsAny<DAL.Models.Blog>()), Times.Once);
            var entities = Service.GetAsync().Result;
            Assert.Equal(3, entities.Count());
        }


        [Fact]
        public void Can_Update_Entity()
        {
            // Arrange
            var entity = new DAL.Models.Blog
            {
                Id = 1,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now,
                Rating = 2,
                Url = "https://google.net/"
            };

            var model = new BlogResponseModel
            {
                Id = 1,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now,
                Rating = 2,
                Url = "https://google.net/"
            };

            mapper.Setup(x => x.Map<DAL.Models.Blog, BlogResponseModel>(entity)).Returns(model);
            mapper.Setup(x => x.Map<BlogResponseModel, DAL.Models.Blog>(model)).Returns(entity);

            // Act
            Service.AddOrUpdate(model);

            // Assert
            Repository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Repository.Verify(x => x.Update(It.IsAny<DAL.Models.Blog>()), Times.Once);
            var entityResult = Service.GetByIdAsync(1).Result;
            Assert.Equal(2, entityResult.Rating);
            Assert.Equal(DateTime.UtcNow.Date, entityResult.DateModified.Date);
        }
    }
}
