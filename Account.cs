﻿namespace Bank2026;

public abstract class Account
{
    public string Number { get; set; }
    public double Balance { get; private set; }
    public Person Owner { get; set; }

    public virtual void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance += amount;
        }
    }

    public virtual void Withdraw(double amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
        }
    }

    protected void AddBalance(double amount)
    {
        Balance += amount;
    }

    protected void RemoveBalance(double amount)
    {
        Balance -= amount;
    }

    protected abstract double CalculInterets();

    public void ApplyInterest()
    {
        Balance += CalculInterets();
    }
}