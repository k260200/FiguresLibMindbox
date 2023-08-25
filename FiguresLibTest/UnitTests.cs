using FiguresLib;

namespace FiguresLibTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitializationTest()
        {
            Figure figure;
            Triangle triangle;
            Circle circle;
            Polygon polygon;

            Assert.DoesNotThrow(() =>
            {
                figure = new Circle();
                figure = new Triangle();
                figure = new Polygon();

                circle = new Circle(1);
                triangle = new Triangle(1, 1, 1);
                triangle = new Triangle(new List<decimal> { 1, 2, 2 });
                polygon = new Polygon(new List<decimal> { 1, 2, 2, 2, 5, 6 });

                triangle.Sides = new List<decimal> { 1, 2, 2 };
                polygon.Sides = new List<decimal> { 1, 2, 2, 2, 5, 6 };
            });

            triangle = new Triangle(1, 1, 1);
            Assert.That(triangle.AmountOfSides, Is.EqualTo(3));
            triangle = new Triangle();
            Assert.That(triangle.AmountOfSides, Is.EqualTo(3));

            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 1, 2, 2, 2, 5, 6 }); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 1 }); });
            Assert.Throws<ArgumentException>(() => { polygon = new Polygon(new List<decimal> { 1, 2 }); });

            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 1, 2, 100 }); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 100, 2, 1 }); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 1, 200, 1 }); });

            Assert.Throws<ArgumentException>(() => { circle = new Circle(0); });
            Assert.Throws<ArgumentException>(() => { circle = new Circle(-1); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 1, 2, -2}); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(new List<decimal> { 1, 2, 0 }); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(1, 2, 0); });
            Assert.Throws<ArgumentException>(() => { triangle = new Triangle(1, 2, -1); });

            List<decimal> nullList = null;
            Assert.Throws<ArgumentNullException>(() => { figure = new Triangle(nullList); });
            Assert.Throws<ArgumentNullException>(() => { figure = new Polygon(nullList); });

            triangle = new Triangle();
            polygon = new Polygon();
            Assert.Throws<ArgumentNullException>(() => { triangle.Sides = nullList; });
            Assert.Throws<ArgumentNullException>(() => { polygon.Sides = nullList; });
        }

        [Test]
        public void SquareCalculationTest()
        {
            var squareCalculatableObjectsAndSquares = new List<Tuple<ISquareCalculatable, decimal>>();

            squareCalculatableObjectsAndSquares.AddRange(new List<Tuple<ISquareCalculatable, decimal>>()
            { 
                new Tuple<ISquareCalculatable, decimal>(new Circle(1), Convert.ToDecimal(Math.PI)),
                new Tuple<ISquareCalculatable, decimal>(new Circle(2), Convert.ToDecimal(Math.PI) * 4m),
                new Tuple<ISquareCalculatable, decimal>(new Triangle(3, 4, 5), 6),
                new Tuple<ISquareCalculatable, decimal>(new Triangle(6, 8, 10), 24),
                new Tuple<ISquareCalculatable, decimal>(new Triangle(6, 5, 2.2m), 5.28m),
                new Tuple<ISquareCalculatable, decimal>(new Triangle(6, 8, 10), 24)
            }
            );

            foreach (var pair in squareCalculatableObjectsAndSquares)
                Assert.That(pair.Item1.GetSquare(), Is.EqualTo(pair.Item2));
        }

        [Test]
        public void TriangleIsRectangularTest()
        {
            var trianglesAndRectangularFlag = new List<Tuple<Triangle, bool>>();

            trianglesAndRectangularFlag.AddRange(new List<Tuple<Triangle, bool>>()
            {
                new Tuple<Triangle, bool>(new Triangle(3, 4, 5), true),
                new Tuple<Triangle, bool>(new Triangle(5, 4, 3), true),
                new Tuple<Triangle, bool>(new Triangle(5, 3, 4), true),
                new Tuple<Triangle, bool>(new Triangle(4, 3, 5), true),
                new Tuple<Triangle, bool>(new Triangle(4, 5, 3), true),
                new Tuple<Triangle, bool>(new Triangle(3, 5, 4), true),
                new Tuple<Triangle, bool>(new Triangle(5, 12, 13), true),
                new Tuple<Triangle, bool>(new Triangle(8, 15, 17), true),
                new Tuple<Triangle, bool>(new Triangle(9, 40, 41), true),
                new Tuple<Triangle, bool>(new Triangle(6, 8, 10), true),
                new Tuple<Triangle, bool>(new Triangle(6, 5, 2.2m), false),
                new Tuple<Triangle, bool>(new Triangle(6, 6, 6), false)
            }
            );

            foreach (var pair in trianglesAndRectangularFlag)
                Assert.That(pair.Item1.IsRectangular(), Is.EqualTo(pair.Item2), $"sides of triangle: {string.Join(",", pair.Item1.Sides)}");
        }

        [Test]
        public void PerimeterTest()
        {
            var perimeterCalculatableObjectsAndPerimeters = new List<Tuple<IPerimeterCalculatable, decimal>>();

            perimeterCalculatableObjectsAndPerimeters.AddRange(new List<Tuple<IPerimeterCalculatable, decimal>>()
            {
                new Tuple<IPerimeterCalculatable, decimal>(new Polygon(new List<decimal>{ 1m, 2m, 3m }), 6m),
                new Tuple<IPerimeterCalculatable, decimal>(new Polygon(new List<decimal>{ 4m, 4m, 3m, 5m }), 16m),
                new Tuple<IPerimeterCalculatable, decimal>(new Triangle(3, 4, 5), 12),
                new Tuple<IPerimeterCalculatable, decimal>(new Triangle(1, 1, 1), 3)
            }
            );

            foreach (var pair in perimeterCalculatableObjectsAndPerimeters)
                Assert.That(pair.Item1.GetPerimeter(), Is.EqualTo(pair.Item2));
        }

        [Test]
        public void EqualityTest()
        {
            // Порядок и "состав" сторон
            Polygon polygon1 = new Polygon(new List<decimal> { 1m, 2m, 3m });
            Polygon polygon2 = new Polygon(new List<decimal> { 3m, 2m, 1m });
            Polygon polygon3 = new Polygon(new List<decimal> { 3m, 2m, 3m });
            Polygon polygon4 = new Polygon(new List<decimal> { 3m, 3m, 2m });

            Assert.IsTrue(polygon1.Equals(polygon2));
            Assert.IsTrue(polygon2.Equals(polygon1));
            Assert.IsTrue(polygon3.Equals(polygon4));
            Assert.IsTrue(polygon4.Equals(polygon3));

            Assert.IsFalse(polygon1.Equals(polygon3));
            Assert.IsFalse(polygon3.Equals(polygon1));

            // Полиморфизм
            Figure figure1 = new Triangle(new List<decimal> { 3m, 2m, 3m });
            Triangle triangle1 = new Triangle(new List<decimal> { 3m, 2m, 3m });
            Circle circle1 = new Circle(3.14159m);

            Assert.IsFalse(figure1.Equals(triangle1));
            Assert.IsTrue(triangle1.Equals(figure1));
            Assert.IsFalse(figure1.Equals(null));
            Assert.IsFalse(figure1.Equals(circle1));
            Assert.IsFalse(triangle1.Equals(circle1));

            // Дроби
            Circle circle2 = new Circle(3.14159m);
            Circle circle3 = new Circle(2 * 3.14159m);

            Assert.IsTrue(circle1.Equals(circle2));
            Assert.IsFalse(circle2.Equals(circle3));
        }
    }
}