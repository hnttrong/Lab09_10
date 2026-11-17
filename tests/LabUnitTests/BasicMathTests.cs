using BasicMath;

namespace LabUnitTests;

public sealed class BasicMathTests
{
    private readonly BasicMaths _math = new();

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, -2, -3)]
    [InlineData(0, 0, 0)]
    [InlineData(int.MaxValue, 1, 2147483648d)]
    public void Add_EquivalenceAndBoundaryValues_ReturnsExpected(double a, double b, double expected) =>
        Assert.Equal(expected, _math.Add(a, b));

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(-5, -3, -2)]
    [InlineData(0, 0, 0)]
    [InlineData(int.MinValue, 1, -2147483649d)]
    public void Subtract_EquivalenceAndBoundaryValues_ReturnsExpected(double a, double b, double expected) =>
        Assert.Equal(expected, _math.Subtract(a, b));

    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(-2, -3, 6)]
    [InlineData(0, 5, 0)]
    [InlineData(int.MaxValue, 1, int.MaxValue)]
    public void Multiply_EquivalenceAndBoundaryValues_ReturnsExpected(double a, double b, double expected) =>
        Assert.Equal(expected, _math.Multiply(a, b));

    [Theory]
    [InlineData(6, 3, 2)]
    [InlineData(-6, -3, 2)]
    [InlineData(0, 1, 0)]
    [InlineData(int.MaxValue, 1, int.MaxValue)]
    public void Divide_EquivalenceAndBoundaryValues_ReturnsExpected(double a, double b, double expected) =>
        Assert.Equal(expected, _math.Divide(a, b));

    [Fact]
    public void Divide_ByZero_Throws() => Assert.Throws<DivideByZeroException>(() => _math.Divide(1, 0));
}
