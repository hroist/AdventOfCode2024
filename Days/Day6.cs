using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Days
{
    internal class Day6: IDay

    {
        public void Run()
        {
            string filePath = Path.Combine("Inputs", "Day6.txt");

            var lines = File.ReadAllLines(filePath);

            var mapList = new List<char[]>();

            foreach (var line in lines)
            {
                char[] row = line.ToCharArray();
                mapList.Add(row);
            }

            char[][] map = mapList.ToArray();

            char[] directions = new[] { '^', '>', 'v', '<' };

            var startingPosition = getCurrentPosition(map, directions);

            static (int, int) getCurrentPosition(char[][] map, char[] directions)
            {
                var position = (0,0);

                for (int row = 0; row < map.Length; row++)
                {
                    for(int col = 0; col < map[row].Length; col++)
                    {
                        if(directions.Contains(map[row][col]))
                            position = (row, col);

                    }
                }
                return position;
            }

            char getCurrentDirection(char[][] map, char[] directions)
            {
                var position = getCurrentPosition(map, directions);
                char currentDirection = map[position.Item1][position.Item2];
                return currentDirection;
            }

            char getNextCellInCurrentDirection(char[][] map, char currentDirection, (int , int) currentPosition) 
            {
                (int x, int y) nextCellPosition = getNextCellCoordinates(currentDirection);
                char nextCellValue = map[currentPosition.Item1 + nextCellPosition.x][currentPosition.Item2 + nextCellPosition.y];
                return nextCellValue;

            }

            (int, int) getNextCellCoordinates(char currentDirection)
            {
                List<(char direction, (int x, int y))> directions = new()
                {
                    ('^', (0, -1) ),
                    ('>', (1, 0)  ),
                    ('v', (0, 1)  ),
                    ('<', (-1, 0) )
                };
                (int x, int y) nextCellPosition = directions.First(d => d.direction == currentDirection).Item2;
                return nextCellPosition;
            }

            void PlayGame(char[][] map, char[] directions)
            {
                var currentPosition = getCurrentPosition(map, directions);
                int maxRowIndex = map.Length;
                int maxColumnIndex = map[0].Length;

                while (true)
                {
                    var currentDirection = getCurrentDirection(map, directions);

                    (int dx, int dy) = getNextCellCoordinates(currentDirection);
                    int nextRow = currentPosition.Item1 + dy; 
                    int nextCol = currentPosition.Item2 + dx; 

                    if (nextRow < 0 || nextRow >= maxRowIndex || nextCol < 0 || nextCol >= maxColumnIndex)
                    {
                        Console.WriteLine("Next position is out of bounds. Stopping.");
                        break;
                    }

                    char nextCell = map[nextRow][nextCol];

                    if (nextCell == '.' || nextCell == 'X')
                    {
                        map[currentPosition.Item1][currentPosition.Item2] = 'X';
                        map[nextRow][nextCol] = currentDirection;

                        currentPosition = (nextRow, nextCol);
                    }
                    else if (nextCell == '#')
                    {
                        int currentIndex = Array.IndexOf(directions, currentDirection);
                        int nextIndex = (currentIndex + 1) % directions.Length;
                        currentDirection = directions[nextIndex];

                        map[currentPosition.Item1][currentPosition.Item2] = currentDirection;
                    }
                    else
                    {
                        Console.WriteLine("Unexpected cell encountered. Stopping.");
                        break;
                    }
                }
            }


            PlayGame(map, directions);
            var countDistinctPositions = map.Sum(row => row.Count(cell => cell == 'X'));
            Console.WriteLine("Distinct positions: {0}", countDistinctPositions + 1);
        }
    }
}
