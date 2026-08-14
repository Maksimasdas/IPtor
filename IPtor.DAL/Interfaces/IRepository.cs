using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace IPtor.DAL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<T> GetByIdAsync(int id); // Чтение по ID
        Task<IEnumerable<T>> GetAllAsync(); // Чтение всех
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate); // Поиск по условию
        Task AddAsync(T entity); // Добавление 
        void Update(T entity); // Обновление
        void Delete(T entity); // Удаление
    }
}
