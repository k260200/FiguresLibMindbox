using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq;

namespace FiguresLib
{
    public class Polygon : Figure, IPerimeterCalculatable, IEquatable<Polygon>
    {
        public const int MinAmountOfSides = 3;

        protected int _amountOfSides = 0;
        protected List<decimal> _sides = new List<decimal>();

        public virtual int AmountOfSides {
            get => _amountOfSides;
            private set 
            {
                if (value < MinAmountOfSides)
                    throw new ArgumentException($"Amount of sides in polygon can not be less than {MinAmountOfSides}");

                _amountOfSides = value;
                _sides = new List<decimal>();
            } 
        }

        public virtual IEnumerable<decimal> Sides 
        {
            get => _sides ?? new List<decimal>();
            set
            {
                try
                {
                    CheckSidesAreValid(value);
                }
                catch
                {
                    throw;
                }

                _sides = value.ToList();
                _amountOfSides = _sides.Count();
            }
        }

        public Polygon() 
        {
            _amountOfSides = 0;
            _sides = new List<decimal>();
        }

        public Polygon(IEnumerable<decimal> sides) => Sides = sides;

        // Не делаем виртуальным т.к. периметр для всех многоугольников всегда будет считаться одинаково
        public decimal GetPerimeter() => Sides.Sum();

        public bool Equals(Polygon? other)
        {
            if (other is null)
                return false;
            if (other.AmountOfSides != this.AmountOfSides)
                return false;

            foreach (var side in other.Sides.Distinct())
                if (Sides.Count(s => s == side) != other.Sides.Count(s => s == side))
                    return false;
            return true;
        }

        public bool Equals(Object? other)
        {
            var otherObj = other as Polygon;
            if (otherObj is null)
                return false;

            return Equals(otherObj);
        }

        public override int GetHashCode() => HashCode.Combine(AmountOfSides, Sides);

        /// <summary>Проверка, что стороны корректны и могут быть присвоены многоугольнику. При ошибках генерирует исключения.</summary>
        protected void CheckSidesAreValid(IEnumerable<decimal> sides, int? expectedAmountOfSides = null)
        {
            if (sides is null)
                throw new ArgumentNullException("Can not set sides as null!");
            if (!expectedAmountOfSides.HasValue && sides.Count() < MinAmountOfSides)
                throw new ArgumentException($"Amount of sides in polygon can not be less than {MinAmountOfSides}!");
            if (expectedAmountOfSides.HasValue && sides.Count() != expectedAmountOfSides.Value)
                throw new ArgumentException($"Expected amount of sides is {MinAmountOfSides}!");
            if (sides.Any(s => s <= 0))
                throw new ArgumentException($"All sides must be more than zero!");
        }
    }
}
