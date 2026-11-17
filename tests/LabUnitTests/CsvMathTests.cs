using BasicMath;
using System.Globalization;

namespace LabUnitTests;

public sealed class CsvMathTests
{
    public static TheoryData<double, double, double> AddRows => ReadRows("add_testdata.csv");
    public static TheoryData<double, double, double> SubtractRows => ReadRows("subtract_testdata.csv");
    public static TheoryData<double, double, double> MultiplyRows => ReadRows("multiply_testdata.csv");
    public static TheoryData<double, double, double> DivideRows => ReadRows("divide_testdata.csv");

    [Theory, MemberData(nameof(AddRows))]
    public void Add_FromCsv(double a, double b, double expected) => Assert.Equal(expected, new BasicMaths().Add(a, b));

    [Theory, MemberData(nameof(SubtractRows))]
    public void Subtract_FromCsv(double a, double b, double expected) => Assert.Equal(expected, new BasicMaths().Subtract(a, b));

    [Theory, MemberData(nameof(MultiplyRows))]
    public void Multiply_FromCsv(double a, double b, double expected) => Assert.Equal(expected, new BasicMaths().Multiply(a, b));

    [Theory, MemberData(nameof(DivideRows))]
    public void Divide_FromCsv(double a, double b, double expected) => Assert.Equal(expected, new BasicMaths().Divide(a, b));

    private static TheoryData<double, double, double> ReadRows(string fileName)
    {
        var data = new TheoryData<double, double, double>();
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", fileName);
        foreach (var line in File.ReadLines(path).Skip(1).Where(line => !string.IsNullOrWhiteSpace(line)))
        {
            var cells = line.Split(',');
            data.Add(
                double.Parse(cells[0], CultureInfo.InvariantCulture),
                double.Parse(cells[1], CultureInfo.InvariantCulture),
                double.Parse(cells[2], CultureInfo.InvariantCulture));
        }

        return data;
    }
}
