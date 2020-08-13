using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

class IncomeTaxDev
{
    public static void main()
    {
        //decimal income = Math.Round(GetEnterValue("taxable income"), 2);
        //decimal deduction = Math.Round(GetEnterValue("tax deduction"), 2);

        List<decimal> results = new List<decimal>
        {
            new decimal(3175.09),
            new decimal(13434.12),
            new decimal(95407.45),
            new decimal(194719.30),
            new decimal(34217.27),
            new decimal(15397.77),
        };

        List<decimal> incomes = new List<decimal>
        {
            new decimal(36734.29),//3175.09
            new decimal(76158.34),//13434.12
            new decimal(280379.8),//95407.45
            new decimal(499542.14),//194.719.30
            new decimal(129620.87),//34217.27
            new decimal(79575.69),//34217.27
        };
        List<decimal> deductions = new List<decimal>
        {
            new decimal(1823.77),
            new decimal(8813.14),
            new decimal(8578.27),
            new decimal(7048.49),
            new decimal(3349.31),
            new decimal(6188.31),
        };
        for (int i = 0; i < 6; i++)
        {
            decimal income = incomes[i];
            decimal deduction = deductions[i];
            Console.WriteLine(income);
            Console.WriteLine(deduction);
            income = RoundMoeny(income);
            deduction = RoundMoeny(deduction);
            Console.WriteLine(income);
            Console.WriteLine(deduction);


            var a = income - deduction;
            decimal tax = GetTaxDeduction(a);
            Console.WriteLine("Your tax after deductions is: " + DecimalFormat(tax));
            Console.WriteLine("except:{0}", results[i]);

            Console.WriteLine("-----------------------------------------------------------");
        }

    }
    

    private static decimal GetEnterValue(string t)
    {
        decimal income = 0;
        bool isValid = false;
        while (!isValid)
        {

            Console.Write("Enter your {0}: ",t);
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
        if(income < 18201)
        {
            over = new decimal(0);
        }
        if(income >= 18201 && income < 37001)
        {
            over = new decimal(0.19);
            border = new decimal(18200);
        }
        if(income >= 37001 && income < 90001)
        {
            tax = new decimal(3572);
            over = new decimal(0.325);
            border = new decimal(37000);
        }
        if(income >= 90001 && income < 180001)
        {
            tax = 20797;
            over = new decimal(0.37);
            border = 90000;
        }
        if(income >= 180001)
        {
            tax = new decimal(54097);
            over = new decimal(0.45);
            border = new decimal(180000);
        }
        var count = income - border;
        decimal overTax = over * count;
        return RoundMoeny2(tax + overTax);
    }

    private static string DecimalFormat(decimal money)
    {
        return string.Format("{0:C}", money);
    }

    private static decimal RoundMoeny(decimal money)
    {
        string result = money.ToString();
        result = result.Split(".")[0];
        return decimal.Parse(result);
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

