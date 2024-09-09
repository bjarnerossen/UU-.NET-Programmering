using System;
using System.Collections.Generic;

namespace GuessNumberGame
{
    public class UI
    {
        private HandleHighScore scoreList;
        private List<Score> scores;

        public UI()
        {
            scoreList = new HandleHighScore();
            scores = scoreList.FetchHighScore();
        }

        public bool DrawUI()
        {
            Console.WriteLine("Välkommen till Gissa Numret!");
            if (scores.Count == 0)
            {
                Console.WriteLine("Ingen har spelat tidigare. Du kan bli den första!");
            }
            else
            {
                Console.WriteLine("Highscore:");
                foreach (var score in scores)
                {
                    Console.WriteLine($"{score.PlayerName} - {score.Guesses} gissningar");
                }
            }
            return PromptToPlay();
        }

        private bool PromptToPlay()
        {
            while (true)
            {
                Console.WriteLine("Vill du spela? (ja/nej)");
                string input = Console.ReadLine()?.Trim().ToLower();

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
                    Console.WriteLine("Ogiltigt val! Skriv 'ja' för att spela eller 'nej' för att avsluta.");
                }
            }
        }
    }
}
