using TraceYourLife.Domain.Entities.Interfaces;
using TraceYourLife.Domain.Enums;

namespace TraceYourLife.Domain.Entities
{
    /// <summary>
    /// DE: Lebensmittel
    /// </summary>
    public class Food : IFood
    {
        public string Name { get; set; }
        public FoodKind Kind { get; set; }
        public int CaloriesPer100 { get; set; }
        public double Fiber { get; set; }
        public double Protein { get; set; }
    }
}
