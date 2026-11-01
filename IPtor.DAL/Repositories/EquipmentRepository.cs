using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using IPtor.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.DAL.Repositories
{
    public class EquipmentRepository : EntityRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Equipment>> GetEquipmentByServiceAsync(ServiceType service)
        {
            return await _context.Equipment
                .Where(c => c.Service == service)  // Фильтр по службе
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentWithDetailsAsync()
        {
            return await _context.Equipment
               .AsNoTracking()             // Только чтение, не отслеживаем изменения
               .ToListAsync();
        }

        public async Task<Equipment> GetEquipmentWithFullInfoAsync(int id)
        {
            return await _context.Equipment
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EquipmentId == id);
        }
    }
}
