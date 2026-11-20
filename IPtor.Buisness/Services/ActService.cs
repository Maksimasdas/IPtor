using IPtor.Buisness.Helpers;
using IPtor.Buisness.Interfaces;
using IPtor.Buisness.Models;
using IPtor.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPtor.Buisness.Services
{
    public class ActService : IActService
    {
        private readonly IUnitOfWork _uow;

        public ActService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<TOAktData> BuildTOAktDataAsync(int carId)
        {
            return await _uow.ExecuteWithLockAsync(async () =>
            {
                var car = await _uow.Cars.GetCarWithFullInfoAsync(carId);
                if (car == null) return null;

                return new TOAktData
                {
                    Car = car,
                };
            });
        }

        public async Task<TransferActData> BuildTransferActDataAsync(int carId)
        {
            return await _uow.ExecuteWithLockAsync(async () =>
            {
                var car = await _uow.Cars.GetCarWithFullInfoAsync(carId);
                if (car == null) return null;

                return new TransferActData
                {
                    Car = car
                };
            });
        }
    }
}
