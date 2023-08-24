using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Circle : Figure, ISquareCalculatable, IEquatable<Circle>
    {
        decimal _radius;

        public decimal Radius 
        { 
            get => _radius; 
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Radius must be more than zero!");
                _radius = value;
            }
        }

        public Circle()
        {
            _radius = 0;
        }

        public Circle(decimal radius)
        {
            try
            {
                Radius = radius;
            }
            catch
            {
                throw;
            }
        }

        public decimal GetSquare() => Convert.ToDecimal(Math.PI) * Radius * Radius;

        public bool Equals(Circle? other)
        {
            if (other is null)
                return false;

            if (other.Radius != Radius)
                return false;

            return true;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Circle;
            if (other is null)
                return false;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Radius);
        }
    }
}
