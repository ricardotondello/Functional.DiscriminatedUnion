using System;

namespace Functional.DiscriminatedUnion;


public record OneResult<T1>
{
    protected readonly object Value = null!;

    public bool IsT1 => CurrentIndex == 1;

    public T1 AsT1() => IsT1 ? (T1)Value : throw new InvalidOperationException("Cannot cast value to T1");

    public static implicit operator OneResult<T1>(T1 result) => new(1, result!);

    public TResult Match<TResult>(Func<T1, TResult> fnT1) =>
        CurrentIndex switch
        {
            1 => fnT1((T1)Value),
            _ => throw new InvalidOperationException(
                $"Could not match Func, CurrentIndex with value {CurrentIndex} is out of boundaries")
        };

    public bool TryGetT1(out T1 t1)
    {
        t1 = default!;

        if (!IsT1)
        {
            return false;
        }

        t1 = AsT1();
        return true;
    }

    public void When(Action<T1> actT1)
    {
        if (IsT1)
        {
            actT1(AsT1());
        }
    }

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

    public TResult Match<TResult>(Func<T1, TResult> fnT1, Func<T2, TResult> fnT2) =>
        CurrentIndex switch
        {
            2 => fnT2((T2)Value),
            _ => base.Match(fnT1)
        };

    public bool TryGetT2(out T2 t2)
    {
        t2 = default!;

        if (!IsT2)
        {
            return false;
        }

        t2 = AsT2();
        return true;
    }

    public void When(Action<T1> actT1, Action<T2> actT2)
    {
        if (IsT2)
        {
            actT2(AsT2());
            return;
        }

        base.When(actT1);
    }
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

    public TResult Match<TResult>(Func<T1, TResult> fnT1, Func<T2, TResult> fnT2, Func<T3, TResult> fnT3) =>
        CurrentIndex switch
        {
            3 => fnT3((T3)Value),
            _ => base.Match(fnT1, fnT2)
        };

    public bool TryGetT3(out T3 t3)
    {
        t3 = default!;

        if (!IsT3)
        {
            return false;
        }

        t3 = AsT3();
        return true;
    }

    public void When(Action<T1> actT1, Action<T2> actT2, Action<T3> actT3)
    {
        if (IsT3)
        {
            actT3(AsT3());
            return;
        }

        base.When(actT1, actT2);
    }
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

    public TResult Match<TResult>(Func<T1, TResult> fnT1, Func<T2, TResult> fnT2, Func<T3, TResult> fnT3,
        Func<T4, TResult> fnT4) =>
        CurrentIndex switch
        {
            4 => fnT4((T4)Value),
            _ => base.Match(fnT1, fnT2, fnT3)
        };

    public bool TryGetT4(out T4 t4)
    {
        t4 = default!;

        if (!IsT4)
        {
            return false;
        }

        t4 = AsT4();
        return true;
    }

    public void When(Action<T1> actT1, Action<T2> actT2, Action<T3> actT3, Action<T4> actT4)
    {
        if (IsT4)
        {
            actT4(AsT4());
            return;
        }

        base.When(actT1, actT2, actT3);
    }
}
