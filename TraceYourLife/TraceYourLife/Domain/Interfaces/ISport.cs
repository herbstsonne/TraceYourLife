using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Interfaces
{
    public interface ISport
    {
        string Name { get; set; }
        SportKind Kind { get; set; }
        int CaloriesPer60 { get; set; }
    }
}
