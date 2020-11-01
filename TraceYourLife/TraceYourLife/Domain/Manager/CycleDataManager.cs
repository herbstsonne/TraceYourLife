using System;
using TraceYourLife.Database.Repositories;
using TraceYourLife.Domain.Entities;
using TraceYourLife.Domain.Manager.Interfaces;

namespace TraceYourLife.Domain.Manager
{
    public class CycleDataManager : ICycleDataManager
    {
        private readonly CycleDataRepository _cycleDataRepository;

        public CycleDataManager()
        {
            _cycleDataRepository = new CycleDataRepository();
        }

        public CycleData CreateCycle(CycleData cycleDataEntry)
        {
            return _cycleDataRepository.CreateCycleData(cycleDataEntry);
        }

        public void UpdateCurrentCyle(CycleData cycleDataEntry)
        {
            _cycleDataRepository.UpdateCycleData(cycleDataEntry);
        }

        public CycleData GetCurrentCycle()
        {
            return _cycleDataRepository.GetCurrentCycle();
        }
    }
}
