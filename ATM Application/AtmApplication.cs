using System;
using System.Collections.Generic;

namespace ATM_Application
{
    public class AtmApplication
    {
        //This is a private instance of a Bank class
        private Bank bank;

        //When the AtmApplication object is created, it initialize Bank instance to manage bank accounts
        public AtmApplication()
        {
            bank = new Bank();
        }
        //This is the main method to run the application
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("        WELCOME TO THE ATM         ");
                Console.WriteLine("=====================================");
                Console.WriteLine("\nChoose the following options by the number associated");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Select Account");
                Console.WriteLine("3. Exit");
                Console.WriteLine("=====================================");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        SelectAccount();
                        break;
                    case "3":
                        Console.Write("Do you want to exit (y/n):");
                        char exitChoice = Console.ReadLine()[0];
                        if (exitChoice == 'y' || exitChoice == 'Y')
                        {
                            Console.WriteLine("Exiting the application. Bye!");
                            return;
                        }
                        else
                        {
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid Option. Please try again");
                        break;
                }
            }
        }

        //This method reads the input from the user and creates the account
        private void CreateAccount()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("         CREATE A NEW ACCOUNT        ");
            Console.WriteLine("=====================================");

            int accountNumber = 0;
            while (true)
            {
                try
                {
                    Console.Write("\nEnter account number (100-1000): ");
                    accountNumber = int.Parse(Console.ReadLine());

                    if (accountNumber < 100 || accountNumber > 1000)
                    {
                        Console.WriteLine("Account number must be between 100 and 1000.");
                        continue; // Retry input for account number if invalid
                    }

                    //Checking if account number entered by user already exists or not
                    Account checkAccount = bank.RetrieveAccount(accountNumber);
                    if (checkAccount != null)
                    {
                        Console.WriteLine("Account number already exists.");
                        continue;
                    }
                    break; // Exit loop if account number is valid
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input for account number. Please enter a valid number between 100 and 1000.");
                }
            }

            //Validating initial balance input
            double initialBalance = 0;
            while (true)
            {
                Console.Write("\nEnter initial balance ($): ");
                try
                {
                    initialBalance = double.Parse(Console.ReadLine());
                    if (initialBalance < 0)
                    {
                        Console.WriteLine("Initial balance must be greater than or equal to 0.");
                        continue;
                    }
                    break; 
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input for balance. Please enter a valid number.");
                }
            }

            // Validating interest rate input
            double interestRate = 0;
            while (true)
            {
                Console.Write("\nEnter annual interest rate (%): ");
                try
                {
                    interestRate = double.Parse(Console.ReadLine());
                    if (interestRate < 0 || interestRate > 3)
                    {
                        Console.WriteLine("Interest rate must be between 0 and 3.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input for interest rate. Please enter a valid number.");
                }
            }

            Console.Write("\nEnter account holder's name: ");
            string accountHolderName = Console.ReadLine();

            try
            {
                // Creating the account with validated inputs
                Account newAccount = new Account(accountNumber, initialBalance, interestRate, accountHolderName);
                bank.AddAccount(newAccount);
                Console.WriteLine("\nAccount created successfully.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }


        // Prompts the user to enter an account number, validates the input, 
        // checks if the account exists in the system, and if found, allows access to the account menu.
        // If the account is not found or input is invalid, the user is asked to try again.
        private void SelectAccount()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("          SELECT AN ACCOUNT         ");
            Console.WriteLine("=====================================");

            int accountNumber;
            while (true)
            {
                Console.Write("\nEnter your account number: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out accountNumber))
                {
                    Console.WriteLine("Error: Invalid input. Please enter a valid number.");
                    continue;
                }

                Account selectedAccount = bank.RetrieveAccount(accountNumber);

                if (selectedAccount != null)
                {
                    Console.Clear();
                    Console.WriteLine($"\nWelcome {selectedAccount.AccountHolderName}");
                    AccountMenu(selectedAccount);
                    break; 
                }
                else
                {
                    Console.WriteLine("Account not found. Please try again.");
                }
            }
        }

        //This method handles activites like deposit, withdraw and display transactions
        private void AccountMenu(Account account)
        {
            while (true)
            {   
                Console.WriteLine("=====================================");
                Console.WriteLine($"   ACCOUNT MENU - {account.AccountHolderName}   ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Display Transactions");
                Console.WriteLine("5. Exit");
                Console.WriteLine("=====================================");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"\n\nCurrent Balance: {account.Balance}\n");
                        break;

                    case "2":
                        double depositAmount;
                        while (true)
                        {
                            Console.Write("\nEnter the amount you want to deposit\n-> $");
                            if (double.TryParse(Console.ReadLine(), out depositAmount) && depositAmount > 0)
                            {
                                break;
                            }
                            Console.WriteLine("Invalid input. Please enter postive amount.");
                        }
                        account.Deposit(depositAmount);
                        Console.WriteLine($"\n\nDeposit successful! New Balance: ${account.Balance:F2}");
                        break;

                    case "3":
                        double withdrawAmount;
                        while (true)
                        {
                            Console.Write("\nEnter the amount you want to withdraw\n-> $");
                            if (double.TryParse(Console.ReadLine(), out withdrawAmount) && withdrawAmount > 0 && withdrawAmount <= account.Balance)
                            {
                                break;
                            }
                            Console.WriteLine("\nInvalid input or Insufficient funds.");

                        }
                        account.Withdraw(withdrawAmount);
                        Console.WriteLine($"\n\nWithdraw successful! New Balance: ${account.Balance:F2}");
                        break;

                    case "4":
                        Console.Clear();
                        account.DisplayTransactions();
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
