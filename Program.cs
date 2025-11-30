// Menu Admin & User

using System;

namespace GymMembershipVirtual
{
    class Program
    {
        static void Main(string[] args)
        {
            Membership gold = new GoldMembership();
            Membership silver = new SilverMembership();
            Membership bronze = new BronzeMembership();

            Console.WriteLine($"{gold.GetName()} - Rp{gold.GetPrice():N0} - {gold.GetBenefit()}");
            Console.WriteLine($"{silver.GetName()} - Rp{silver.GetPrice():N0} - {silver.GetBenefit()}");
            Console.WriteLine($"{bronze.GetName()} - Rp{bronze.GetPrice():N0} - {bronze.GetBenefit()}");

            Console.ReadKey();
        }
    }
}