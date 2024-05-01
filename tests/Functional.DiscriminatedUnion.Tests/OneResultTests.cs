using System.Globalization;

namespace Functional.DiscriminatedUnion.Tests;

public class OneResultTests
{
    [Fact]
    public void AsT1_WhenIsT1_ReturnsCorrectValue()
    {
        // Arrange
        const int expectedResult = 42;
        var oneResult = (OneResult<int>)42;

        // Act
        var actualResult = oneResult.AsT1();

        // Assert
        expectedResult.Should().Be(actualResult);
        oneResult.IsT1.Should().BeTrue();
    }

    [Fact]
    public void AsT2_WhenIsT2_ReturnsCorrectValue()
    {
        // Arrange
        var oneResult = Create();

        // Act & Assert
        oneResult.AsT2().Should().Be("test");
        oneResult.IsT2.Should().BeTrue();
        oneResult.IsT1.Should().BeFalse();
        Action act = () => oneResult.AsT1();

        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T1");
        oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2).Should().Be("test");
        return;

        OneResult<int, string> Create()
        {
            return "test";
        }
    }

    [Fact]
    public void AsT1_WhenIsT2_ReturnsCorrectValue()
    {
        // Arrange
        var oneResult = Create();

        // Act & Assert
        oneResult.AsT1().Should().Be(34);
        oneResult.IsT2.Should().BeFalse();
        oneResult.IsT1.Should().BeTrue();
        Action act = () => oneResult.AsT2();

        act.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T2");
        oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2).Should().Be("34");
        return;

        OneResult<int, string> Create()
        {
            return 34;
        }
    }

    [Fact]
    public void AsT3_ReturnsCorrectValue()
    {
        // Arrange
        var oneResult = Create();

        // Act & Assert
        oneResult.AsT3().Should().BeTrue();
        oneResult.IsT3.Should().BeTrue();
        oneResult.IsT2.Should().BeFalse();
        oneResult.IsT1.Should().BeFalse();

        Action actT1 = () => oneResult.AsT1();
        Action actT2 = () => oneResult.AsT2();

        actT1.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T1");
        actT2.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T2");
        oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString()).Should().Be("True");

        Action matchWrong = () => oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2);
        matchWrong.Should().Throw<InvalidOperationException>()
            .WithMessage("Could not match Func, CurrentIndex with value 3 is out of boundaries");
        return;

        OneResult<int, string, bool> Create()
        {
            return true;
        }
    }

    [Fact]
    public void AsT3_ImplicitT1T2()
    {
        // Arrange
        var oneResultT1 = CreateT1();
        var oneResultT2 = CreateT2();

        // Act & Assert
        oneResultT1.AsT1().Should().Be(1);
        oneResultT2.AsT2().Should().Be("test");
        return;

        OneResult<int, string, bool> CreateT1()
        {
            return 1;
        }

        OneResult<int, string, bool> CreateT2()
        {
            return "test";
        }
    }

    [Fact]
    public void AsT4_ReturnsCorrectValue()
    {
        // Arrange
        var oneResult = Create();

        // Act & Assert
        oneResult.AsT4().Should().Be(55.5d);
        oneResult.IsT4.Should().BeTrue();
        oneResult.IsT3.Should().BeFalse();
        oneResult.IsT2.Should().BeFalse();
        oneResult.IsT1.Should().BeFalse();

        Action actT1 = () => oneResult.AsT1();
        Action actT2 = () => oneResult.AsT2();
        Action actT3 = () => oneResult.AsT3();

        actT1.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T1");
        actT2.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T2");
        actT3.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T3");
        oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString(),
            fnt4 => fnt4.ToString(CultureInfo.InvariantCulture)).Should().Be("55.5");

        Action matchWrongT1 = () => oneResult.Match(fnT1 => fnT1.ToString());
        Action matchWrongT2 = () => oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2);
        Action matchWrongT3 = () => oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString());
        matchWrongT1.Should().Throw<InvalidOperationException>()
            .WithMessage("Could not match Func, CurrentIndex with value 4 is out of boundaries");
        matchWrongT2.Should().Throw<InvalidOperationException>()
            .WithMessage("Could not match Func, CurrentIndex with value 4 is out of boundaries");
        matchWrongT3.Should().Throw<InvalidOperationException>()
            .WithMessage("Could not match Func, CurrentIndex with value 4 is out of boundaries");
        ((OneResult<int, string, bool, double>)1).Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString(),
            fnt4 => fnt4.ToString(CultureInfo.InvariantCulture)).Should().Be("1");
        return;

        OneResult<int, string, bool, double> Create()
        {
            return 55.5d;
        }
    }

    [Fact]
    public void AsT3_ImplicitT1T2T3()
    {
        // Arrange
        var oneResultT1 = CreateT1();
        var oneResultT2 = CreateT2();
        var oneResultT3 = CreateT3();

        // Act & Assert
        oneResultT1.AsT1().Should().Be(1);
        oneResultT2.AsT2().Should().Be("test");
        oneResultT3.AsT3().Should().BeTrue();

        return;

        OneResult<int, string, bool> CreateT1()
        {
            return 1;
        }

        OneResult<int, string, bool> CreateT2()
        {
            return "test";
        }

        OneResult<int, string, bool> CreateT3()
        {
            return true;
        }
    }

    [Fact]
    public void AsT4_ImplicitT1T2T3T4()
    {
        // Arrange
        var oneResultT1 = CreateT1();
        var oneResultT2 = CreateT2();
        var oneResultT3 = CreateT3();
        var oneResultT4 = CreateT4();

        // Act & Assert
        oneResultT1.AsT1().Should().Be(1);
        oneResultT2.AsT2().Should().Be("test");
        oneResultT3.AsT3().Should().BeTrue();
        oneResultT4.AsT4().Should().Be(55.6d);
        Action actT4 = () => oneResultT3.AsT4();
        actT4.Should().Throw<InvalidOperationException>().WithMessage("Cannot cast value to T4");
        return;

        OneResult<int, string, bool, double> CreateT1()
        {
            return 1;
        }

        OneResult<int, string, bool, double> CreateT2()
        {
            return "test";
        }

        OneResult<int, string, bool, double> CreateT3()
        {
            return true;
        }

        OneResult<int, string, bool, double> CreateT4()
        {
            return 55.6d;
        }
    }
}