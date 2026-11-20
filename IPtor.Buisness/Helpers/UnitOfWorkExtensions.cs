using IPtor.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Helpers
{
    public static class UnitOfWorkExtensions
    {
        // Обертка для методов, возвращающих результат
        public static async Task<T> ExecuteWithLockAsync<T>(this IUnitOfWork uow, Func<Task<T>> action)
        {
            await uow.WaitAsync();
            try
            {
                return await action();
            }
            finally
            {
                uow.Release();
            }
        }

        // Обертка для методов, которые ничего не возвращают (void / Task)
        public static async Task ExecuteWithLockAsync(this IUnitOfWork uow, Func<Task> action)
        {
            await uow.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                uow.Release();
            }
        }
    }
}
