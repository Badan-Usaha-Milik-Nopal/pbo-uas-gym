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
