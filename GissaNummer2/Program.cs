using System;

namespace GuessNumberGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skapar användargränssnittet för spelet och visar highscore, samt frågar om användaren vill spela
            UI userInterface = new UI();
            bool playGame = userInterface.DrawUI();

            // Startar spelet om användaren valde att spela
            GuessNumberGame game = new GuessNumberGame();
            game.PlayGame(playGame);
        }
    }
}