using System;
using TraceYourLife.Domain.Entities;

namespace TraceYourLife.Domain.Manager.Interfaces
{
    public interface ICycleDataManager
    {
        CycleData CreateCycle(CycleData cycleDataEntry);

        void UpdateCurrentCyle(CycleData cycleDataEntry);

        CycleData GetCurrentCycle();
    }
}
