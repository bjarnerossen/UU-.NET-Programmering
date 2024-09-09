using System;
using System.Collections.Generic;
using System.IO;

namespace GuessNumberGame
{
    public class HandleHighScore
    {
        private const string filePath = "highscore.txt";

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

            scores.Sort();
            return scores;
        }
    }
}
