using SB_UTILS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REG_MARK_LIB
{
    public class RegNumberSeries
    {

        private static readonly List<string> _ALPHABET = new List<string>() { "a", "b", "e", "k", "m", "h", "o", "p", "c", "t", "y", "x" };
        private const int _MIN_STR_LEN = 3;
        private const int _MAX_POSITIONS = 3;

        public List<string> Value { get; set; }

        public RegNumberSeries(string value)
        {
            Value = value.Select(x => x.ToString()).ToList();
            if (Value.Count > _MAX_POSITIONS)
            {
                throw new ArgumentException("превышено число позиций в серии");
            }
        }

        public RegNumberSeries(List<string> value)
        {
            Value = value;
            if (Value.Count > _MAX_POSITIONS)
            {
                throw new ArgumentException("превышено число позиций в серии");
            }
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

        public static RegNumberSeries operator +(RegNumberSeries number, int other)
        {
            var v = NFoldConverter.ToDec(number.Value, _ALPHABET);
            return new RegNumberSeries(NFoldConverter.FromDec(v + other, _ALPHABET));
        }

        public static RegNumberSeries operator -(RegNumberSeries number, int other)
        {
            var v = NFoldConverter.ToDec(number.Value, _ALPHABET);
            return new RegNumberSeries(NFoldConverter.FromDec(v - other, _ALPHABET));
        }
    }
}
