using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB_UTILS;

namespace REG_MARK_LIB
{

    public class RegNumberRange : IEnumerator<RegNumber>
    {
        public RegNumber Current { get; private set; }

        object IEnumerator.Current => Current;

        private RegNumber _stop;

        public RegNumberRange(RegNumber start, RegNumber stop)
        {
            Current = start;
            _stop = stop;
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (Current < _stop)
            {
                Current++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            Current = new RegNumber(new RegNumberSeries(), new RegNumberNumber(000), 0);
        }

        public IEnumerator<RegNumber> GetEnumerator()
        {
            return this;
        }
    }

    /// <summary>
    /// Сущность регистрационного номера автомобиля
    /// </summary>
    public class RegNumber
    {
        /// <summary>
        /// Серия регистрационного номера
        /// </summary>
        public RegNumberSeries Series { get; set; }

        /// <summary>
        /// Номер регистрационного номера
        /// </summary>
        public RegNumberNumber Number { get; set; }

        /// <summary>
        /// Код региона регистрации
        /// </summary>
        public int RegionCode { get; set; }


        public RegNumber(RegNumberSeries series, RegNumberNumber number, int regionCode)
        {
            Series = series;
            Number = number;
            RegionCode = regionCode;
        }

        public override string ToString()
        {
            var series = Series.ToString();
            return $"{string.Join("", series.Take(1))}{Number}{string.Join("", series.Skip(1).Take(2))}{RegionCode}";
        }

        public static RegNumber operator + (RegNumber regNumber, int other)
        {
            var number = regNumber.Number;
            var series = regNumber.Series;

            try
            {
                return new RegNumber(series, number + other, regNumber.RegionCode); // пытаемся увеличить номер
            } catch (OverflowException)
            {
                series += 1; // если не получилось, меняем серию. Тут тоже может быть OverflowException, но пока что пох
                number = new RegNumberNumber(001);
                return new RegNumber(series, number, regNumber.RegionCode);
            }
        }

        public static RegNumber operator ++ (RegNumber regNumber)
        {
            return regNumber + 1;
        }

        public static bool operator <= (RegNumber a, RegNumber b)
        {
            if (a.Series < b.Series)
            {
                return true;
            } else if (a.Series > b.Series)
            {
                return false;
            }
            return a.Number <= b.Number;
        }

        public static bool operator <(RegNumber a, RegNumber b)
        {
            if (a.Series < b.Series)
            {
                return true;
            }
            else if (a.Series > b.Series)
            {
                return false;
            }
            return a.Number < b.Number;
        }

        public static bool operator >(RegNumber a, RegNumber b)
        {
            if (a.Series > b.Series)
            {
                return true;
            }
            else if (a.Series < b.Series)
            {
                return false;
            }
            return a.Number > b.Number;
        }

        public static bool operator >=(RegNumber a, RegNumber b)
        {
            if (a.Series > b.Series)
            {
                return true;
            }
            else if (a.Series < b.Series)
            {
                return false;
            }
            return a.Number >= b.Number;
        }

        public static RegNumberRange Range(RegNumber from, RegNumber to)
        {
            return new RegNumberRange(from, to);
        }
    }
}
