using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IPtor.DAL.Repositories
{
    public class CarRepository : EntityRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        { //остановился на добавлении людей, перемотать вверх в чате, там подсказки
        }

        public async Task<IEnumerable<Car>> GetCarsByServiceAsync(ServiceType service)
        {
            return await _context.Cars
                .Where(c => c.Service == service)  // Фильтр по службе
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsWithDetailsAsync()
        {
            return await _context.Cars
                .Include(c => c.Batteries)  // Подгружаем батареи
                .Include(c => c.Tires)      // Подгружаем шины
                .AsNoTracking()             // Только чтение, не отслеживаем изменения
                .ToListAsync();
        }

        public async Task<Car> GetCarWithFullInfoAsync(int id)
        {
            return await _context.Cars
                .Include(c => c.Batteries)
                .Include(c => c.Tires)
                .Include(c => c.Defects)
                .Include(c => c.DefectsTO)
                .AsNoTracking()   // что бы не городить город копий, просто уберем сохранения без явной команды
                .FirstOrDefaultAsync(c => c.CarId == id);
        }
    }
}
