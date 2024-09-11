using System;

namespace GuessNumberGame
{
    // Huvudklassen för spelet
    public class GuessNumberGame
    {
        private bool runGame;
        private readonly Score gameScore;
        private readonly HandleHighScore saveScoreList;
        private readonly Random random;

        // Konstruktorn som initierar de nödvändiga instanserna för spelet
        public GuessNumberGame()
        {
            gameScore = new Score();
            saveScoreList = new HandleHighScore();
            random = new Random();
        }

        // Startar spelet och kör det i en loop beroende på användarens val
        public void PlayGame(bool playGame)
        {
            runGame = playGame;
            while (runGame)
            {
                StartNewGame();

                Console.WriteLine("Vill du spela igen? (ja/nej)");
                runGame = WillPlay();
            }
        }

        // Startar en ny omgång, låter spelaren gissa tills rätt svar ges
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

        // Bearbetar spelarens gissning och ger feedback om den är rätt, för hög eller för låg
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

        // Sparar spelarens resultat (namn och poäng) i highscore-listan (highscore.txt)
        private void SaveScore(int guessCount)
        {
            string playerName = GetValidPlayerName();
            gameScore.PlayerName = playerName;
            gameScore.Guesses = guessCount;

            saveScoreList.SaveHighScore(gameScore);
        }

        // Matar in en giltig gissning från spelaren, dvs. ett tal mellan 1 och 100
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

        // Hämtar ett giltigt spelarnamn som inte är tomt och max. 20 tecken långt
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

        // Frågar spelaren om de vill fortsätta spela och validerar input
        private bool WillPlay()
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
