using System.Text.Json;

namespace ATMApplication
{
    internal class Program
    {
        static List<Clients> clientsList = new List<Clients>();
        private const string _transactionsFileLocation = @"../../../historyLogger.json";
        private const string _clientsFileLocation = @"../../../Clients.json";

        static void Main(string[] args)
        {
            LoadClientsFromJson();

            char answer = 'Y';

            do
            {
                try
                {
                    Console.WriteLine("Welcome, please choose the operation : ");
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                    char operationIndex = Convert.ToChar(Console.ReadLine());

                    switch (operationIndex)
                    {
                        case '1':
                            RegisterUser();
                            answer = 'N';
                            break;
                        case '2':
                            Login();
                            answer = 'N';
                            break;
                        case '3':
                            Console.WriteLine("Thank you for your cooperation.");
                            answer = 'N';
                            return;
                        default:
                            Console.WriteLine("Invalid operation. Please try again.");
                            answer = 'Y';
                            break;
                    }

                    Console.WriteLine("Do you want to try another operation like Register or Log in?(Y/N)");
                    answer = Convert.ToChar(Console.ReadLine().ToUpper());

                }
                catch (Exception ex)
                {
                    answer = 'Y';
                    Console.WriteLine("Invalid operation. Please try again.");
                }
            } while (answer.Equals('Y'));
        }
        static void RegisterUser()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter ID Number: ");
            string idNumber = Console.ReadLine();

            if (idNumber.Length != 11)
            {
                Console.WriteLine("ID Number must have exactly 11 symbols. ");
                return;
            }
            bool exists = clientsList.Exists(x => x.IdNumber.Trim().Equals(idNumber.Trim(), StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                Console.WriteLine("This client already exists.");
                return;
            }
            else
            {
                string password = Convert.ToString(Random.Shared.Next(1000, 9999));

                Clients client = new Clients
                {
                    Id = clientsList.Count + 1,
                    Name = name,
                    Surname = lastName,
                    IdNumber = idNumber,
                    Password = password,
                    Balance = 0,
                    TransactionHistory = default
                };

                clientsList.Add(client);

                LogOperation(client, $"{client.Name} {client.Surname} registered {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");

                Console.WriteLine($"You registered successfully. Your password is: {password}");

                AddNewCustumerToJson(client);

            }
        }

        static void Login()
        {
            Console.Write("Enter ID Number: ");
            string idNumber = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();


            Clients currentClient = clientsList.Find(x => x.IdNumber == idNumber && x.Password == password);

            if (currentClient == null)
            {
                Console.WriteLine("This client doesn't exist .");
                return;
            }
            else
            {

                char answer = 'Y';

                do
                {
                    try
                    {
                        Console.WriteLine($"Welcome ,{currentClient.Name} {currentClient.Surname}");
                        Console.WriteLine("Please choose operation");
                        Console.WriteLine("1. Check balance");
                        Console.WriteLine("2. Deposit");
                        Console.WriteLine("3. Withdrawal");
                        Console.WriteLine("4. Check history");
                        Console.WriteLine("5. Log Out");


                        char operation = Convert.ToChar(Console.ReadLine());

                        switch (operation)
                        {
                            case '1':
                                CheckBalance(currentClient);
                                break;
                            case '2':
                                Deposit(currentClient);
                                break;
                            case '3':
                                Withdrawal(currentClient);
                                break;
                            case '4':
                                HistoryCheck(currentClient);
                                break;
                            case '5':
                                Console.WriteLine("Logging out.");
                                return;

                        }

                        Console.WriteLine("Do you want to try another operation?(Y/N)");
                        answer = Convert.ToChar(Console.ReadLine().ToUpper());

                    }
                    catch (Exception ex)
                    {
                        answer = 'Y';
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                } while (answer.Equals('Y'));

            }
        }

        static void CheckBalance(Clients client)
        {
            Console.WriteLine($"Your current balance : {client.Balance} Gel.");
            LogOperation(client, $"{client.Name} {client.Surname} checked the balance on {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");
        }

        static void Deposit(Clients client)
        {
            int check = 0;

            while (check == 0)
            {
                Console.Write("Please indicate amount: ");
                try
                {
                    int amount = int.Parse(Console.ReadLine());
                    client.Balance += amount;
                    LogOperation(client, $"{client.Name} {client.Surname} made deposite of {amount} GEL on {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");
                    check = 1;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please indicate correct amount.");
                    check = 0;
                }

            }

        }

        static void Withdrawal(Clients client)
        {
            int check = 0;
            Console.Write("Please indicate amount: ");

            while (check == 0)
            {
                try
                {
                    int amount = int.Parse((Console.ReadLine()));

                    if (amount > client.Balance)
                    {
                        Console.WriteLine("You don't have enought amount on your balance.");
                        return;
                    }
                    client.Balance -= amount;

                    LogOperation(client, $"{client.Name} {client.Surname} made withdraw of {amount} GEL on {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");

                    check = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please indicate correct amount.");
                    check = 0;
                }
            }
        }

        static void HistoryCheck(Clients client)
        {
            if (client.TransactionHistory is null)
            {
                Console.WriteLine("No transaction history.");
                return;
            }

            Console.WriteLine("Transaction history:");
            LogOperation(client, $"{client.Name} {client.Surname} checked transaction history on {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}");
            Console.WriteLine($"{client.TransactionHistory}");
        }

        static void LogOperation(Clients client, string operation)
        {
            string currentTransactions = client.TransactionHistory;
            client.TransactionHistory = $"{currentTransactions}\n{operation}";

            SavetransactionToJson(operation);
        }
        static void SavetransactionToJson(string input)
        {
            string existingJson = File.ReadAllText(_transactionsFileLocation);
            existingJson = existingJson.TrimEnd(']');
            input = $"\"{input}\"";


            File.WriteAllText(_transactionsFileLocation, $"{existingJson},\n{input}]");
        }

        static void AddNewCustumerToJson(Clients client)
        {
            //string jsonFormaterStart = "{";
            //string jsonFormaterEnd = "}";
            var jsonObject = JsonSerializer.Serialize(client);
            //var finalJson = $"{jsonFormaterStart}{jsonObject}{jsonFormaterEnd}";
            SaveClientToJson(jsonObject);
        }

        static void SaveClientToJson(string input)
        {
            if (!input.StartsWith("{") || !input.EndsWith("}"))
                throw new FormatException("Invalid json format");

            string existingJson = File.ReadAllText(_clientsFileLocation);

            existingJson = existingJson.TrimEnd(']');

            input = $"{input}";

            File.WriteAllText(_clientsFileLocation, $"{existingJson},{input}]");
        }
        static void LoadClientsFromJson()
        {
            if (File.Exists(_clientsFileLocation))
            {
                List<Clients> _temp = new();
                string json = File.ReadAllText(_clientsFileLocation);
                _temp = JsonSerializer.Deserialize<List<Clients>>(json);

                foreach (Clients client in _temp)
                {
                    clientsList.Add(client);
                }
            }
        }

    }
}

