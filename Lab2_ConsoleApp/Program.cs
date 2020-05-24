using System;
using Lab2;

namespace Lab2_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Eratosthenes e;
            PasswordManager passwordManager = new PasswordManager();
            string encrypted = passwordManager.EncryptPasswordAES("6\\Rn/\"Le7j'W*X6f", "uTbO6Gyfh6pdPG8uhMtaQjml/lBICm7Ga3CrWx9KxnMMbFFUIbMn7B1pQC5T37Sa1zgesGezOP17DLIiRyJdRw==");
            Console.WriteLine(encrypted);
            Console.WriteLine(passwordManager.DecryptPasswordAES(encrypted, "uTbO6Gfh6pdPG8uhMtaQjml/lBICm7Ga3CrWx9KxnMMbFFUIbMn7B1pQC5T37Sa1zgesGezOP17DLIiRyJdRw=="));
            Console.WriteLine(passwordManager.GenerateSaltString());
            Console.ReadLine();
        }
    }
}
