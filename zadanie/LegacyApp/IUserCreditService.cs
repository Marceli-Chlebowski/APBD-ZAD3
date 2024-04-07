using System;

namespace LegacyApp;

public interface IUserCreditService
{
    decimal GetCreditLimit(string lastName, DateTime dateOfBirth);
}