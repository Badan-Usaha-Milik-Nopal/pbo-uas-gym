using System;
using System.Collections.Generic;
using System.Linq;

namespace GymMembershipVirtual
{
    class MemberManager
    {
        public List<Pelanggan> Members = new List<Pelanggan>();

        public MemberManager()
        {
            SeedMembers();
        }

        private void SeedMembers()
        {
            // Seed contoh member (agar langsung bisa dicoba)
            Members.Add(new Pelanggan ("noval", "noval123", "Noval Dwiputra",
                new GoldMembership(), 6, 101, true));

            Members.Add(new Pelanggan ("kevin", "kevin123", "Kevin Santoso",
                new SilverMembership(), 3, 102, true));

            Members.Add(new Pelanggan("fariz", "fariz123", "Fariz Ramadhan",
                new BronzeMembership(), 1, 0, true));
        }

        public Pelanggan FindByUsername(string username)
        {
            return Members.Find(m => m.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public Pelanggan FindByFullName(string name)
        {
            return Members.Find(m => m.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteMember(string username)
        {
            var m = FindByUsername(username);
            if (m != null) Members.Remove(m);
        }
        
        public void AddNewMember()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH MEMBER BARU ===");

            Console.Write("Masukkan username baru: ");
            string username = Console.ReadLine();

            if (FindByUsername(username) != null)
            {
                Console.WriteLine("Username sudah digunakan!");
                return;
            }

            Console.Write("Masukkan password: ");
            string password = Console.ReadLine();

            Console.Write("Masukkan nama lengkap: ");
            string fullname = Console.ReadLine();

            Console.Write("Durasi membership (bulan): ");
            if (!int.TryParse(Console.ReadLine(), out int duration))
            {
                Console.WriteLine("Durasi tidak valid!");
                return;
            }

            Console.WriteLine("\nPilih Tipe Membership:");
            Console.WriteLine("1. Gold");
            Console.WriteLine("2. Silver");
            Console.WriteLine("3. Bronze");
            Console.Write("Pilihan: ");
            string tipe = Console.ReadLine();

            Membership plan;

            if (tipe == "1") plan = new GoldMembership();
            else if (tipe == "2") plan = new SilverMembership();
            else if (tipe == "3") plan = new BronzeMembership();
            else
            {
                Console.WriteLine("Pilihan tidak valid!");
                return;
            }

            // Tentukan loker
            int locker = 0;
            if (plan.GetName() != "Bronze")
                locker = new Random().Next(200, 300);

            Members.Add(new Pelanggan(username, password, fullname, plan, duration, locker, true));

            Console.WriteLine("\nMember baru berhasil ditambahkan!");
            Console.WriteLine($"Username : {username}");
            Console.WriteLine($"Tipe     : {plan.GetName()}");
            Console.WriteLine($"Locker   : {(locker == 0 ? "-" : locker)}");
        }

        public int TotalMembers()
        {
            return Members.Count;
        }

        public List<Pelanggan> GetByMembershipType(string type)
        {
            return Members.Where(m => m.MembershipPlan.GetName().Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void ToggleActive(string username)
        {
            var m = FindByUsername(username);
            if (m != null) m.IsActive = !m.IsActive;
        }

        public void PrintTableAll()
        {
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,-7} | {4,-8} | {5,-12} | {6,-30} |",
                "Username", "Tipe", "Nama Lengkap", "Durasi", "Locker", "Status", "Benefit");
            Console.WriteLine(new string('-', 110));
            foreach (var m in Members)
            {
                Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,6} bln | {4,6} | {5,-12} | {6,-30} |",
                    m.Username,
                    m.MembershipPlan.GetName(),
                    m.FullName,
                    m.DurationMonths,
                    (m.LockerNumber == 0 ? "-" : m.LockerNumber.ToString()),
                    (m.IsActive ? "Aktif" : "Expired"),
                    Truncate(m.MembershipPlan.GetBenefit(), 30));
            }
            Console.WriteLine(new string('-', 110));
        }

        public void PrintTableList(List<Pelanggan> list)
        {
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,-7} | {4,-8} | {5,-12} | {6,-30} |",
                "Username", "Tipe", "Nama Lengkap", "Durasi", "Locker", "Status", "Benefit");
            Console.WriteLine(new string('-', 110));
            foreach (var m in list)
            {
                Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,6} bln | {4,6} | {5,-12} | {6,-30} |",
                    m.Username,
                    m.MembershipPlan.GetName(),
                    m.FullName,
                    m.DurationMonths,
                    (m.LockerNumber == 0 ? "-" : m.LockerNumber.ToString()),
                    (m.IsActive ? "Aktif" : "Expired"),
                    Truncate(m.MembershipPlan.GetBenefit(), 30));
            }
            Console.WriteLine(new string('-', 110));
        }

        private string Truncate(string s, int max)
        {
            if (s.Length <= max) return s;
            return s.Substring(0, max - 3) + "...";
        }

    }
}

