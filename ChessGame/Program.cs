using System;


namespace ChessGame
{
    internal class Program
    {
        public class ChessGame
        {
            private char[,] chessboard;
            private int currentRow;
            private int currentColumn;

            public ChessGame()
            {
                InitializeChessboard();
                currentRow = 0;
                currentColumn = 0;
            }

            private void InitializeChessboard()
            {
                chessboard = new char[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        chessboard[i, j] = '*';
                    }
                }
                chessboard[0, 0] = 'X';
            }

            private void PrintChessboard()
            {
                Console.WriteLine("   a b c d e f g h");
                for (int i = 0; i < 8; i++)
                {
                    Console.Write($"{8 - i} |");
                    for (int j = 0; j < 8; j++)
                    {
                        Console.Write($"{chessboard[i, j]}|");
                    }
                    Console.WriteLine();
                }
            }

            private bool IsValidPosition(int row, int column)
            {
                return (column >= 0 && column < 8 && row >= 0 && row < 8);
            }

            private void ShowPossibleKnightMoves(int row, int column)
            {
                // Clear previous marks before showing new moves
                ClearPreviousMarks();

                int[,] knightMoves =
            {
                { -2, -1 }, { -1, -2 }, { 1, -2 }, { 2, -1 },
                { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 }
            };

                Console.WriteLine("Possible knight moves:");
                for (int i = 0; i < knightMoves.GetLength(0); i++)
                {
                    int newRow = row + knightMoves[i, 0];
                    int newColumn = column + knightMoves[i, 1];

                    if (IsValidPosition(newRow, newColumn))
                    {
                        chessboard[newRow, newColumn] = 'K';
                    }
                }
            }

            private void ClearPreviousMarks()
            {
                // Reset all 'K' symbols back to '*'
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (chessboard[i, j] == 'K')
                        {
                            chessboard[i, j] = '*';
                        }
                    }
                }
            }

            public void StartGame()
            {
                Console.WriteLine("Starting new game...");
                PrintChessboard();

                while (true)
                {
                    Console.Write("Enter the position of 'X' (e.g., c4): ");
                    string input = Console.ReadLine();

                    if (!IsValidInput(input))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid position (e.g., c4)");
                        continue;
                    }

                    int column = input[0] - 'a';
                    int row = 8 - int.Parse(input[1].ToString());

                    if (!IsValidPosition(row, column))
                    {
                        Console.WriteLine("Invalid position. Please enter a valid position (e.g., c4)");
                        continue;
                    }

                    chessboard[currentRow, currentColumn] = '*'; // Clear the current position
                    currentRow = row;
                    currentColumn = column;

                    chessboard[currentRow, currentColumn] = 'X'; // Move 'X' to the new position

                    // Display possible knight moves for the new position
                    ShowPossibleKnightMoves(currentRow, currentColumn);
                    PrintChessboard();
                }
            }

            private bool IsValidInput(string input)
            {
                if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
                {
                    return false;
                }

                return true;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. New Game");

            string select = Console.ReadLine();

            switch (select)
            {
                case "1":
                    ChessGame game = new ChessGame();
                    game.StartGame();
                    break;
            }
        }
    }
}
