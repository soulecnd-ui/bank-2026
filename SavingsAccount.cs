﻿namespace Bank2026;

public class SavingsAccount : Account
{
    public DateTime DateLastWithdraw { get; set; }

    public override void Withdraw(double amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            RemoveBalance(amount);
            DateLastWithdraw = DateTime.Now;
        }
    }

    protected override double CalculInterets()
    {
        return Balance * 0.045;
    }
}