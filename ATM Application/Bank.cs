using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Application
{
    public class Bank
    {
        private List<Account> accounts;
        public Bank()
        {
            //Initializing the account field to hold the Account objects
            accounts = new List<Account>();

            for (int i = 0; i < 10; i++)
            {
                accounts.Add(new Account(100 + i, 100, 3, $"Account Holder {i+1}"));
            }
        }

        //This method adds the account object in accounts list
        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        //This method searches for an account using its account number and return account object if found
        public Account RetrieveAccount(int accountNumber)
        {
            foreach (var account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }
    }

}
