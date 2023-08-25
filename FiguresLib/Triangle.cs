using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Triangle : Polygon, ISquareCalculatable, IEquatable<Triangle>
    {
        public override int AmountOfSides
        {
            get => 3;
        }

        public override IEnumerable<decimal> Sides 
        { 
            get => base.Sides; 
            set
            {
                try
                {
                    CheckSidesAreValid(value, expectedAmountOfSides: AmountOfSides);
                }
                catch
                {
                    throw;
                }

                var orderedSides = value
                    .OrderByDescending(s => s)
                    .ToList();

                // стороны треугольника (для удобства и читаемости)
                var a = orderedSides.ElementAt(0); // после упорядочивания a - вероятная сторона, нарушающая условие
                                            // "ни одна из сторон треугольника не может быть больше суммы двух других его сторон"
                var b = orderedSides.ElementAt(1);
                var c = orderedSides.ElementAt(2);

                if (a >= b + c)
                    throw new ArgumentException($"Side {a} is more than {b} + {c}!");

                _sides = value.ToList();
            }
        }


        public Triangle() => _amountOfSides = AmountOfSides;

        public Triangle(decimal firstSide, decimal secondSide, decimal thirdSide) 
        {
            var sides = new List<decimal>() { firstSide, secondSide, thirdSide };

            _amountOfSides = AmountOfSides;
            Sides = sides;
        }

        public Triangle(IEnumerable<decimal> sides)
        {
            _amountOfSides = AmountOfSides;
            Sides = sides;
        }

        /// <summary>Треугольник - прямоугольный</summary>
        public bool IsRectangular()
        {
            var orderedSides = Sides
                .OrderByDescending(s => s)
                .ToList();

            // стороны треугольника (для удобства и читаемости)
            var c = orderedSides.ElementAt(0); // после упорядочивания c - вероятная гипотенуза
            var b = orderedSides.ElementAt(1);
            var a = orderedSides.ElementAt(2); 

            return (a * a + b * b == c * c);
        }

        public decimal GetSquare()
        {
            var p = GetPerimeter() / 2;

            // стороны треугольника (для удобства и читаемости)
            var a = Sides.ElementAt(0);
            var b = Sides.ElementAt(1);
            var c = Sides.ElementAt(2);

            // формула Герона
            return Convert.ToDecimal(
                Math.Sqrt(
                    Convert.ToDouble(p * (p - a) * (p - b) * (p - c))
                    )
                );
        }

        public bool Equals(Triangle? other) => base.Equals(other);

        public bool Equals(Object? other)
        {
            var otherObj = other as Triangle;
            if (otherObj is null)
                return false;

            return Equals(otherObj);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
