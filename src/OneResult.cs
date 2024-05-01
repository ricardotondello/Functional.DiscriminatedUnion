using System;

namespace Functional.DiscriminatedUnion;

public record OneResult<T1>
{
    protected readonly object Value = null!;

    public bool IsT1 => CurrentIndex == 1;

    public T1 AsT1() => IsT1 ? (T1)Value : throw new InvalidOperationException("Cannot cast value to T1");

    public static implicit operator OneResult<T1>(T1 result) => new(1, result!);

    public TResult Match<TResult>(Func<T1, TResult> onResult1) =>
        CurrentIndex switch
        {
            1 => onResult1((T1)Value),
            _ => throw new InvalidOperationException(
                $"Could not match Func, CurrentIndex with value {CurrentIndex} is out of boundaries")
        };

    protected readonly byte CurrentIndex;

    protected OneResult(byte index, object value)
    {
        CurrentIndex = index;
        Value = value;
    }
}

public record OneResult<T1, T2> : OneResult<T1>
{
    protected OneResult(byte index, object value) : base(index, value)
    {
    }

    public bool IsT2 => CurrentIndex == 2;

    public T2 AsT2() => IsT2 ? (T2)Value : throw new InvalidOperationException("Cannot cast value to T2");

    public static implicit operator OneResult<T1, T2>(T1 result) => new(1, result!);

    public static implicit operator OneResult<T1, T2>(T2 result) => new(2, result!);

    public TResult Match<TResult>(Func<T1, TResult> onResult1, Func<T2, TResult> onResult2) =>
        CurrentIndex switch
        {
            2 => onResult2((T2)Value),
            _ => base.Match(onResult1)
        };
}

public record OneResult<T1, T2, T3> : OneResult<T1, T2>
{
    protected OneResult(byte index, object value) : base(index, value)
    {
    }

    public bool IsT3 => CurrentIndex == 3;

    public T3 AsT3() => IsT3 ? (T3)Value : throw new InvalidOperationException("Cannot cast value to T3");

    public static implicit operator OneResult<T1, T2, T3>(T1 result) => new(1, result!);

    public static implicit operator OneResult<T1, T2, T3>(T2 result) => new(2, result!);

    public static implicit operator OneResult<T1, T2, T3>(T3 result) => new(3, result!);

    public TResult Match<TResult>(Func<T1, TResult> onResult1,
        Func<T2, TResult> onResult2, Func<T3, TResult> onResult3) =>
        CurrentIndex switch
        {
            3 => onResult3((T3)Value),
            _ => base.Match(onResult1, onResult2)
        };
}

public record OneResult<T1, T2, T3, T4> : OneResult<T1, T2, T3>
{
    private OneResult(byte index, object value) : base(index, value)
    {
    }

    public bool IsT4 => CurrentIndex == 4;

    public T4 AsT4() => IsT4 ? (T4)Value : throw new InvalidOperationException("Cannot cast value to T4");

    public static implicit operator OneResult<T1, T2, T3, T4>(T1 result) => new(1, result!);

    public static implicit operator OneResult<T1, T2, T3, T4>(T2 result) => new(2, result!);

    public static implicit operator OneResult<T1, T2, T3, T4>(T3 result) => new(3, result!);

    public static implicit operator OneResult<T1, T2, T3, T4>(T4 result) => new(4, result!);

    public TResult Match<TResult>(Func<T1, TResult> onResult1, Func<T2, TResult> onResult2, Func<T3, TResult> onResult3,
        Func<T4, TResult> onResult4) =>
        CurrentIndex switch
        {
            4 => onResult4((T4)Value),
            _ => base.Match(onResult1, onResult2, onResult3)
        };
}