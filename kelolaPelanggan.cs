using System;
using System.Collections.Generic;
using System.Linq;

namespace GymMembershipVirtual
{
    class KelolaPelanggan
    {
        public List<Pelanggan> Pelangganan = new List<Pelanggan>();

        public KelolaPelanggan()
        {
            isiPelanggan();
        }

        private void isiPelanggan()
        {
            // Isi contoh member (bisa lngsung coba)
            Pelangganan.Add(new Pelanggan ("noval", "noval123", "Noval Dwiputra",
                new GoldMembership(), 6, 101, true));

            Pelangganan.Add(new Pelanggan ("kevin", "kevin123", "Kevin Santoso",
                new SilverMembership(), 3, 102, true));

            Pelangganan.Add(new Pelanggan("fariz", "fariz123", "Fariz Ramadhan",
                new BronzeMembership(), 1, 0, true));
        }

        public Pelanggan cariUsername(string user)
        {
            return Pelangganan.Find(m => m.username.Equals(user, StringComparison.OrdinalIgnoreCase));
        }

        public Pelanggan cariNamaLengkap(string fullName)
        {
            return Pelangganan.Find(m => m.namaLengkap.Equals(fullName, StringComparison.OrdinalIgnoreCase));
        }

        public void hapusPelanggan(string user)
        {
            var m = cariUsername(user);
            if (m != null) Pelangganan.Remove(m);
        }

        public void tambahPelanggan()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH MEMBER BARU ===");

            Console.Write("Masukkan username baru: ");
            string username = Console.ReadLine();

            if (cariUsername(username) != null)
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

            Pelangganan.Add(new Pelanggan(username, password, fullname, plan, duration, locker, true));

            Console.WriteLine("\nMember baru berhasil ditambahkan!");
            Console.WriteLine($"Username : {username}");
            Console.WriteLine($"Tipe     : {plan.GetName()}");
            Console.WriteLine($"Locker   : {(locker == 0 ? "-" : locker)}");
        }

        public int TotalPelanggan()
        {
            return Pelangganan.Count;
        }

        public List<Pelanggan> cariTipeMembership(string tipe)
        {
            return Pelangganan.Where(m => m.membershipPlan.GetName().Equals(tipe, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void ToggleAktif(string user)
        {
            var m = cariUsername(user);
            if (m != null) m.isAktif = !m.isAktif;
        }

        public void PrintTabelSemua()
        {
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,-7} | {4,-8} | {5,-12} | {6,-30} |",
                "Username", "Tipe", "Nama Lengkap", "Durasi", "Loker", "Status", "Benefit");
            Console.WriteLine(new string('-', 110));
            foreach (var m in Pelangganan)
            {
                Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,6} bln | {4,6} | {5,-12} | {6,-30} |",
                    m.username,
                    m.membershipPlan.GetName(),
                    m.namaLengkap,
                    m.durasiLangganan,
                    (m.loker == 0 ? "-" : m.loker.ToString()),
                    (m.isAktif ? "Aktif" : "Expired"),
                    Potong(m.membershipPlan.GetBenefit(), 30));
            }
            Console.WriteLine(new string('-', 110));
        }

        public void PrintTabelSatu(List<Pelanggan> list)
        {
            Console.WriteLine(new string('-', 110));
            Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,-7} | {4,-8} | {5,-12} | {6,-30} |",
                "Username", "Tipe", "Nama Lengkap", "Durasi", "Loker", "Status", "Benefit");
            Console.WriteLine(new string('-', 110));
            foreach (var m in list)
            {
                Console.WriteLine("| {0,-12} | {1,-8} | {2,-20} | {3,6} bln | {4,6} | {5,-12} | {6,-30} |",
                    m.username,
                    m.membershipPlan.GetName(),
                    m.namaLengkap,
                    m.durasiLangganan,
                    (m.loker == 0 ? "-" : m.loker.ToString()),
                    (m.isAktif ? "Aktif" : "Expired"),
                    Potong(m.membershipPlan.GetBenefit(), 30));
            }
            Console.WriteLine(new string('-', 110));
        }

        private string Potong(string kata, int max)
        {
            if (kata.Length <= max) return kata;
            return kata.Substring(0, max - 3) + "...";
        }
    }
}