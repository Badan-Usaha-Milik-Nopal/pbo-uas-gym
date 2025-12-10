// Data user - class member
namespace GymMembershipVirtual
{
    class Pelanggan
    {
        public string username;
        public string password;
        public string namaLengkap;
        public Membership membershipPlan;
        public int durasiLangganan;
        public int loker;
        public bool isAktif;

            public Pelanggan(string user, string pw, string fullName, Membership plan, int durasi, int lokNum, bool aktif)
        {
            username = user;
            password = pw;
            namaLengkap = fullName;
            membershipPlan = plan;
            durasiLangganan = durasi;
            loker = lokNum;
            isAktif = aktif;
        }
    }
}

// Nama, membershipnya, lokernya