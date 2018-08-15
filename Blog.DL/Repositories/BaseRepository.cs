using Blog.DAL.Infrastructure.ErrorHandler;
using Blog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public readonly DataContext _Context;

        private readonly DbSet<T> _Entities;

        private readonly IErrorHandler _ErrorHandler;

        public BaseRepository(DataContext context, IErrorHandler errorHandler)
        {
            _Context = context;
            _Entities = context.Set<T>();
            _ErrorHandler = errorHandler;
        }

        public async void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_ErrorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));

            _Entities.Remove(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _Entities.ToListAsync<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await _Entities.SingleOrDefaultAsync<T>(s => s.Id == id);
        }

        public async void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_ErrorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            await _Entities.AddAsync(entity);
            await _Context.SaveChangesAsync();
        }

        public async void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_ErrorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));

            var oldEntity = await _Context.FindAsync<T>(entity.Id);
            _Context.Entry(oldEntity).CurrentValues.SetValues(entity);
            await _Context.SaveChangesAsync();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _Entities.Where<T>(exp);
        }
    }
}
