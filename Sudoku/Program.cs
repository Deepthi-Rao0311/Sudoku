using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] input = {
                { 0, 2, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 6, 0, 0, 0, 0, 3 },
                { 0, 7, 4, 0, 8, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 3, 0, 0, 2 },
                { 0, 8, 0, 0, 4, 0, 0, 1, 0 },
                { 6, 0, 0, 5, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 7, 8, 0 },
                { 5, 0, 0, 0, 0, 9, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 4, 0 },
            };
            Print(input);

            Solve(input);
            Console.ResetColor();
            Console.WriteLine("Press enter to exit !");
            Console.ReadLine();
        }

        private static void Solve(int[,] input)
        {
            int[,] output = (int[,])input.Clone();
            int i = 0;
            int j = 0;
            bool moveForward = true;            
            do
            {
                if (input[i, j] != 0) // current item in input value. cannot be changed. move forward.
                {
                    //moveForward = true;
                }
                else
                {
                    int val = GetValidValue(i, j, input, output);
                    if (val == 10)
                    {
                        output[i, j] = 0;
                        moveForward = false;
                    }
                    else
                    {
                        output[i, j] = val;
                        //Print(input, output, i, j);
                        moveForward = true;
                    }
                }

            } while (GetNextIndex(i, j, input, moveForward, out i, out j));
            Print(output);

        }

        //private static void Forward(int i, int j, int[,] input, int[,] output)
        //{
            
        //    if (i==9 && j ==9)
        //    {
        //        Console.WriteLine("Program finished !");
        //        return;
        //    }
        //    if(j == 9)
        //    {
        //        i = i + 1;
        //        j = 0;
        //    }
        //    if (input[i, j] != 0) // current item in input value. cannot be changed. move forward.
        //    {
        //        Forward(i, j+1, input, output);
        //    }
        //    int val = GetValidValue(i, j, input, output);
        //    if (val == 10)
        //    {
        //        output[i, j] = 0;
        //        Backward(i, j, input, output);
        //    }
        //    else
        //    {
        //        output[i, j] = val;
        //        Print(input,output,i,j);
        //        Forward(i,j+1, input, output);
        //    }
        //}

        private static int GetValidValue(int i, int j, int[,] input, int[,] output)
        {
            int val = output[i, j] +1 ;
            for (val = output[i, j] +1; val < 10; val++)
            {
                if (IsValid(output, i, j, val))
                    break;
            }
            return val;
        }

        //private static void Backward(int i, int j, int[,] input, int[,] output)
        //{
        //    if (i == 0 && j == 0)
        //    {
        //        Console.WriteLine("End of array ! Cannot go further back !");
        //        return;
        //    }
        //    j = j - 1;
        //    if(j == -1)
        //    {
        //        i=i-1;
        //        j = 8;
        //    }
        //    if(input[i, j] != 0) // if this item is part of input then keep going backward.
        //        Backward(i, j, input, output);
        //    Forward(i,j, input, output);
        //}

        private static bool IsValid(int[,] output, int i, int j, int val)
        {
            //1. check if value is in range of 1 to 9
            if (val < 1 || val > 9)
                return false;
            //2. check if value is repetate in the row
            for (int col = 0; col < 9; col++)
            {
                if (col != j && output[i, col] == val)
                    return false;
            }
            //3. check if value is repetate in the column
            for (int row = 0; row < 9; row++)
            {
                if (row != i && output[row, j] == val)
                    return false;
            }
            //4. check if value is repetate in same inner 3x3 sq
            int innersqStartingRow = i -(i %3);
            int innersqStartingColumn = j - (j % 3);
            for (int row = innersqStartingRow; row < innersqStartingRow + 3; row++)
            {
                for (int col = innersqStartingColumn; col< innersqStartingColumn + 3; col++)
                {
                    if( row != i && col != j && output[row, col] == val )
                        return false;
                }
            }

            return true;
        }

        private static bool GetNextIndex(int i, int j, int[,] input, bool forward, out int row, out int col)
        {
            row = i;
            col = j;
            if (forward)
            {
                col = j + 1;
                if (col == 9)
                {
                    row = i + 1;
                    col = 0;
                }
                if (row == 9)
                    return false;
                return true;
            }
            else
            {
                col = j - 1;
                if(col == -1)
                {
                    row= i - 1;
                    col = 8;
                }
                if (row == -1)
                    return false;
                return true;
            }
        }
        private static void Print(int[,] input)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(input[i, j] != 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(input[i,j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static void Print(int[,] input, int[,] output, int row, int col)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (input[i, j] != 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if(i == row && j == col)
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(output[i, j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
