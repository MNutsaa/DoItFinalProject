namespace CalculatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char n = default;
            do
            {
                try
                {
                    Console.WriteLine("Please enter two number and choose operation");
                    Console.WriteLine("First Number :");
                    int x = int.Parse(Console.ReadLine());
                    Console.WriteLine("Second Number :");
                    int y = int.Parse(Console.ReadLine());
                    Console.WriteLine("1 - Sum ; 2 - Subtract ; 3 - Multiplay ; 4 - Devide ;");
                    char operation = Convert.ToChar(Console.ReadLine());
                    int result = default;


                    switch (operation)
                    {
                        case '1':
                            {
                                result = x + y;
                                Console.WriteLine($"Result : {result}");
                                break;
                            }
                        case '2':
                            {
                                result = x - y;
                                Console.WriteLine($"Result : {result}");
                                break;
                            }

                        case '3':
                            {
                                result = x * y;
                                Console.WriteLine($"Result : {result}");
                                break;
                            }
                        case '4':
                            {
                                result = x / y;
                                Console.WriteLine($"Result : {result}");
                                break;

                            }
                    }
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Number can not be devided by zero");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input .Please try again. ");
                }

                Console.WriteLine("Do you want to continue ? (Y/N)");
                n = Convert.ToChar(Console.ReadLine().ToUpper());

            } while (n.Equals('Y'));
        }
    }
}