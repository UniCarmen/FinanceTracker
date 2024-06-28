﻿using FinanceTracker.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess
{
    public interface IAccountRepository
    {
        public List<Account> LoadAccounts();
        public void SaveAccounts(List<Account> accounts);
        public Account LoadAccountById(Guid id);
        //public void SaveAccountById(Account account);
    }
}
