using System;

namespace LudumDare50.Client.Extensions
{
    public static class ArrayExtensions 
    {
        public static T[] Shuffle<T>(this T[] array)
        {
            var rng = new Random();
            var n = array.Length;
            while (n > 1)
            {
                var k = rng.Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
            return array;
        }
    }
}