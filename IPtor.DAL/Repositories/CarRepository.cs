using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IPtor.DAL.Repositories
{
    public class CarRepository : EntityRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
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
                       .ThenInclude(d => d.SheffPassService)
                   .Include(c => c.Defects)
                       .ThenInclude(d => d.ResponcePerson)
                   .Include(c => c.Defects)
                       .ThenInclude(d => d.Driver)
                   .Include(c => c.Defects)
                       .ThenInclude(d => d.AcceptUtoPerson)
                   .Include(c => c.Defects)
                       .ThenInclude(d => d.AdditionalPerson)
                   .Include(c => c.DefectsTO)
                       .ThenInclude(d => d.SheffPassService)
                   .Include(c => c.DefectsTO)
                       .ThenInclude(d => d.ResponcePerson)
                   .Include(c => c.DefectsTO)
                       .ThenInclude(d => d.Driver)
                   .AsNoTracking()
                   .FirstOrDefaultAsync(c => c.CarId == id);
        }
    }
}
