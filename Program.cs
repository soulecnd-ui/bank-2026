﻿using Bank2026;
Person personne = new Person
{
    FirstName = "Souleymane",
    LastName = "Conde",
    BirthDate = new DateTime(1997, 1, 1)
};

CurrentAccount courant = new CurrentAccount
{
    Number = "BE001",
    Owner = personne,
    CreditLine = 500
};

SavingsAccount epargne = new SavingsAccount
{
    Number = "BE002",
    Owner = personne
};

// Dépôts
courant.Deposit(1000);
epargne.Deposit(2000);

// Retrait
courant.Withdraw(200);

// Création de la banque
Bank banque = new Bank
{
    Name = "Ma Banque"
};

// Ajouter les comptes
banque.AddAccount(courant);
banque.AddAccount(epargne);

// Affichage
Console.WriteLine("Compte courant : " + courant.Balance);
Console.WriteLine("Compte épargne : " + epargne.Balance);

Console.WriteLine("Total : " +
    banque.GetTotalBalance(personne));

// Intérêts
courant.ApplyInterest();
epargne.ApplyInterest();

Console.WriteLine("Après intérêts :");
Console.WriteLine(courant.Balance);
Console.WriteLine(epargne.Balance);