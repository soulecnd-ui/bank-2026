
using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class CurrentAccount
{
    public string Number { get; set; }
    public double Balance { get; private set; }
    public double CreditLine { get; set; }
    public Person Owner { get; set; }

    public void Withdraw(double amount)
    {
        if (amount > 0 && Balance - amount >= -CreditLine)
        {
            Balance -= amount;
        }
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance += amount;
        }
    }
}

public class Bank
{
    public Dictionary<string, CurrentAccount> Accounts { get; } = new Dictionary<string, CurrentAccount>();
    public string Name { get; set; }

    public void AddAccount(CurrentAccount account)
    {
        Accounts.Add(account.Number, account);
    }

    public void DeleteAccount(string number)
    {
        Accounts.Remove(number);
    }

    public double GetBalance(string number)
    {
        if (Accounts.TryGetValue(number, out CurrentAccount account))
        {
            return account.Balance;
        }

        return 0;
    }

    public double GetTotalBalance(Person person)
    {
        return Accounts.Values
            .Where(account => account.Owner == person)
            .Sum(account => account.Balance);
    }
}

public class Program
{
    public static void Main()
    {
        Person personne = new Person
        {
            FirstName = "Soule",
            LastName = "Conde",
            BirthDate = new DateTime(1996, 1, 15)
        };

        Bank banque = new Bank
        {
            Name = "Ma Banque"
        };

        CurrentAccount compte1 = new CurrentAccount
        {
            Number = "001",
            Owner = personne,
            CreditLine = 100
        };

        CurrentAccount compte2 = new CurrentAccount
        {
            Number = "002",
            Owner = personne,
            CreditLine = 50
        };

        compte1.Deposit(500);
        compte2.Deposit(300);

        banque.AddAccount(compte1);
        banque.AddAccount(compte2);

        Console.WriteLine($"Banque : {banque.Name}");
        Console.WriteLine($"Solde du compte 001 : {banque.GetBalance("001")}");
        Console.WriteLine($"Total des comptes de {personne.FirstName} : {banque.GetTotalBalance(personne)}");
    }
}
