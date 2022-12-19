using System;
using System.Collections.Generic;
using System.Linq;
using SB_UTILS;

namespace REG_MARK_LIB
{
    public static class RegMarkService
    {
        private static readonly List<char> _SERIES_ALPHABET = new List<char>() { 'a', 'b', 'e', 'k', 'm', 'h', 'o', 'p', 'c', 't', 'y', 'x'};
        private static readonly Dictionary<char, char> _ALPHABET_TRANSLATE_MAP = new Dictionary<char, char>()
        {
            { 'А', 'a' },
            { 'В', 'b' },
            { 'Е', 'e' },
            { 'К', 'k' },
            { 'М', 'm' },
            { 'Н', 'h' },
            { 'О', 'o' },
            { 'Р', 'p' },
            { 'С', 'c' },
            { 'Т', 't' },
            { 'У', 'y' },
            { 'Х', 'x' },
        };
        private static readonly Dictionary<string, List<int>> _REGIONS = new Dictionary<string, List<int>>()
        {
            { "Республика Адыгея", new List<int>() { 1 } },
            { "Республика Алтай", new List<int>() { 4 } },
            { "Республика Башкортостан", new List<int>() { 2, 102 } },
            { "Республика Бурятия", new List<int>() { 3 } },
            { "Республика Дагестан", new List<int>() { 5 } },
            { "Республика Ингушетия", new List<int>() { 6 } },
            { "Кабардино-Балкарская республика", new List<int>() { 7 } },
            { "Республика Калмыкия", new List<int>() { 8 } },
            { "Карачаево-Черкесская республика", new List<int>() { 9 } },
            { "Республика Карелия", new List<int>() { 10 } },
            { "Республика Коми", new List<int>() { 11, 111 } },
            { "Республика Крым", new List<int>() { 82, 91 } },
            { "Республика Марий Эл", new List<int>() { 12 } },
            { "Республика Мордовия", new List<int>() { 113, 13 } },
            { "Республика Саха (Якутия)", new List<int>() { 14 } },
            { "Республика Северная Осетия — Алания", new List<int>() { 15 } },
            { "Республика Татарстан", new List<int>() { 16, 116, 716 } },
            { "Республика Тыва", new List<int>() { 17 } },
            { "Удмуртская республика", new List<int>() { 18 } },
            { "Республика Хакасия", new List<int>() { 19 } },
            { "Чеченская республика", new List<int>() { 20, 95 } },
            { "Чувашская республика", new List<int>() { 121, 21 } },
            { "Алтайский край", new List<int>() { 22 } },
            { "Забайкальский край", new List<int>() { 80, 75 } },
            { "Камчатский край", new List<int>() { 41 } },
            { "Краснодарский край", new List<int>() { 123, 93, 23 } },
            { "Красноярский край", new List<int>() { 24, 88, 124, 84 } },
            { "Пермский край", new List<int>() { 81, 59, 159 } },
            { "Приморский край", new List<int>() { 25, 125 } },
            { "Ставропольский край", new List<int>() { 26, 126 } },
            { "Хабаровский край", new List<int>() { 27 } },
            { "Амурская область", new List<int>() { 28 } },
            { "Архангельская область", new List<int>() { 29 } },
            { "Астраханская область", new List<int>() { 30 } },
            { "Белгородская область", new List<int>() { 31 } },
            { "Брянская область", new List<int>() { 32 } },
            { "Владимирская область", new List<int>() { 33 } },
            { "Волгоградская область", new List<int>() { 34, 134 } },
            { "Вологодская область", new List<int>() { 35 } },
            { "Воронежская область", new List<int>() { 136, 36 } },
            { "Ивановская область", new List<int>() { 37 } },
            { "Иркутская область", new List<int>() { 138, 85, 38 } },
            { "Калининградская область", new List<int>() { 91, 39 } },
            { "Калужская область", new List<int>() { 40 } },
            { "Кемеровская область", new List<int>() { 42, 142 } },
            { "Кировская область", new List<int>() { 43 } },
            { "Костромская область", new List<int>() { 44 } },
            { "Курганская область", new List<int>() { 45 } },
            { "Курская область", new List<int>() { 46 } },
            { "Ленинградская область", new List<int>() { 47 } },
            { "Липецкая область", new List<int>() { 48 } },
            { "Магаданская область", new List<int>() { 49 } },
            { "Московская область", new List<int>() { 750, 50, 150, 90, 190 } },
            { "Мурманская область", new List<int>() { 51 } },
            { "Нижегородская область", new List<int>() { 152, 52 } },
            { "Новгородская область", new List<int>() { 53 } },
            { "Новосибирская область", new List<int>() { 154, 54 } },
            { "Омская область", new List<int>() { 55 } },
            { "Оренбургская область", new List<int>() { 56 } },
            { "Орловская область", new List<int>() { 57 } },
            { "Пензенская область", new List<int>() { 58 } },
            { "Псковская область", new List<int>() { 60 } },
            { "Ростовская область", new List<int>() { 161, 61 } },
            { "Рязанская область", new List<int>() { 62 } },
            { "Самарская область", new List<int>() { 163, 63 } },
            { "Саратовская область", new List<int>() { 64, 164 } },
            { "Сахалинская область", new List<int>() { 65 } },
            { "Свердловская область", new List<int>() { 96, 66, 196 } },
            { "Смоленская область", new List<int>() { 67 } },
            { "Тамбовская область", new List<int>() { 68 } },
            { "Тверская область", new List<int>() { 69 } },
            { "Томская область", new List<int>() { 70 } },
            { "Тульская область", new List<int>() { 71 } },
            { "Тюменская область", new List<int>() { 72 } },
            { "Ульяновская область", new List<int>() { 73, 173 } },
            { "Челябинская область", new List<int>() { 74, 174 } },
            { "Ярославская область", new List<int>() { 76 } },
            { "Москва", new List<int>() { 97, 99, 197, 199, 777, 77, 177 } },
            { "Санкт-Петербург", new List<int>() { 178, 98, 78 } },
            { "Севастополь", new List<int>() { 92 } },
            { "Еврейская автономная область", new List<int>() { 79 } },
            { "Ненецкий автономный округ", new List<int>() { 83 } },
            { "Ханты-Мансийский автономный округ -Югра", new List<int>() { 186, 86 } },
            { "Чукотский автономный округ", new List<int>() { 87 } },
            { "Ямало-Ненецкий автономный округ", new List<int>() { 89 } },
            { "Байконур(Казахстан)", new List<int>() { 94 } },
        };
        private static readonly int _REG_NUMBER_LEN = 3;

        /// <summary>
        /// данный метод проверяет переданный номерной знак в формате a999aa999  (латинскими буквами) и возвращает true или false 
        /// в зависимости от правильности номерного знака. Метод должен учитывать также и существующие номера регионов. 
        /// </summary>
        /// <param name="mark">номерной знак в формате a999aa999  (латинскими буквами)</param>
        /// <returns>true или false в зависимости от правильности номерного знака</returns>
        public static bool CheckMark(string mark)
        {
            if (Funcs.All(GetSeries(mark).Select(x => _SERIES_ALPHABET.Contains(x))) == false)
            {
                return false;
            }
            var regionName = Funcs.C((string x) => GetRegionCode(x)).Then(x => int.Parse(x)).Then(GetRegion);
            if (regionName.Call(mark) == null)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// данный метод принимает номерной знак в формате a999aa999 (латинскими буквами) и выдает следующий номер в данной серии или создает следующую серию
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        public static string GetNextMarkAfter(string mark)
        {
            var regNumber = ParseMark(mark);
            regNumber++;
            return regNumber.ToString();
        }

        /// <summary>
        /// данный метод принимает номерной знак в формате a999aa999 (латинскими буквами) 
        /// и выдает следующий номер в данной данном промежутке номеров rangeStart до rangeEnd (включая обе границы). 
        /// Если нет возможности выдать следующий номер, необходимо вернуть сообщение “out of stock”.
        /// </summary>
        /// <param name="prevMark"></param>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <returns></returns>
        public static string GetNextMarkAfterInRange(string prevMark, string rangeStart, string rangeEnd)
        {
            var found = false;
            var prevRegNumber = ParseMark(prevMark);
            foreach (var mark in RegNumber.Range(ParseMark(rangeStart), ParseMark(rangeEnd))) {
                if (found)
                {
                    return mark.ToString();
                }
                if (prevRegNumber == mark)
                {
                    found = true;
                }
            }
            return "out of stock";
        }

        /// <summary>
        /// данный метод принимает два номера в формате a999aa999 (латинскими буквами) и возвращает количество возможных номеров между ними (включая обе границы). 
        /// Метод необходим, чтобы рассчитать оставшиеся свободные номера для региона.
        /// </summary>
        /// <param name="mark1"></param>
        /// <param name="mark2"></param>
        /// <returns></returns>
        public static int GetCombinationsCountInRange(string mark1, string mark2)
        {
            var result = 0;

            foreach (var _ in RegNumber.Range(ParseMark(mark1), ParseMark(mark2)))
            {
                result++;
            }

            return result;
        }

        public static string TranslateMark(string mark)
        {
            if (Funcs.All(mark.Where(x => !char.IsDigit(x)).Select(x => _ALPHABET_TRANSLATE_MAP.ContainsKey(x))) == false)
            {
                throw new ArgumentException();
            }
            return string.Join("", mark.Select(x =>
            {
                if (char.IsDigit(x))
                {
                    return x.ToString();
                }
                else
                {
                    return _ALPHABET_TRANSLATE_MAP[x].ToString();
                }
            }));
        }

        private static RegNumber ParseMark(string mark)
        {
            var series = new RegNumberSeries(GetSeries(mark));
            var number = new RegNumberNumber(int.Parse(GetNumber(mark)));
            var regionCode = int.Parse(GetRegionCode(mark));

            return new RegNumber(series, number, regionCode);
        }

        private static string GetSeries(string mark)
        {
            return string.Join("", mark.Where(x => !char.IsDigit(x)).Select(x => x.ToString()));
        }

        private static string GetNumber(string mark)
        {
            return string.Join("", mark.Where(x => char.IsDigit(x)).Select(x => x.ToString()).Take(_REG_NUMBER_LEN));
        }

        private static string GetRegionCode(string mark)
        {
            return string.Join("", mark.Where(x => char.IsDigit(x)).Select(x => x.ToString()).Skip(_REG_NUMBER_LEN).Take(mark.Length));
        }

        private static string GetRegion(int code)
        {
            return _REGIONS.Where(x => x.Value.Contains(code)).Select(x => x.Key).FirstOrDefault();
        }
    }
}
