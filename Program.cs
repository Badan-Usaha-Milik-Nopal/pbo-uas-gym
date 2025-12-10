// Menu Admin & User

using System;

namespace GymMembershipVirtual
{
    class Program
    {
        static KelolaPelanggan manager = new KelolaPelanggan();
        static List<AdminAccount> admins = new List<AdminAccount> {
        new AdminAccount("admin", "admin123")
    };

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== BUMN GYM MEMBERSHIP ===");
            Console.WriteLine("1. Login (Admin / User)");
            Console.WriteLine("2. Daftar (User)");
            Console.WriteLine("3. Exit");
            Console.Write("Pilih: ");
            string pilihan = Console.ReadLine();

            if (pilihan == "1") DoLogin();
            else if (pilihan == "2") DaftarAkun();
            else if (pilihan == "3") { Console.WriteLine("Bye"); break; }
            else { Console.WriteLine("Pilihan tidak valid"); Pause(); }
        }
    }

    static void DoLogin()
    {
        Console.Clear();
        Console.Write("Username: ");
        string user = Console.ReadLine();
        Console.Write("Password: ");
        string pass = ReadPassword();

        // cek admin
        var adm = admins.Find(a => a.username == user && a.password == pass);
        if (adm != null)
        {
            AdminMenu();
            return;
        }

        // cek member
        var mem = manager.cariUsername(user);
        if (mem != null && mem.password == pass)
        {
            UserMenu(mem);
            return;
        }

        Console.WriteLine("\nLogin gagal: username/password salah.");
        Pause();
    }

    static void DaftarAkun()
    {
        Console.Clear();
        Console.Write("Username: ");
        string user = Console.ReadLine();
        Console.Write("Password: ");
        string pass = ReadPassword();
        Console.Write("\nNama lu: ");
        string nama = Console.ReadLine();
        Console.Write("1. Gold (6 Bulan)\n" +
            "2. Silver (3 Bulan)\n" +
            "3. Bronze (1 Bulan)\n" +
            "Tipe Membership: ");
        string member = Console.ReadLine();

        // cek member
        var mem = manager.cariUsername(user);
        if (mem != null)
        {
            Console.WriteLine("Udah ada akun");
            return;
        }
        if (member == "1")
        {
            int loker = new Random().Next(200, 300);
            manager.Pelangganan.Add(new Pelanggan(user, pass, nama,
                new GoldMembership(), 6, loker, true));

            Console.WriteLine($"\nLoker Gold kamu: {loker}");
        }
        else if (member == "2")
        {
            int loker = new Random().Next(200, 300);   // Silver: auto random
            manager.Pelangganan.Add(new Pelanggan(user, pass, nama,
                new SilverMembership(), 3, loker, true));

            Console.WriteLine($"\nLoker Silver kamu: {loker}");
        }
        else if (member == "3")
        { 
            manager.Pelangganan.Add(new Pelanggan (user, pass, nama,
                new BronzeMembership(), 1, 0, true));

            Console.WriteLine("\nBronze tidak mendapatkan loker.");
        }

        Console.WriteLine("\nDaftar berhasil. Terima kasih! Selamat berolahraga.");
        Pause();
    }

    // ---------- Admin menu (B, D, E, F, G, H) ----------
    static void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ADMIN MENU ===");
            Console.WriteLine("A. Tambah Member Baru");
            Console.WriteLine("B. Lihat Daftar Member");
            Console.WriteLine("C. Hapus Member");
            Console.WriteLine("D. Lihat Total Jumlah Member");
            Console.WriteLine("E. Lihat Member berdasarkan Tipe (Gold/Silver/Bronze)");
            Console.WriteLine("F. Ubah Status Aktif/Nonaktif Member");
            Console.WriteLine("X. Logout");
            Console.Write("Pilih: ");
            string c = Console.ReadLine().ToUpper();

            if (c == "A")
            {
                manager.tambahPelanggan();
                Pause();
            }

            else if (c == "B")
            {
                Console.Clear();
                Console.WriteLine("== Daftar Member ==");
                manager.PrintTabelSemua();
                Pause();
            }
            else if (c == "C")
            {
                Console.Write("Masukkan username member yang akan dihapus: ");
                string u = Console.ReadLine();
                var m = manager.cariUsername(u);
                if (m == null) { Console.WriteLine("Member tidak ditemukan"); Pause(); continue; }
                Console.Write($"Yakin hapus {m.namaLengkap} ({m.username})? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    manager.hapusPelanggan(u);
                    Console.WriteLine("Member dihapus.");
                }
                else Console.WriteLine("Batal.");
                Pause();
            }
            
            else if (c == "D")
            {
                Console.WriteLine($"Total member: {manager.TotalPelanggan()}");
                Pause();
            }
            else if (c == "E")
            {
                Console.Write("Masukkan tipe (Gold/Silver/Bronze): ");
                string tipe = Console.ReadLine();
                var list = manager.cariTipeMembership(tipe);
                if (list.Count == 0) { Console.WriteLine("Tidak ada member dengan tipe itu."); Pause(); continue; }
                Console.WriteLine($"== Member Tipe {tipe} ==");
                manager.PrintTabelSatu(list);
                Pause();
            }
            else if (c == "F")
            {
                Console.Write("Masukkan username member: ");
                string u = Console.ReadLine();
                var m = manager.cariUsername(u);
                if (m == null) { Console.WriteLine("Member tidak ditemukan"); Pause(); continue; }
                Console.WriteLine($"Status saat ini: {(m.isAktif ? "Aktif" : "Expired")}");
                Console.Write("Ganti status? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    manager.ToggleAktif(u);
                    Console.WriteLine("Status diubah.");
                }
                else Console.WriteLine("Batal.");
                Pause();
            }
            else if (c == "X") break;
            else { Console.WriteLine("Pilihan tidak valid"); Pause(); }
        }
    }

    // ---------- User menu (all user features) ----------
    static void UserMenu(Pelanggan member)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== USER MENU (Selamat datang {member.namaLengkap}) ===");
            Console.WriteLine("1. Lihat Profil");
            Console.WriteLine("2. Lihat Benefit Membership");
            Console.WriteLine("3. Ubah Password");
            Console.WriteLine("4. Beli / Upgrade / Ganti Membership");
            Console.WriteLine("0. Logout");
            Console.Write("Pilih: ");
            string c = Console.ReadLine();

            if (c == "1")
            {
                Console.WriteLine("== Profil Anda ==");
                Console.WriteLine($"Username : {member.username}");
                Console.WriteLine($"Nama     : {member.namaLengkap}");
                Console.WriteLine($"Membership: {member.membershipPlan.GetName()}");
                Console.WriteLine($"Durasi   : {member.durasiLangganan} bulan");
                Console.WriteLine($"Loker    : {(member.loker == 0 ? "-" : member.loker.ToString())}");
                Console.WriteLine($"Status   : {(member.isAktif ? "Aktif" : "Expired")}");
                Pause();
            }

            else if (c == "2")
            {
                Console.WriteLine("== Benefit Membership ==");
                Console.WriteLine(member.membershipPlan.GetBenefit());
                Pause();
            }
            else if (c == "3")
            {
                Console.Write("Masukkan password lama: ");
                string oldp = ReadPassword();
                if (oldp != member.password) { Console.WriteLine("\nPassword lama salah."); Pause(); continue; }
                Console.Write("\nMasukkan password baru: ");
                string np = ReadPassword();
                Console.Write("\nKonfirmasi password baru: ");
                string np2 = ReadPassword();
                if (np != np2) { Console.WriteLine("\nKonfirmasi tidak cocok."); Pause(); continue; }
                member.password = np;
                Console.WriteLine("\nPassword berhasil diubah.");
                Pause();
            }
            else if (c == "4")
            {
                BuyOrUpgradeMembership(member);
            }
            else if (c == "0")
            {
                break;
            }

            else { Console.WriteLine("Pilihan tidak valid"); Pause(); }
        }
    }

    static void BuyOrUpgradeMembership(Pelanggan member)
    {
        Console.Clear();
        Console.WriteLine("=== BELI / UPGRADE MEMBERSHIP ===");
        Console.WriteLine($"Membership Anda saat ini: {member.membershipPlan.GetName()}");
        Console.WriteLine();

        Console.WriteLine("Pilih membership baru:");
        Console.WriteLine("1. Gold");
        Console.WriteLine("2. Silver");
        Console.WriteLine("3. Bronze");
        Console.Write("Pilihan: ");
        string pilih = Console.ReadLine();

        Membership newPlan = null;

        if (pilih == "1") newPlan = new GoldMembership();
        else if (pilih == "2") newPlan = new SilverMembership();
        else if (pilih == "3") newPlan = new BronzeMembership();
        else
        {
            Console.WriteLine("Pilihan tidak valid!");
            Pause();
            return;
        }

        // Cek apakah membership yang dipilih sama dengan yang sekarang
        if (newPlan.GetName() == member.membershipPlan.GetName())
        {
            Console.WriteLine("Anda sudah menggunakan membership ini!");
            Pause();
            return;
        }

        Console.WriteLine($"\nAnda memilih membership: {newPlan.GetName()}");
        Console.Write("Konfirmasi pembayaran (Y/N)? ");
        string confirm = Console.ReadLine().ToLower();

        if (confirm != "y")
        {
            Console.WriteLine("Pembelian dibatalkan.");
            Pause();
            return;
        }

        // Ganti membership (replace)
        member.membershipPlan = newPlan;

        // Atur nomor loker sesuai tier baru
        if (newPlan.GetName() == "Bronze")
            member.loker = 0; // Bronze tidak punya loker eksklusif
        else
            member.loker = new Random().Next(200, 300); // contoh loker baru

        Console.WriteLine($"\nMembership berhasil diganti menjadi {newPlan.GetName()}!");
        Console.WriteLine($"Nomor loker baru: {(member.loker == 0 ? "-" : member.loker)}");

        Pause();
    }


    // ----------------- helpers -----------------
    static void Pause()
    {
        Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
        Console.ReadLine();
    }

    // Simple mask input for password (works in console)
    static string ReadPassword()
    {
        string pass = "";
        ConsoleKeyInfo key;
        while (true)
        {
            key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace)
            {
                if (pass.Length > 0)
                {
                    pass = pass.Substring(0, pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else
            {
                pass += key.KeyChar;
                Console.Write("*");
            }
        }
        return pass;
    }
    }
}