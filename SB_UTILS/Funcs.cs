using System;
using System.Collections.Generic;
using System.Linq;


namespace SB_UTILS
{
    public class CompositionElement<TIn, TOut>
    {
        private Func<TIn, TOut> _func;

        public CompositionElement(Func<TIn, TOut> func)
        {
            _func = func;
        }

        public TOut Call(TIn input)
        {
            return _func(input);
        }

        public CompositionElement<TIn, TOutOther> Then<TOutOther>(Func<TOut, TOutOther> func)
        {
            return new CompositionElement<TIn, TOutOther>((input) => func(_func(input)));
        }

        public CompositionElement<TIn, TOutOther> Then<TOutOther>(CompositionElement<TOut, TOutOther> other)
        {
            return new CompositionElement<TIn, TOutOther>((input) => other.Call(_func(input)));
        }
    }

    public static class Funcs
    {
        public static IEnumerable<TOutput> Map<TInput, TOutput>(Func<TInput, TOutput> func, IEnumerable<TInput> collection)
        {
            return collection.Select(x => func(x));
        }

        public static IEnumerable<TInput> Filter<TInput>(Func<TInput, bool> func, IEnumerable<TInput> collection)
        {
            return collection.Where(x => func(x));
        }

        public static bool All<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                try
                {
                    if (Convert.ToBoolean(item) == false)
                    {
                        return false;
                    }
                } catch
                {
                    return true;
                }
            }
            return true;
        }

        public static IList<(T1, T2)> Zip<T1, T2>(IList<T1> collection1, IList<T2> collection2)
        {
            var result = new List<(T1, T2)>();
            for (var i = 0; i < collection1.Count; i++)
            {
                result.Add((collection1[i], collection2[i]));
            }
            return result;
        }

        public static CompositionElement<TIn, TOut> C<TIn, TOut>(Func<TIn, TOut> func)
        {
            return new CompositionElement<TIn, TOut>(func);
        }
    }
}
