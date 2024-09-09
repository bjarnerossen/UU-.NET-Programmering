namespace GuessNumberGame
{
    public class Score : IComparable<Score>
    {
        public string PlayerName { get; set; } = string.Empty;
        public int Guesses { get; set; }

        public int CompareTo(Score? other)
        {
            if (other == null)
            {
                return 1;
            }
            return Guesses.CompareTo(other.Guesses);
        }
    }
}
