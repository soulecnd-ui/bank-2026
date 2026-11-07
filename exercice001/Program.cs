
using System;
using System.Collections.Generic;

// ======================================================
// PARTIE 1 : CLASSE PERSON
// Représente une personne qui possède un ou plusieurs comptes
// ======================================================

class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}


// ======================================================
// PARTIE 2 : CLASSE ABSTRAITE ACCOUNT
// Contient les propriétés et méthodes communes aux comptes
// ======================================================

abstract class Account
{
    public string Number { get; set; } = string.Empty;

    // Le solde est lisible publiquement, mais modifiable
    // uniquement depuis la classe Account.
    public double Balance { get; private set; }

    public Person Owner { get; set; } = null!;

    // Déposer de l'argent sur le compte
    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Le montant doit être positif.");
            return;
        }

        Balance += amount;
    }

    // Permet aux classes dérivées de modifier le solde
    protected void UpdateBalance(double newBalance)
    {
        Balance = newBalance;
    }

    // Chaque type de compte définit son propre retrait
    public abstract void Withdraw(double amount);

    // Chaque type de compte définit son calcul d'intérêts
    protected abstract double CalculInterets();

    // Appliquer les intérêts au solde
    public void ApplyInterest()
    {
        double interest = CalculInterets();
        Balance += interest;
    }
}


// ======================================================
// PARTIE 3 : CLASSE CURRENTACCOUNT
// Gestion du compte courant
// ======================================================

class CurrentAccount : Account
{
    public double CreditLine { get; set; }

    // Retirer de l'argent en respectant la limite de crédit
    public override void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Le montant doit être positif.");
            return;
        }

        if (Balance - amount < -CreditLine)
        {
            Console.WriteLine("Crédit insuffisant.");
            return;
        }

        UpdateBalance(Balance - amount);
    }

    // Calcul des intérêts du compte courant
    protected override double CalculInterets()
    {
        if (Balance >= 0)
        {
            return Balance * 0.03;
        }

        return Balance * 0.0975;
    }
}


// ======================================================
// PARTIE 4 : CLASSE SAVINGSACCOUNT
// Gestion du compte d'épargne
// ======================================================

class SavingsAccount : Account
{
    public DateTime DateLastWithdraw { get; private set; }

    // Retirer de l'argent si le solde est suffisant
    public override void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Le montant doit être positif.");
            return;
        }

        if (amount > Balance)
        {
            Console.WriteLine("Solde insuffisant.");
            return;
        }

        UpdateBalance(Balance - amount);

        // Enregistrer la date du dernier retrait réussi
        DateLastWithdraw = DateTime.Now;
    }

    // Calcul des intérêts du compte d'épargne : 4,5 %
    protected override double CalculInterets()
    {
        return Balance * 0.045;
    }
}


// ======================================================
// PARTIE 5 : CLASSE BANK
// Gestion des comptes de la banque
// ======================================================

class Bank
{
    public string Name { get; set; } = string.Empty;

    private readonly Dictionary<string, Account> _accounts = new();

    // Accès en lecture à la collection des comptes
    public Dictionary<string, Account> Accounts
    {
        get { return _accounts; }
    }

    // Ajouter un compte
    public void AddAccount(Account account)
    {
        if (account == null)
        {
            Console.WriteLine("Compte invalide.");
            return;
        }

        if (_accounts.ContainsKey(account.Number))
        {
            Console.WriteLine("Ce numéro de compte existe déjà.");
            return;
        }

        _accounts.Add(account.Number, account);

        Console.WriteLine("Compte ajouté avec succès.");
    }

    // Supprimer un compte à partir de son numéro
    public void DeleteAccount(string number)
    {
        if (_accounts.Remove(number))
        {
            Console.WriteLine("Compte supprimé avec succès.");
        }
        else
        {
            Console.WriteLine("Compte introuvable.");
        }
    }

    // Retourner le solde d'un compte
    public double GetAccountBalance(string number)
    {
        if (_accounts.TryGetValue(number, out Account account))
        {
            return account.Balance;
        }

        Console.WriteLine("Compte introuvable.");
        return 0;
    }

    // Calculer la somme des comptes d'une personne
    public double GetTotalBalance(Person person)
    {
        double total = 0;

        foreach (Account account in _accounts.Values)
        {
            if (ReferenceEquals(account.Owner, person))
            {
                total += account.Balance;
            }
        }

        return total;
    }
}


// ======================================================
// PARTIE 6 : EXERCICE SUR LE POLYMORPHISME
// Plusieurs véhicules utilisent une méthode commune
// ======================================================

abstract class Vehicule
{
    public abstract void SeDeplacer();
}

class Voiture : Vehicule
{
    public override void SeDeplacer()
    {
        Console.WriteLine("La voiture roule sur la route.");
    }
}

class Avion : Vehicule
{
    public override void SeDeplacer()
    {
        Console.WriteLine("L'avion vole dans le ciel.");
    }
}

class Bateau : Vehicule
{
    public override void SeDeplacer()
    {
        Console.WriteLine("Le bateau navigue sur l'eau.");
    }
}


// ======================================================
// PARTIE 7 : PROGRAMME PRINCIPAL
// Tester les classes de la banque et le polymorphisme
// ======================================================

class Program
{
    static void Main()
    {
        // ----------------------------------------------
        // A. Créer une personne
        // ----------------------------------------------

        Person personne = new Person
        {
            FirstName = "Souleymane",
            LastName = "Conde",
            BirthDate = new DateTime(1997, 1, 15)
        };

        // ----------------------------------------------
        // B. Créer une banque
        // ----------------------------------------------

        Bank banque = new Bank
        {
            Name = "Banque IFOSUP"
        };

        Console.WriteLine("BANQUE : " + banque.Name);
        Console.WriteLine();

        // ----------------------------------------------
        // C. Créer un compte courant
        // ----------------------------------------------

        CurrentAccount courant = new CurrentAccount
        {
            Number = "C001",
            Owner = personne,
            CreditLine = 200
        };

        // ----------------------------------------------
        // D. Créer un compte d'épargne
        // ----------------------------------------------

        SavingsAccount epargne = new SavingsAccount
        {
            Number = "E001",
            Owner = personne
        };

        // ----------------------------------------------
        // E. Ajouter les comptes à la banque
        // ----------------------------------------------

        banque.AddAccount(courant);
        banque.AddAccount(epargne);

        Console.WriteLine();

        // ----------------------------------------------
        // F. Effectuer des dépôts et des retraits
        // ----------------------------------------------

        courant.Deposit(1000);
        courant.Withdraw(200);

        epargne.Deposit(500);
        epargne.Withdraw(100);

        // ----------------------------------------------
        // G. Afficher les soldes avant les intérêts
        // ----------------------------------------------

        Console.WriteLine("--- Soldes avant intérêts ---");

        Console.WriteLine(
            "Compte courant : "
            + banque.GetAccountBalance("C001") + " €");

        Console.WriteLine(
            "Compte épargne : "
            + banque.GetAccountBalance("E001") + " €");

        Console.WriteLine(
            "Total des comptes : "
            + banque.GetTotalBalance(personne) + " €");

        Console.WriteLine();

        // ----------------------------------------------
        // H. Appliquer les intérêts
        // ----------------------------------------------

        courant.ApplyInterest();
        epargne.ApplyInterest();

        // ----------------------------------------------
        // I. Afficher les soldes après les intérêts
        // ----------------------------------------------

        Console.WriteLine("--- Soldes après intérêts ---");

        Console.WriteLine(
            "Compte courant : " + courant.Balance + " €");

        Console.WriteLine(
            "Compte épargne : " + epargne.Balance + " €");

        Console.WriteLine(
            "Total des comptes : "
            + banque.GetTotalBalance(personne) + " €");

        Console.WriteLine();

        // ----------------------------------------------
        // J. Afficher la date du dernier retrait
        // ----------------------------------------------

        Console.WriteLine(
            "Dernier retrait du compte épargne : "
            + epargne.DateLastWithdraw);

        Console.WriteLine();

        // ----------------------------------------------
        // K. Tester le polymorphisme
        // ----------------------------------------------

        Console.WriteLine("--- Polymorphisme : véhicules ---");

        Vehicule voiture = new Voiture();
        Vehicule avion = new Avion();
        Vehicule bateau = new Bateau();

        voiture.SeDeplacer();
        avion.SeDeplacer();
        bateau.SeDeplacer();

        Console.WriteLine();

        // ----------------------------------------------
        // L. Tester la suppression d'un compte
        // ----------------------------------------------

        // Décommente la ligne suivante pour tester :
        // banque.DeleteAccount("C001");
    }
}
