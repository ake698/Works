using System;
using System.IO;

using static System.Console;

namespace Bank
{
    public class BankAccount
    {
        // Private variables: account balance, interest rate
        // INSERT VARIABLES HERE
        private decimal funds;
        private decimal interestRate;
        /// <summary>
        /// Creates a new bank account with zero starting balance and the default
        /// interest rate of 6.2%.
        /// </summary>
        public BankAccount ()
        {
            // INSERT CODE HERE
            funds = 0;
            interestRate = new decimal(6.2);
        }

        /// <summary>
        /// Creates a new bank account with a starting balance and the default
        /// interest rate of 6.2%.
        /// </summary>
        /// <param name="startingBalance">The starting balance</param>
        public BankAccount ( decimal startingBalance )
        {
            // INSERT CODE HERE
            funds = startingBalance;
            interestRate = new decimal(6.2);
        }

        /// <summary>
        /// Creates a new bank account with designated starting balance and 
        /// interest rate.
        /// </summary>
        /// <param name="startingBalance">The starting balance</param>
        /// <param name="interestRate">The interest rate (expressed as a percentage)</param>
        public BankAccount ( decimal startingBalance, decimal interestRate )
        {
            // INSERT CODE HERE
            funds = startingBalance;
            this.interestRate = interestRate;
        }

        /// <summary>
        ///     Attempt to reduce the balance of the bank account by 'amount'. Funds will
        ///     only be deducted if amount is less than the current balance.
        /// </summary>
        /// <param name="amount">
        ///     The amount of money to (try to) deduct from the account.
        /// </param>
        /// <returns>
        ///     True if and only if funds were deducted from the account.
        /// </returns>
        public bool DeductFunds ( decimal amount )
        {
            // INSERT CODE HERE
            if(funds >= amount)
            {
                funds -= amount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Increase the balance of the account by 'amount'
        /// </summary>
        /// <param name="amount">The amount to increase the balance by</param>
        public void AddFunds ( decimal amount )
        {
            // INSERT CODE HERE
            funds += amount;
        }

        /// <summary>
        /// Returns the total balance currently in the account.
        /// </summary>
        /// <returns>The total balance currently in the account</returns>
        public decimal QueryFunds ()
        {
            // INSERT CODE HERE
            return funds;
        }

        /// <summary>
        /// Sets the account's interest rate to the rate provided
        /// </summary>
        /// <param name="interestRate">The interest rate for this account (%)
        /// </param>
        public void SetInterestRate ( decimal interestRate )
        {
            // INSERT CODE HERE
            this.interestRate = interestRate;
        }

        /// <summary>
        /// Returns the account's interest rate
        /// </summary>
        /// <returns>The percentage interest rate of this account</returns>
        public decimal GetInterestRate ()
        {
            // INSERT CODE HERE
            return interestRate;
        }

        /// <summary>
        /// Calculates the interest on the current account balance and adds it
        /// to the account
        /// </summary>
        public void ApplyInterest ()
        {
            // INSERT CODE HERE
            var profile = interestRate / 100 * funds;
            funds += profile;
        }

        /// <summary>
        ///     Test driver for Bank.BankAccount.
        ///     Interactively queries user for deposit amount, withdrawal amount,
        ///     and interest rate, applies operation and displays results.
        /// </summary>
        public static void Main ()
        {
            BankAccount acct = new BankAccount();
            bool finished = false;

            while ( !finished )
            {
                Write( menu );
                var line = ReadLine();

                if ( line == null ) break;

                var fields = line.Trim().ToLower().Split( ' ' );

                if ( fields.Length > 0 && fields[0].Length > 0 )
                {
                    decimal deposit;
                    decimal withdrawal;
                    decimal interestRate;

                    switch ( fields[0][0] )
                    {
                        case 'n':
                            switch ( fields.Length )
                            {
                                case 1:
                                    acct = new BankAccount();
                                    break;
                                case 2:
                                    deposit = decimal.Parse( fields[1] );
                                    acct = new BankAccount( deposit );
                                    break;
                                case 3:
                                    deposit = decimal.Parse( fields[1] );
                                    interestRate = decimal.Parse( fields[2] );
                                    acct = new BankAccount( deposit, interestRate );
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 'd':
                            if ( fields.Length > 1 )
                            {
                                deposit = decimal.Parse( fields[1] );
                                acct.AddFunds( deposit );
                            }
                            break;
                        case 'w':
                            if ( fields.Length > 1 )
                            {
                                withdrawal = decimal.Parse( fields[1] );
                                acct.DeductFunds( withdrawal );
                            }
                            break;
                        case 'i':
                            acct.ApplyInterest();
                            break;
                        case 'r':
                            if ( fields.Length > 1 )
                            {
                                interestRate = decimal.Parse( fields[1] );
                                acct.SetInterestRate( interestRate );
                            }
                            break;
                        case 'q':
                            finished = true;
                            break;
                        default:
                            break;
                    }
                }

                if ( !finished )
                {
                    var balance = acct.QueryFunds();
                    var interest = acct.GetInterestRate();
                    WriteLine( $"After operation: balance = {balance:0.0000000000}, interest rate = {interest:0.0000000000}" );
                }
            }
        }

        static readonly string menu =
            "Enter option:\n   n = new a/c;  d = deposit;  w = withdraw;  i = apply interest;  r = new interest rate;  q = exit.\n==> ";
    }
}
