using LegacyApp;
using System;

namespace LegacyAppConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * DO NOT CHANGE THIS FILE AT ALL
             */

            // Konfiguracja kontenera IoC
            var userService = new UserService(
                new UserRepository(),       // Zakładając, że UserRepository to konkretna implementacja interfejsu IUserRepository
                new ClientRepository(),     // Zakładając, że ClientRepository to konkretna implementacja interfejsu IClientRepository
                new UserCreditService()     // Zakładając, że UserCreditService to konkretna implementacja interfejsu IUserCreditService
            );

            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
            if (addResult)
                Console.WriteLine($"Adding John Doe was successful");
            else
                Console.WriteLine($"Adding John Doe was unsuccessful");
        }
    }
}