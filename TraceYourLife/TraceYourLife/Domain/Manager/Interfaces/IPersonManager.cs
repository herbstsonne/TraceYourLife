using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TraceYourLife.Domain.Entities.Interfaces;

namespace TraceYourLife.Domain.Manager.Interfaces
{
    public interface IPersonManager
    {
        Task<IPerson> LoadFirstPerson();
        bool SavePerson(IPerson person);
        Task<IPerson> GetPerson(string name);
    }
}
