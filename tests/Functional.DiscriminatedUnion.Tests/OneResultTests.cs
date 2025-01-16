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
        Assert.Equal(expectedResult, actualResult);
        Assert.True(oneResult.IsT1);
    }

    [Fact]
    public void AsT2_WhenIsT2_ReturnsCorrectValue()
    {
        // Arrange
        var oneResult = Create();

        // Act & Assert
        Assert.Equal("test", oneResult.AsT2());
        Assert.True(oneResult.IsT2);
        Assert.False(oneResult.IsT1);
        var ex = Assert.Throws<InvalidOperationException>(() => oneResult.AsT1());
        Assert.Equal("Cannot cast value to T1", ex.Message);
        Assert.Equal("test", oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2));
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
        Assert.Equal(34, oneResult.AsT1());
        Assert.False(oneResult.IsT2);
        Assert.True(oneResult.IsT1);
        var ex = Assert.Throws<InvalidOperationException>(() => oneResult.AsT2());

        Assert.Equal("Cannot cast value to T2", ex.Message);
        Assert.Equal("34", oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2));
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
        Assert.True(oneResult.AsT3());
        Assert.True(oneResult.IsT3);
        Assert.False(oneResult.IsT2);
        Assert.False(oneResult.IsT1);

        var ex1 = Assert.Throws<InvalidOperationException>(() => oneResult.AsT1());
        var ex2 = Assert.Throws<InvalidOperationException>(() => oneResult.AsT2());

        Assert.Equal("Cannot cast value to T1", ex1.Message);
        Assert.Equal("Cannot cast value to T2", ex2.Message);
        Assert.Equal("True", oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString()));

        var matchWrong =
            Assert.Throws<InvalidOperationException>(() => oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2));
        Assert.Equal("Could not match Func, CurrentIndex with value 3 is out of boundaries", matchWrong.Message);
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
        Assert.Equal(1, oneResultT1.AsT1());
        Assert.Equal("test", oneResultT2.AsT2());
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
        Assert.Equal(55.5d, oneResult.AsT4());
        Assert.True(oneResult.IsT4);
        Assert.False(oneResult.IsT3);
        Assert.False(oneResult.IsT2);
        Assert.False(oneResult.IsT1);

        var actT1 = Assert.Throws<InvalidOperationException>(() => oneResult.AsT1());
        var actT2 = Assert.Throws<InvalidOperationException>(() => oneResult.AsT2());
        var actT3 = Assert.Throws<InvalidOperationException>(() => oneResult.AsT3());

        Assert.Equal("Cannot cast value to T1", actT1.Message);
        Assert.Equal("Cannot cast value to T2", actT2.Message);
        Assert.Equal("Cannot cast value to T3", actT3.Message);
        Assert.Equal("55.5",
            oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString(),
                fnt4 => fnt4.ToString(CultureInfo.InvariantCulture)));

        var matchWrongT1 = Assert.Throws<InvalidOperationException>(() => oneResult.Match(fnT1 => fnT1.ToString()));
        var matchWrongT2 =
            Assert.Throws<InvalidOperationException>(() => oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2));
        var matchWrongT3 = Assert.Throws<InvalidOperationException>(() =>
            oneResult.Match(fnT1 => fnT1.ToString(), fnT2 => fnT2, fnT3 => fnT3.ToString()));
        Assert.Equal("Could not match Func, CurrentIndex with value 4 is out of boundaries", matchWrongT1.Message);
        Assert.Equal("Could not match Func, CurrentIndex with value 4 is out of boundaries", matchWrongT2.Message);
        Assert.Equal("Could not match Func, CurrentIndex with value 4 is out of boundaries", matchWrongT3.Message);
        Assert.Equal("1",
            ((OneResult<int, string, bool, double>)1).Match(fnT1 => fnT1.ToString(), fnT2 => fnT2,
                fnT3 => fnT3.ToString(), fnt4 => fnt4.ToString(CultureInfo.InvariantCulture)));
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
        Assert.Equal(1, oneResultT1.AsT1());
        Assert.Equal("test", oneResultT2.AsT2());
        Assert.True(oneResultT3.AsT3());

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
        Assert.Equal(1, oneResultT1.AsT1());
        Assert.Equal("test", oneResultT2.AsT2());
        Assert.True(oneResultT3.AsT3());
        Assert.Equal(55.6d, oneResultT4.AsT4());
        var actT4 = Assert.Throws<InvalidOperationException>(() => oneResultT3.AsT4());
        Assert.Equal("Cannot cast value to T4", actT4.Message);
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

    [Fact]
    public void TryGetT1_ShouldReturnTrueWhenIsT1()
    {
        var oneResultT1 = (OneResult<int>)6;

        var result = oneResultT1.TryGetT1(out var t1);

        Assert.True(result);
        Assert.Equal(6, t1);
    }

    [Fact]
    public void TryGetT1_ShouldReturnFalseWhenIsNotT1()
    {
        var oneResultT1 = (OneResult<int, string>)"test";

        var result = oneResultT1.TryGetT1(out var t1);

        Assert.False(result);
        Assert.Equal(0, t1);
    }

    [Fact]
    public void TryGetT2_ShouldReturnTrueWhenIsT2()
    {
        var oneResultT2 = (OneResult<int, string>)"test";

        var result = oneResultT2.TryGetT2(out var t2);

        Assert.True(result);
        Assert.Equal("test", t2);
    }

    [Fact]
    public void TryGetT2_ShouldReturnFalseWhenIsNotT2()
    {
        var oneResultT2 = (OneResult<int, string>)1;

        var result = oneResultT2.TryGetT2(out var t2);

        Assert.False(result);
        Assert.Null(t2);
    }

    [Fact]
    public void TryGetT3_ShouldReturnTrueWhenIsT3()
    {
        var oneResult = (OneResult<int, string, bool>)true;

        var result = oneResult.TryGetT3(out var t3);

        Assert.True(result);
        Assert.True(t3);
    }

    [Fact]
    public void TryGetT3_ShouldReturnFalseWhenIsNotT3()
    {
        var oneResult = (OneResult<int, string, bool>)1;

        var result = oneResult.TryGetT3(out var t3);

        Assert.False(result);
        Assert.False(t3);
    }

    [Fact]
    public void TryGetT4_ShouldReturnTrueWhenIsT4()
    {
        var oneResult = (OneResult<int, string, bool, double>)7.5d;

        var result = oneResult.TryGetT4(out var t4);

        Assert.True(result);
        Assert.Equal(7.5d, t4);
    }

    [Fact]
    public void TryGetT4_ShouldReturnFalseWhenIsNotT4()
    {
        var oneResult = (OneResult<int, string, bool, double>)"test";

        var result = oneResult.TryGetT4(out var t4);

        Assert.False(result);
        Assert.Equal(0, t4);
    }

    [Fact]
    public void When_ShouldExecuteActionWhenIsT1()
    {
        var oneResult = (OneResult<int>)1;

        var value = 0;
        oneResult.When(actT1 => { value = actT1; });

        Assert.Equal(oneResult.AsT1(), value);
    }

    [Fact]
    public void When_ShouldExecuteActionWhenIsT2()
    {
        var oneResult = (OneResult<int, string>)"test";

        int? valueT1 = null;
        var valueT2 = string.Empty;
        oneResult.When(actT1 => { valueT1 = actT1; }, actT2 => { valueT2 = actT2; });

        Assert.Null(valueT1);
        Assert.Equal(oneResult.AsT2(), valueT2);
    }

    [Fact]
    public void When_ShouldExecuteActionWhenIsT3()
    {
        var oneResult = (OneResult<int, string, bool>)true;

        int? valueT1 = null;
        var valueT2 = string.Empty;
        var valueT3 = false;
        oneResult.When(actT1 => { valueT1 = actT1; }, actT2 => { valueT2 = actT2; }, actT3 => { valueT3 = actT3; });

        Assert.Null(valueT1);
        Assert.Equal(string.Empty, valueT2);
        Assert.Equal(oneResult.AsT3(), valueT3);
    }

    [Fact]
    public void When_ShouldExecuteActionWhenIsT4()
    {
        var oneResult = (OneResult<int, string, bool, double>)12.5d;

        int? valueT1 = null;
        var valueT2 = string.Empty;
        var valueT3 = false;
        var valueT4 = 0d;
        oneResult.When(actT1 => { valueT1 = actT1; }, actT2 => { valueT2 = actT2; }, actT3 => { valueT3 = actT3; },
            actT4 => { valueT4 = actT4; });

        Assert.Null(valueT1);
        Assert.Equal(string.Empty, valueT2);
        Assert.False(valueT3);
        Assert.Equal(oneResult.AsT4(), valueT4);
    }

    [Fact]
    public void When_ShouldExecuteActionOfT1WhenIsOneResultOfElements()
    {
        var oneResult = (OneResult<int, string, bool, double>)1;

        int? valueT1 = null;
        var valueT2 = string.Empty;
        var valueT3 = false;
        var valueT4 = 0d;
        oneResult.When(actT1 => { valueT1 = actT1; }, actT2 => { valueT2 = actT2; }, actT3 => { valueT3 = actT3; },
            actT4 => { valueT4 = actT4; });

        Assert.Equal(oneResult.AsT1(), valueT1);
        Assert.Equal(string.Empty, valueT2);
        Assert.False(valueT3);
        Assert.Equal(0d, valueT4);
    }
}