using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days
{
    internal class Day4: IDay
    {
        public void Run()
        {
            string filePath = Path.Combine("Inputs", "Day4.txt");

            var lines = File.ReadAllLines(filePath);

            var boardList = new List<char[]>();

            foreach (var line in lines)
            {
                char[] row = line.ToCharArray();
                boardList.Add(row);
            }

            char[][] board = boardList.ToArray();

            static bool SearchInAllDirections(char[][] board, int row, int col, string word)
            {
                int m = board.Length;
                int n = board[0].Length;

                if (board[row][col] != word[0])
                {
                    return false;
                }

                int[] x = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] y = { -1, 0, 1, -1, 1, -1, 0, 1 };

                int wordLength = word.Length;
                int wordCounter = 0;


                for (int dir = 0; dir < 8; dir++)
                {
                    int k, currX = row + x[dir], currY = col + y[dir];

                    for(k = 1; k < wordLength; k++)
                    {
                        if (currX >= m || currX < 0 || currY >= n || currY < 0)
                            break;

                        if (board[currX][currY] != word[k])
                            break;

                        currX += x[dir];
                        currY += y[dir];
                    }

                    if (k == wordLength)
                    {
                        wordCounter++;
                        Console.WriteLine("Word counter {0} ", wordCounter);
                    }
                    
                }
                if (wordCounter > 0)
                {
                    return true;
                }
                return false;
            }

            static List<int[]> SearchWords(char[][] board, string word)
            {
                int m = board.Length;
                int n = board[0].Length;

                int numberOfWordsFound = 0;
                List<int[]> ans = new List<int[]>();

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (SearchInAllDirections( board, i , j , word ))
                        {
                            ans.Add(new int[] { i, j });
                            numberOfWordsFound++;
                        }
                    }
                }

                Console.WriteLine("Word: {0}, Number of Words Found: {1}", word, numberOfWordsFound);
                return ans;
            }

            static void printResult(List<int[]> ans)
            {
                foreach (var coord in ans)
                {
                    Console.Write("{" + coord[0] + "," + coord[1] + "}" + " ");
                }
                Console.WriteLine();
            }

            //int answer = SearchWords(board, "XMAS") + SearchWords(board, "SAMX");
            List<int[]> answer = SearchWords(board, "XMAS");
            printResult(answer);

        }
    }
}
