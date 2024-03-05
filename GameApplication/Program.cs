namespace GameApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string>
            {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
            };

            int randomIndex = Random.Shared.Next(0, words.Count);
            string randWord = words[randomIndex];
            int lenghtCount = randWord.Length;
            int tryCount = 6;
            char[] guessedCharacters = new char[lenghtCount];
            Console.WriteLine("You have only 6 attemps .");
            Console.WriteLine($"Word has {lenghtCount} characters.");
            string formatChecker = "No";


            for (int i = 0; i < lenghtCount; i++)
            {
                guessedCharacters[i] = '-';
            }

            while (formatChecker.Equals("No"))
            {
                formatChecker = "Yes";
                try

                {
                    while (tryCount > 0 && string.Join("", guessedCharacters) != randWord)
                    {
                        Console.WriteLine("Please enter character : ");
                        char letter = Convert.ToChar(Console.ReadLine());
                        bool check = false;

                        for (int i = 0; i < lenghtCount; i++)
                        {
                            if (randWord[i] == letter)
                            {
                                guessedCharacters[i] = letter;
                                check = true;
                            }
                        }
                        if (check == false)
                        {
                            Console.WriteLine("This word doesn't include indicated character ,try again.");

                        }
                        else
                        {
                            Console.WriteLine("Your guess is correct .");
                            Console.WriteLine(string.Join("", guessedCharacters));
                        }

                        tryCount--;
                        if (tryCount > 0 && string.Join("", guessedCharacters) != randWord)
                        {
                            Console.WriteLine($"You have {tryCount} attamps .");
                        }
                        else if (string.Join("", guessedCharacters) == randWord)
                        {
                            Console.WriteLine($"You win .The word was {randWord}");
                        }
                        else
                        {
                            Console.WriteLine("You have no more attamp");
                        }
                    }
                    if (string.Join("", guessedCharacters) != randWord)
                    {
                        Console.WriteLine("Please write the whole word.");
                        string finalGuess = Console.ReadLine();

                        if (finalGuess == randWord)
                        {
                            Console.WriteLine(finalGuess);
                            Console.WriteLine("You Win. ");
                        }
                        else
                        {
                            Console.WriteLine("Game Over.");
                        }
                    }
                }
                catch (Exception e)
                {
                    formatChecker = "No";
                    Console.WriteLine(e.Message);
                }
            }

        }
    }
}