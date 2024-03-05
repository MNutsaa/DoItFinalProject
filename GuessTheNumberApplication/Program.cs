namespace GuessTheNumberApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select difficulty : 1 - Easy ; 2 - Medium ; 3 - Hard ");

            int count = 10;
            var randomNumber = 0;
            var n = 1;

            while (n == 1)
            {
                char difficulty = Convert.ToChar(Console.ReadLine());
                if (difficulty == '1')
                {
                    randomNumber = Random.Shared.Next(1, 25);
                    n = 0;
                }
                else if (difficulty == '2')
                {
                    randomNumber = Random.Shared.Next(1, 50);
                    n = 0;
                }
                else if (difficulty == '3')
                {
                    randomNumber = Random.Shared.Next(1, 100);
                    n = 0;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    n = 1;
                }
            }

            int check = 0;
            do
            {
                try
                {
                    while (count > 0)
                    {
                        Console.WriteLine($"You have only {count} attemps to guess the number .");
                        Console.WriteLine("Please enter the number : ");

                        int guess = int.Parse(Console.ReadLine());

                        if (guess == randomNumber)
                        {
                            Console.WriteLine("You win !");
                            break;
                        }
                        else
                        {
                            if (guess < randomNumber)
                            {
                                Console.WriteLine("Number must be higher.");
                            }
                            else
                            {
                                Console.WriteLine("Number must be lower.");
                            }
                        }

                        count--;

                        if (count == 0)
                        {
                            Console.WriteLine("Game Over.");
                        }
                    }
                }
                catch (Exception e)
                {
                    check = 1;
                    count--;
                    Console.WriteLine(e.Message);
                }

            } while (check == 1);


        }
    }
}