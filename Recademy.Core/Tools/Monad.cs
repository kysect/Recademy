using System;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Core.Tools;

public static class Monad
{
    public static TResult Maybe<TValue, TResult>(this TValue value, Func<TValue, TResult> just) where TValue : class
    {
        return value == null ? default : just(value);
    }

    public static TResult To<TValue, TResult>(this TValue value, Func<TValue, TResult> just)
    {
        return just(value);
    }

    public static IReadOnlyCollection<TResult> To<TValue, TResult>(this IEnumerable<TValue> value, Func<TValue, TResult> just)
    {
        return value.Select(just).ToList();
    }
}