// Data user - class member
class Pelanggan()
{
    public string username;
    public string password;
    public string fullName;
    public Membership membershipPlan;
    public int durasiLangganan;
    public int loker;

        // string name, 
        public AkunPelanggan(string user, string pw, Membership plan, int durasi, int lokNum)
    {
        username = user;
        password = pw;
        membershipPlan = plan;
        durasiLangganan = durasi;
        loker = lokNum;
    }
}


// Nama, membershipnya, lokernya

