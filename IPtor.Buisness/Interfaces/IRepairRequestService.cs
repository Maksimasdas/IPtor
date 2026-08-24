using IPtor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPtor.Buisness.Interfaces
{
    public interface IRepairRequestService
    {
        Task<IEnumerable<RepairRequest>> GetAllRequestsAsync();
        Task AddRequestAsync(RepairRequest request);
        Task DeleteRequestAsync(int id);
        Task UpdateRequestAsync(RepairRequest request);
    }
}
