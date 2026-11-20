using IPtor.Buisness.Helpers;
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
            return await _uow.ExecuteWithLockAsync(async () =>
            {
                return await _uow.RepairRequests.GetAllAsync();
            });
        }
        public async Task AddRequestAsync(RepairRequest request)
        {
            await _uow.ExecuteWithLockAsync(async () =>
            {
                await _uow.RepairRequests.AddAsync(request);
                await _uow.SaveChangesAsync();
            });
        }
        public async Task DeleteRequestAsync(int id)
        {
            await _uow.ExecuteWithLockAsync(async () =>
            {
                var request = await _uow.RepairRequests.GetByIdAsync(id);
                if (request != null)
                {
                    _uow.RepairRequests.Delete(request);
                    await _uow.SaveChangesAsync();
                }
            });
        }

        public async Task UpdateRequestAsync(RepairRequest request)
        {
            await _uow.ExecuteWithLockAsync(async () =>
            {
                var existing = await _uow.RepairRequests.GetByIdAsync(request.RepairRequestId);
                if (existing != null)
                {
                    existing.Description = request.Description;
                    existing.Status = request.Status;
                    await _uow.SaveChangesAsync();
                }
            });
        }
    }
}