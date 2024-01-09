using System;

namespace TicTacToe
{
    class Program
    {
        static char[,] board;
        static bool isGameComplete;
        static char activePlayer;

        static void Main(string[] args)
        {
            // Starts the game
            StartGame();

            // While loop checks if game is finished or not.
            while (!isGameComplete)
            {
                // if not finished
                DisplayBoard();
                PlayerTurn();
                TakeComputerTurn();
                WhoWon();
                TogglePlayer();
            }

            PlayAgain();
        }

        static void StartGame()
        {
            // assigning values to the array for the board
            board = new char[3, 3] { { '_', '_', '_' }, { '_', '_', '_' }, { '_', '_', '_' } };
            // assigning false value to start the game
            isGameComplete = false;
            // 1st player playing would be X
            activePlayer = 'X';

            // Welcome Test. PS: 'Sun Chokdi' means TicTacToe. Read it as "Soon Cho-kuh-di"
            Console.WriteLine("Welcome to Meet Sheth's Sun Chokdi");
            
        }

        static void DisplayBoard()
        {
            // Creates the top line of the board
            Console.WriteLine("    0    1    2");
            Console.WriteLine("---------------");
            for (int row = 0; row < 3; row++)
            {
                Console.Write(row + " | ");
                for (int col = 0; col < 3; col++)
                {
                    // Displays the array value and the 
                    Console.Write(board[row, col] + " | ");
                }
                Console.WriteLine();
                Console.WriteLine("---------------");
            }
        }

        static void PlayerTurn()
        {
            if (activePlayer == 'X') 
            { 
            
            // Takes input for the Game
            Console.WriteLine("Its Your turn. Think Carefully, you must win against the AI.  \nEnter the Row number for 'X' (0-2):");
            int row = GetNumberInput();
            Console.WriteLine("Enter the Column number for 'X' (0-2):");
            int col = GetNumberInput();

                // checks with EmptySpace method if the space it vacant for the entry 
                if (EmptySpace(row, col))
                {
                    board[row, col] = activePlayer;
                }
                // If not, prompts it as invalid input
                else
                {
                    Console.WriteLine("Invalid space! Can't overwrite. Please try again.");
                    
                    // Calls the same method again from within the Method
                    PlayerTurn();
                }
            }
        }

        static void TakeComputerTurn()
        {
            if (activePlayer == 'O')
            {
                Console.WriteLine("AI entered the 'O', Here you Go");

                // A Very Smart computer logic here. Shh..
                Random random = new Random();
                
                int row, col;

                // Do Loop until a Empty Space is found
                do
                {
                    row = random.Next(0, 3);
                    col = random.Next(0, 3);
                } while (!EmptySpace(row, col));

                board[row, col] = activePlayer;
            }
        }

        static int GetNumberInput()
        {
            int number;
            // Checks the number entered is a valid integer or not.
            if (int.TryParse(Console.ReadLine(), out number))
            {
                return number;
            }
            else
            {
                // If not, prompts it as invalid input
                Console.WriteLine("Thats not a correct number. You can only enter a Integer between 0-2");
                return GetNumberInput();
            }
        }

        static bool EmptySpace(int row, int col)
        {
            // if (board[row, col] == '_') - Could be another logic.
            // | the row limit   |   the column limit    |  check for X inputs    |   check for O inputs |
            if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] != 'X' && board[row, col] != 'O')
            {
                // confirms it as a empty space
                return true;
            }
            // Denies to enter input here, as there is already an input there.
            return false;
        }

        static void WhoWon()
        {
            // check if anyone won the game with 'WinningRules' Method
            if (WinningRules(activePlayer))
            {
                // finishes the game
                isGameComplete = true;
                // code explains itself
                if (activePlayer == 'X')
                {
                    Console.WriteLine("\n***************************************\n");
                    Console.WriteLine("\nHoorah! You have won! Congratulations! \n");
                    Console.WriteLine("\n***************************************\n");
                }
                else
                {
                    Console.WriteLine("\n\n\nCommon man! Its not Chess, Atleast, you can win a TicTacToe against the computer! ");
                    Console.WriteLine("\n\nWhat are you? 25 IQ ? \n\n\n\n");
                }
                
            }
            // checks if the board is full with 'AllComplete' Method
            else if (AllComplete())
            {
                // Finishes the game
                isGameComplete = true;
                Console.WriteLine("\n \n \nIt's a tie! Don't Cry\n \n \n");
            }
        }

        static bool WinningRules(char winner)
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == winner && board[row, 1] == winner && board[row, 2] == winner)
                {
                    return true;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == winner && board[1, col] == winner && board[2, col] == winner)
                {
                    return true;
                }
            }

            // Check diagonals
            if (board[0, 0] == winner && board[1, 1] == winner && board[2, 2] == winner)
            {
                return true;
            }

            if (board[0, 2] == winner && board[1, 1] == winner && board[2, 0] == winner)
            {
                return true;
            }

            return false;
        }

        static bool AllComplete()
        {
            // Checks each Row
            for (int row = 0; row < 3; row++)
            {
                // Checks each Column in the row
                for (int col = 0; col < 3; col++)
                {
                    // Checks if the value of the array is X or O
                    if (board[row, col] != 'X' && board[row, col] != 'O')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void TogglePlayer()
        {
            // Changes the turn of player
            if (activePlayer == 'X')
            {
                activePlayer = 'O';
            }
            else
            {
                activePlayer = 'X';
            }
        }

        static void PlayAgain()
        {
            // Displays the updated board
            DisplayBoard();
            Console.WriteLine("\nIts the End of the Game.\n \nEnter 'y' to play the Game Again ? y/n");
            string replay = Console.ReadLine();
            Console.WriteLine("\n");
            // Replays the game
            if (replay == "y") 
            {
                Console.WriteLine("\n");
                StartGame();
                while (!isGameComplete)
                {
                    DisplayBoard();
                    PlayerTurn();
                    TakeComputerTurn();
                    WhoWon();
                    TogglePlayer();
                }

                PlayAgain();

            } 
            // Thanks and closes
            else
            {
                Console.WriteLine("Thank you for playing.");
            }
        }
    }
}
