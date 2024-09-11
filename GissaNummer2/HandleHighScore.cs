using System;
using System.Collections.Generic;
using System.IO;

namespace GuessNumberGame
{
    // Hanterar att spara och hämta högsta poäng till/från en fil
    public class HandleHighScore
    {
        private const string filePath = "highscore.txt";

        // Sparar ett highscore till filen, returnerar true om det lyckas
        public bool SaveHighScore(Score score)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"{score.PlayerName},{score.Guesses}");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Hämtar highscore från filen och returnerar en lista med Score-objekt
        public List<Score> FetchHighScore()
        {
            List<Score> scores = new List<Score>();

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        scores.Add(new Score
                        {
                            PlayerName = data[0],
                            Guesses = int.Parse(data[1])
                        });
                    }
                }
            }

            // Sorterar listan (lägst först)
            scores.Sort();
            return scores;
        }
    }
}
