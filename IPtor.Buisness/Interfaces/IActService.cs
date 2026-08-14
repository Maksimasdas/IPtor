using IPtor.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Interfaces
{
    public interface IActService
    {
        // Собрать данные для акта приёмки в ремонт
        Task<TransferActData> BuildTransferActDataAsync(int carId);

        // Собрать данные для акта тех. состояния
        Task<TOAktData> BuildTOAktDataAsync(int carId);
    }
}
