using System.Threading.Tasks;
using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Entities.Interfaces
{
    public interface IPerson
    {
        int Id { get; }
        string Name { get; set; }
        int Age { get; set; }
        int Height { get; set; }
        Gender Gender { get; set; }
        decimal StartWeight { get; set; }
        string Password { get; set; }
    }
}
