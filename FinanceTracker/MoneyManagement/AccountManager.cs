﻿using FinanceTracker.Classes;
using FinanceTracker.DataAccess;

namespace FinanceTracker.MoneyManagement
{
    public class AccountManager
    {
        private readonly IAccountRepository _accountRepository;
        public List<Account> Accounts;
        //public Account Account;

        public AccountManager(IAccountRepository accountRepository, Guid optional = new Guid()) //to give in an optional Guid parameter
        {
            var guidIsEmpty = optional == Guid.Empty;

            _accountRepository = accountRepository;
            Accounts = LoadAccounts();

            //if (!guidIsEmpty)
            //{
            //    Account = LoadAccountById(optional);
            //}
        }

        //public Account LoadAccountById(Guid id) 
        //{ return _accountRepository.LoadAccountById(id); }
        
        public List<Account> LoadAccounts() 
        { return _accountRepository.LoadAccounts(); }

        public void SaveAccounts (List<Account> accounts)
        { _accountRepository.SaveAccounts(accounts);}

        //public void SaveAccounts(List<Account> accounts)
        //{ _accountRepository.SaveAccounts(accounts); }
    }
}
