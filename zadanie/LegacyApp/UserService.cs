using System;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;

        public UserService(IUserRepository userRepository, IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _userCreditService = userCreditService ?? throw new ArgumentNullException(nameof(userCreditService));
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidUser(firstName, lastName, email, dateOfBirth))
                return false;

            var client = _clientRepository.GetById(clientId);
            if (client == null)
                return false;

            var user = CreateUserObject(firstName, lastName, email, dateOfBirth, client);
            if (user == null)
                return false;

            if (!IsUserCreditLimitValid(client, user))
                return false;

            _userRepository.AddUser(user);
            return true;
        }

        private User CreateUserObject(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            return user;
        }

        private bool IsUserCreditLimitValid(Client client, User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                var creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (client.Type == "ImportantClient")
                {
                    creditLimit *= 2;
                }
                user.CreditLimit = (int)creditLimit;
                user.HasCreditLimit = true;
            }

            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }

        private bool IsValidUser(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
                return false;

            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age >= 21;
        }
    }
}
