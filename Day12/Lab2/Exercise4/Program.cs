using System;
using System.Collections.Generic;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 5, 6, 7 }, { 9, 8, 7 } };
            //Console.WriteLine(matrix.GetLength(1));
            Solution(matrix);
        }

        static List<int> Solution(int[,] matrix)
        {
            List<int> ans = new List<int>();

            //List<List<int>> temp = new List<List<int>>();
            //temp.Add(new List<int> { 1, 2 });
            //ans.AddRange(temp[0]);

            if (matrix.Length == 0) return ans;

            int R = matrix.Length, C = matrix.GetLength(1);
            //bool[][] seen = new bool[R][C];
            bool[,] seen = new bool[R, C];
            int[] dr = { 0, 1, 0, -1 };
            int[] dc = { 1, 0, -1, 0 };
            int r = 0, c = 0, di = 0;

            for (int i = 0; i < R * C; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ans.Add(matrix[r, j]);
                    seen[r, j] = true;
                }
                int cr = r + dr[di];
                int cc = c + dc[di];

                if (0 <= cr && cr < R && 0 <= cc && cc < C && !seen[cr, cc])
                {
                    r = cc;
                    c = cc;
                }
                else
                {
                    di = (di + 1) % 4;
                    r += dr[di];
                    c += dc[di];
                }
            }

            return ans;
        }
    }
}
