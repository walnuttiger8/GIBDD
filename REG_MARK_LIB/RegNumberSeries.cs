using SB_UTILS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REG_MARK_LIB
{
    /// <summary>
    /// Сущность для представления серии регистрационного номера автомобиля
    /// </summary>
    public class RegNumberSeries
    {

        private static readonly List<string> _ALPHABET = new List<string>() { "a", "b", "e", "k", "m", "h", "o", "p", "c", "t", "y", "x" };
        private const int _MIN_STR_LEN = 3;
        private const int _MAX_POSITIONS = 3;

        private List<string> _value;
        public List<string> Value { 
            get 
            {
                return _value;
            } 
            set 
            {
                foreach (var item in value)
                {
                    if (!_ALPHABET.Contains(item))
                    {
                        throw new ArgumentException($"недопустимый символ - {item}");
                    }
                }
                if (value.Count > _MAX_POSITIONS)
                {
                    throw new ArgumentException("превышено число позиций в серии");
                }
                _value = value;
            } 
        }

        public RegNumberSeries(string value)
        {
            Value = value.Select(x => x.ToString()).ToList();
        }

        public RegNumberSeries(List<string> value)
        {
            Value = value;
        }

        public RegNumberSeries(int value=0)
        {
            Value = NFoldConverter.FromDec(value, _ALPHABET);
        }

        public override string ToString()
        {
            var result = string.Join("", Value);
            while (result.Length < _MIN_STR_LEN)
            {
                result = _ALPHABET[0] + result;
            }
            return result;
        }

        public static List<RegNumberSeries> Range(RegNumberSeries from, RegNumberSeries to)
        {
            var start = from;
            var result = new List<RegNumberSeries>();

            while (start <= to)
            {
                result.Add(start);
                start++;
            }
            return result;
        }

        public static RegNumberSeries operator +(RegNumberSeries number, int other)
        {
            var v = NFoldConverter.ToDec(number.Value, _ALPHABET);
            try
            {
                return new RegNumberSeries(v + other);
            } catch
            {
                throw new OverflowException();
            }
            
        }

        public static RegNumberSeries operator -(RegNumberSeries number, int other)
        {
            var v = NFoldConverter.ToDec(number.Value, _ALPHABET);
            return new RegNumberSeries(v - other);
        }

        public static RegNumberSeries operator ++ (RegNumberSeries series)
        {
            return series + 1;
        }

        public static bool operator <(RegNumberSeries a, RegNumberSeries b)
        {
            var v1 = NFoldConverter.ToDec(a.Value, _ALPHABET);
            var v2 = NFoldConverter.ToDec(b.Value, _ALPHABET);

            return v1 < v2;
        }

        public static bool operator >(RegNumberSeries a, RegNumberSeries b)
        {
            var v1 = NFoldConverter.ToDec(a.Value, _ALPHABET);
            var v2 = NFoldConverter.ToDec(b.Value, _ALPHABET);

            return v1 > v2;
        }

        public static bool operator <=(RegNumberSeries a, RegNumberSeries b)
        {
            var v1 = NFoldConverter.ToDec(a.Value, _ALPHABET);
            var v2 = NFoldConverter.ToDec(b.Value, _ALPHABET);

            return v1 <= v2;
        }

        public static bool operator >=(RegNumberSeries a, RegNumberSeries b)
        {
            var v1 = NFoldConverter.ToDec(a.Value, _ALPHABET);
            var v2 = NFoldConverter.ToDec(b.Value, _ALPHABET);

            return v1 >= v2;
        }
    }
}
