using System;
using System.Collections.Generic;
using System.Linq;

namespace SB_UTILS
{
    public class NFoldConverter
    {
        public static List<T> FromDec<T>(int number, List<T> alphabet)
        {
            var base_ = alphabet.Count;
            var stack = new Stack<int>();

            if (number == 0)
            {
                stack.Push(0);
            }
            else
            {
                while (number > 0)
                {
                    stack.Push(number % base_);
                    number /= base_;
                }
            }
            var result = new List<T>();
            while (stack.Count > 0)
            {
                result.Add(alphabet[stack.Pop()]);
            }
            return result;
        }

        public static int ToDec<T>(List<T> number, List<T> alphabet)
        {
            var base_ = alphabet.Count;
            var result = 0;

            var rNumber = number.ToList();
            rNumber.Reverse();

            for (int i = 0; i < rNumber.Count; i++)
            {
                result += alphabet.IndexOf(rNumber[i]) * (int) Math.Pow(base_, i);
            }
            return result;

        }
    }
}
