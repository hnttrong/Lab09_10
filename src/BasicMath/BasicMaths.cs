namespace BasicMath;

public sealed class BasicMaths
{
    public double Add(double first, double second) => first + second;

    public double Subtract(double first, double second) => first - second;

    public double Multiply(double first, double second) => first * second;

    public double Divide(double dividend, double divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }

        return dividend / divisor;
    }
}
