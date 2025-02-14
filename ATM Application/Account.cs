using System;
using System.Collections.Generic;


namespace ATM_Application
{
    public class Account
    {
        //These are the properties to store the account details
        //Private set ensures these properties can only be modified within the class 
        public int AccountNumber { get; private set; }
        public double Balance { get; private set; }
        public double InterestRate { get; private set; }
        public string AccountHolderName { get; private set; }
        public List<string> Transactions { get; private set; } //This property is for Transaction History

        //Initializing account with parameterized constructor
        public Account(int accountNumber, double initialBalance, double interestRate, string accountHolderName)
        {
            //Validating intital balance (must be greater than 0 or 0)
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance must be greater or equal to 0");
            }

            if (interestRate < 0 || interestRate > 100 )
            {
                throw new ArgumentException("Interest rate must be between 0 and 100");
            }

            //Initilizing the properties
            AccountNumber = accountNumber;
            Balance = initialBalance;
            InterestRate = interestRate;
            AccountHolderName = accountHolderName;
            Transactions = new List<string>();
        }

        //This method deposits the amount and adds a transaction
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                
            }
            else
            {
                Balance += amount;
                Transactions.Add($"Deposited: ${amount}");
            }
        }

        //This method withdraws the amount and adds a transaction
        public void Withdraw(double amount)
        {
            if (amount < 0 || amount > Balance)
            {
                Console.WriteLine("\nInvalid withdrawal amount or insufficient balance.");
                
            }
            else
            {
                Balance -= amount;
                Transactions.Add($"Withdrawn: ${amount}");
            }
        }

        //This method displays the transactions
        public void DisplayTransactions()
        {
            Console.WriteLine("--------------Transaction History----------------");
            DisplayUserDetails();
            foreach (var transaction in Transactions)
            {
                Console.WriteLine(transaction); 
            }
            Console.WriteLine("\n\n");
        }

        //This method displays the user details
        public void DisplayUserDetails()
        {
            Console.WriteLine("\n\n=====================================");
            Console.WriteLine($"Account Holder: {AccountHolderName}");
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Balance: ${Balance}");
            Console.WriteLine($"Interest Rate: {InterestRate}%");
            Console.WriteLine("=====================================");

        }
    }
}
