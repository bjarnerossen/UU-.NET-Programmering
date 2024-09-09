using System;

namespace GuessNumberGame
{
    class Program
    {
        static void Main(string[] args)
        {
            UI userInterface = new UI();
            bool playGame = userInterface.DrawUI();

            GuessNumberGame game = new GuessNumberGame();
            game.PlayGame(playGame);
        }
    }
}
