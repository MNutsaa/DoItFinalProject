namespace ATMApplication
{
    internal class Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public string? TransactionHistory { get; set; }
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Surname: {Surname}, Password: {Password}, ID Number: {IdNumber}, Balance: {Balance}";
        }
    }
}
