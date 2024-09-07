using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Random random = new Random();
        List<int> guessesList = new List<int>();

        // Fråga användaren i början om den vill spela
        string? startResponse = "";

        while (true)
        {
            Console.WriteLine("Vill du spela ett spel? (ja/nej)");
            startResponse = Console.ReadLine()?.Trim().ToLower();

            if (startResponse == "ja")
            {
                break; // Fortsätt till spelet om användaren vill spela
            }
            else if (startResponse == "nej")
            {
                Console.WriteLine("Spelet avslutas.");
                return; // Avslutar programmet om användaren inte vill spela
            }
            else
            {
                Console.WriteLine("Ogiltigt svar. Svara med 'ja' eller 'nej'.");
            }
        }

        bool playAgain = true;

        // Huvudloop för spelet, som körs så länge användaren vill spela igen
        while (playAgain)
        {
            int numberToGuess = random.Next(1, 101);
            int numberOfGuesses = 0;
            bool guessedCorrectly = false;

            Console.WriteLine("Gissa ett nummer mellan 1 och 100:");

            while (!guessedCorrectly)
            {
                numberOfGuesses++;
                if (int.TryParse(Console.ReadLine(), out int guess))
                {
                    if (guess < 1 || guess > 100)
                    {
                        Console.WriteLine("Numret måste vara mellan 1 och 100. Gissa igen:");
                    }
                    else if (guess < numberToGuess)
                    {
                        Console.WriteLine("För lågt! Gissa igen:");
                    }
                    else if (guess > numberToGuess)
                    {
                        Console.WriteLine("För högt! Gissa igen:");
                    }
                    else
                    {
                        guessedCorrectly = true;
                        Console.WriteLine($"Rätt gissat! Det tog {numberOfGuesses} gissningar.");
                        guessesList.Add(numberOfGuesses);
                    }
                }
                else
                {
                    Console.WriteLine("Vänligen ange ett giltigt nummer.");
                }
            }

            Console.WriteLine("Vill du spela igen? (ja/nej)");
            playAgain = Console.ReadLine()?.Trim().ToLower() == "ja";
        }

        Console.WriteLine("Tack för att du spelade! Här är antalet gissningar för varje spel:");
        guessesList.ForEach(guesses => Console.WriteLine(guesses));

    }
}