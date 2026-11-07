﻿namespace Bank2026;

public class CurrentAccount : Account
{
    public double CreditLine { get; set; }

    public override void Withdraw(double amount)
    {
        if (amount > 0 && Balance - amount >= -CreditLine)
        {
            RemoveBalance(amount);
        }
    }

    protected override double CalculInterets()
    {
        if (Balance > 0)
        {
            return Balance * 0.03;
        }
        else
        {
            return Balance * 0.0975;
        }
    }
}