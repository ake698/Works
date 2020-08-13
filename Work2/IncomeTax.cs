using System;
using System.Collections.Generic;
using System.Text;

class IncomeTax
{
    private static decimal income1 = 0;
    private static decimal deduction1 = 0;
    public static void Main()
    {
        income1 = Math.Round(GetEnterValue("taxable income"), 2);
        deduction1 = Math.Round(GetEnterValue("tax deduction"), 2);
        decimal income = RoundMoeny(income1);
        decimal deduction = RoundMoeny(deduction1);
        var a = income - deduction;
        decimal tax = GetTaxDeduction(a);
        Console.WriteLine("Your tax after deductions is: " + DecimalFormat(tax));
    }


    private static decimal GetEnterValue(string t)
    {
        decimal income = 0;
        bool isValid = false;
        while (!isValid)
        {

            Console.Write("Enter your {0}: ", t);
            var input = Console.ReadLine();

            if (!decimal.TryParse(input, out income))
            {
                Console.WriteLine("WARNING: You must enter a valid number.");
                continue;
            }

            if (income < 0)
            {
                Console.WriteLine("WARNING: You must enter a positive value.");
                continue;
            }
            isValid = true;
        }
        return income;
    }

    private static decimal GetTaxDeduction(decimal income)
    {
        decimal tax = 0;
        decimal over = 0;
        decimal border = 0;
        if (income < 18201)
        {
            over = new decimal(0);
        }
        if (income >= 18201 && income < 37001)
        {
            over = new decimal(0.19);
            border = new decimal(18200);
        }
        if (income >= 37001 && income < 90001)
        {
            tax = new decimal(3572);
            over = new decimal(0.325);
            border = new decimal(37000);
        }
        if (income >= 90001 && income < 180001)
        {
            tax = 20797;
            over = new decimal(0.37);
            border = 90000;
        }
        if (income >= 180001)
        {
            tax = new decimal(54097);
            over = new decimal(0.45);
            border = new decimal(180000);
        }
        decimal overTax = over * (income - border);
        try 
        { 
            var a = RoundMoeny2((tax + overTax));
            return a;
        }
        catch
        {
            Console.WriteLine(income1);
            Console.WriteLine(deduction1);
            Console.WriteLine(income);
        }
        return 0;
    }

    private static string DecimalFormat(decimal money)
    {
        return string.Format("{0:C}", money);
    }

    private static int RoundMoeny(decimal money)
    {
        string result = money.ToString();
        return Convert.ToInt32(result.Split(".")[0]);
    }
    private static decimal RoundMoeny2(decimal money)
    {
        money = Math.Round(money, 3);
        string result = money.ToString();
        var dot = result.Split(".")[1];
        var n = result.Split(".")[0];
        dot = dot.Substring(0, 2);
        return decimal.Parse(n + "." + dot);
    }
}
