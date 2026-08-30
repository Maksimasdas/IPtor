using IPtor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPtor.Buisness.Interfaces
{
    public interface IRepairRequestService
    {
        Task<IEnumerable<RepairRequest>> GetAllRequestsAsync(); // получить все заявки в ремонт
        Task AddRequestAsync(RepairRequest request); // добавить заявку на ремонт
        Task DeleteRequestAsync(int id); // удалить заявку на ремонт
        Task UpdateRequestAsync(RepairRequest request); // обновить заявку на ремонт
    }
}
