using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Interfaces
{
    public interface IFood
    {
        string Name { get; set; }
        FoodKind Kind { get; set; }
        int CaloriesPer100 { get; set; }
        double Fiber { get; set; }
        double Protein { get; set; }
    }
}
