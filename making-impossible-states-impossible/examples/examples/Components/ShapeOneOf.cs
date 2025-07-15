using OneOf;
namespace examples.Components;


public record Circle(double Radius);
public record Rectangle(double Length, double Width);
public record Triangle(double Base, double Height);


public class Shape : OneOfBase<Circle, Rectangle, Triangle>
{
    Shape(OneOf<Circle, Rectangle, Triangle> _) : base(_) { }
}

public class ShapeOneOf
{
    public void test()
    {
        Shape circle = new Circle(0.5);
        var area = Area(circle);
    }

    OneOf<Circle, Rectangle, Triangle> shape = new Circle(10);
    public static double Area(OneOf<Circle, Rectangle, Triangle> shape) {
        return shape.Match(
            circle => 3.14 * circle.Radius * circle.Radius,
            rectangle => rectangle.Length * rectangle.Width,
            triangle => triangle.Base * triangle.Height / 2
        );
    }

}