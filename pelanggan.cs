namespace GymMembershipVirtual
{
    class Pelanggan
    {
        public string Username;
        public string Password;
        public string FullName;
        public Membership MembershipPlan;
        public int DurationMonths;
        public int LockerNumber;
        public bool IsActive;

        public Pelanggan(string username, string password, string fullName,
                      Membership plan, int durationMonths, int lockerNumber, bool active)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            MembershipPlan = plan;
            DurationMonths = durationMonths;
            LockerNumber = lockerNumber;
            IsActive = active;
        }
    }
}
