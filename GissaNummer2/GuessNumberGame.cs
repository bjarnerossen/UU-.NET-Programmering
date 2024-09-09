using System;

namespace GuessNumberGame
{
    public class GuessNumberGame
    {
        private bool runGame;
        private readonly Score gameScore;
        private readonly HandleHighScore saveScoreList;
        private readonly Random random;

        public GuessNumberGame()
        {
            gameScore = new Score();
            saveScoreList = new HandleHighScore();
            random = new Random();
        }

        public void PlayGame(bool playGame)
        {
            runGame = playGame;
            while (runGame)
            {
                StartNewGame();

                Console.WriteLine("Vill du spela igen? (ja/nej)");
                runGame = WillPlayAgain();
            }
        }

        private void StartNewGame()
        {
            int targetNumber = random.Next(1, 101);
            int guessCount = 0;
            bool correct = false;

            Console.WriteLine("Gissa ett nummer mellan 1 och 100!");

            while (!correct)
            {
                int guess = GetValidGuess();
                guessCount++;

                correct = ProcessGuess(guess, targetNumber, guessCount);
            }

            SaveScore(guessCount);
        }

        private bool ProcessGuess(int guess, int targetNumber, int guessCount)
        {
            if (guess == targetNumber)
            {
                Console.WriteLine($"Rätt! Du gissade rätt på {guessCount} försök.");
                return true;
            }
            else if (guess > targetNumber)
            {
                Console.WriteLine("För högt!");
            }
            else
            {
                Console.WriteLine("För lågt!");
            }
            return false;
        }

        private void SaveScore(int guessCount)
        {
            string playerName = GetValidPlayerName();
            gameScore.PlayerName = playerName;
            gameScore.Guesses = guessCount;

            saveScoreList.SaveHighScore(gameScore);
        }

        private int GetValidGuess()
        {
            int guess;
            while (true)
            {
                Console.Write("Din gissning: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out guess) && guess >= 1 && guess <= 100)
                {
                    return guess;
                }
                else
                {
                    Console.WriteLine("Ogiltigt värde! Ange ett nummer mellan 1 och 100.");
                }
            }
        }

        private string GetValidPlayerName()
        {
            const int maxCharacters = 20;
            string playerName;

            do
            {
                Console.Write("Vad heter du? (max 20 tecken): ");
                playerName = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("Namnet får inte vara tomt. Försök igen.");
                }
                else if (playerName.Length > maxCharacters)
                {
                    Console.WriteLine($"Namnet får inte vara längre än {maxCharacters} tecken. Försök igen.");
                }
            }
            while (string.IsNullOrWhiteSpace(playerName) || playerName.Length > maxCharacters);

            return playerName;
        }

        private bool WillPlayAgain()
        {
            while (true)
            {
                string input = Console.ReadLine().ToLower();

                if (input == "ja")
                {
                    return true;
                }
                else if (input == "nej")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Ogiltigt svar! Skriv 'ja' eller 'nej'.");
                }
            }
        }
    }
}
