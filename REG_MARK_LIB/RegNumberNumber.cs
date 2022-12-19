using System;
using System.Collections.Generic;

namespace REG_MARK_LIB
{
    public class RegNumberNumber
    {
        private const int _MAX_VALUE = 999;
        private const int _MIN_VALUE = 0;

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (value > _MAX_VALUE)
                {
                    throw new ArgumentException($"номер не должен превышать {_MAX_VALUE}");
                }
                if (value < _MIN_VALUE)
                {
                    throw new ArgumentException($"номер не должен быть меньше {_MIN_VALUE}");
                }
                _value = value;
            }
        }
        #region Операторы
        public static RegNumberNumber operator +(RegNumberNumber number, int other)
        {
            try
            {
                return new RegNumberNumber(number.Value + other);
            }
            catch (ArgumentException)
            {
                throw new OverflowException();
            }
        }

        public static RegNumberNumber operator -(RegNumberNumber number, int other)
        {
            try
            {
                return new RegNumberNumber(number.Value - other);
            }
            catch (ArgumentException)
            {
                throw new OverflowException();
            }
        }

        public static RegNumberNumber operator ++(RegNumberNumber number)
        {
            return new RegNumberNumber((number + 1).Value);
        }
        public static RegNumberNumber operator --(RegNumberNumber number)
        {
            return new RegNumberNumber((number - 1).Value);
        }

        public static bool operator < (RegNumberNumber number, int other)
        {
            return number.Value < other;
        }

        public static bool operator <(RegNumberNumber number, RegNumberNumber other)
        {
            return number.Value < other.Value;
        }

        public static bool operator <=(RegNumberNumber number, RegNumberNumber other)
        {
            return number.Value <= other.Value;
        }

        public static bool operator >(RegNumberNumber number, int other)
        {
            return number.Value > other;
        }

        public static bool operator >(RegNumberNumber number, RegNumberNumber other)
        {
            return number.Value > other.Value;
        }

        public static bool operator >= (RegNumberNumber number, RegNumberNumber other)
        {
            return number.Value >= other.Value;
        }

        public static bool operator >=(RegNumberNumber number, int other)
        {
            return number.Value >= other;
        }

        public static bool operator <=(RegNumberNumber number, int other)
        {
            return number.Value <= other;
        }

        public static bool operator ==(RegNumberNumber number, RegNumberNumber other)
        {
            return number.Value == other.Value;
        }

        public static bool operator !=(RegNumberNumber number, RegNumberNumber other)
        {
            return number.Value != other.Value;
        }
        #endregion

        public static List<RegNumberNumber> Range(int from, int to)
        {
            var start = new RegNumberNumber(from);
            var result = new List<RegNumberNumber>();

            while (start <= to)
            {
                result.Add(start);
                start++;
            }
            return result;
        }

        public RegNumberNumber(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            var result = Value.ToString();
            while (result.Length < _MAX_VALUE.ToString().Length)
            {
                result = _MIN_VALUE.ToString() + result;
            }
            return result;
        }
    }
}
