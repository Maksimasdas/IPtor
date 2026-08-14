using IPtor.Buisness.Interfaces;
using IPtor.DAL.Interfaces;
using IPtor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPtor.Buisness.Services
{
    public class RepairRequestService : IRepairRequestService
    {
        private readonly IUnitOfWork _uow;

        public RepairRequestService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<RepairRequest>> GetAllRequestsAsync()
        {
            // Используем _uow.RepairRequests + загружаем Car
            var requests = await _uow.RepairRequests.GetAllAsync();
            return requests;
        }
        public async Task AddRequestAsync(RepairRequest request)
        {
            await _uow.RepairRequests.AddAsync(request);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteRequestAsync(int id)
        {
            var request = await _uow.RepairRequests.GetByIdAsync(id);
            if (request != null)
            {
                _uow.RepairRequests.Delete(request);
                await _uow.SaveChangesAsync();
            }
        }
    }
}