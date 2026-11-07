﻿namespace Bank2026;

public class Bank
{
    public Dictionary<string, Account> Accounts { get; }
        = new Dictionary<string, Account>();

    public string Name { get; set; }

    public void AddAccount(Account account)
    {
        Accounts.Add(account.Number, account);
    }

    public void DeleteAccount(string number)
    {
        Accounts.Remove(number);
    }

    public double GetBalance(string number)
    {
        if (Accounts.ContainsKey(number))
        {
            return Accounts[number].Balance;
        }

        return 0;
    }

    public double GetTotalBalance(Person person)
    {
        double total = 0;

        foreach (Account account in Accounts.Values)
        {
            if (account.Owner == person)
            {
                total += account.Balance;
            }
        }

        return total;
    }
}