using System;
using System.Collections.Generic;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[6];
            A[0] = 20;
            A[1] = 10;
            A[2] = 30;
            A[3] = 30;
            A[4] = 40;
            A[5] = 10;

            Console.WriteLine(solution(A));
        }
        static public int solution(int[] A)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            int count, maxCount = 0, result = 0;

            foreach (int i in A)
            {
                if(map.TryGetValue(i, out count))
                {
                    count++;
                    map[i] = count;
                }
                else
                {
                    count = 1;
                    map.Add(i, count);
                }

                if(count > maxCount)
                {
                    result = i;
                    maxCount = count;
                }
            }
            return result;
        }
    }
}
