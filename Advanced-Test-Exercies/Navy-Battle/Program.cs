using System;

namespace NavyBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sizeOfMatrix = int.Parse(Console.ReadLine());
            char[,] battleField = new char[sizeOfMatrix, sizeOfMatrix];
            int startingRow = 0;
            int startingCol = 0;
            for (int row = 0; row < sizeOfMatrix; row++)
            {
                string value = Console.ReadLine();
                char[] coordinates = value.ToCharArray();
                for (int col = 0; col < sizeOfMatrix; col++)
                {
                    battleField[row, col] = coordinates[col];
                    if (coordinates[col] == 'S')
                    {
                        startingRow = row;
                        startingCol = col;
                    }
                }
            }
            int damage = 0;
            int totalShips = 3;
            while (true)
            {
                string command = Console.ReadLine();
                int oldRow = startingRow;
                int oldCol = startingCol;
                if (command == "left")
                {
                    startingCol--;
                }
                else if (command == "right")
                {
                    startingCol++;
                }
                else if (command == "up")
                {
                    startingRow--;
                }
                else if (command == "down")
                {
                    startingRow++;
                }
                if (battleField[startingRow, startingCol] == '*')
                {
                    if (damage < 2)
                    {                       
                        damage++;
                    }
                    else
                    {
                        battleField[oldRow, oldCol] = '-';
                        battleField[startingRow, startingCol] = 'S';
                        Console.WriteLine($"Mission failed, U-9 disappeared! Last known coordinates [{startingRow}, {startingCol}]!");
                        break;
                    }

                }
                if (battleField[startingRow, startingCol] == 'C')
                {
                    totalShips--;
                    battleField[startingRow, startingCol] = 'S';
                    if (totalShips == 0)
                    {
                        battleField[oldRow, oldCol] = '-';
                        Console.WriteLine("Mission accomplished, U-9 has destroyed all battle cruisers of the enemy!");
                        break;
                    }
                }
                battleField[oldRow, oldCol] = '-';
            }
            for (int row = 0; row < sizeOfMatrix; row++)
            {
                for (int col = 0; col < sizeOfMatrix; col++)
                {
                    Console.Write(battleField[row,col]);
                }
                Console.WriteLine();
            }
        }
    }
}
