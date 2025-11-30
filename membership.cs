// Class Membership (Parent dan Childnya = Inheritance dan Override) | Gold, Silver, dan Bronze

public class Membership
    {
        public virtual string GetName()
        {
            return "Membership Umum";
        }

        public virtual int GetPrice()
        {
            return 0;
        }

        public virtual string GetBenefit()
        {
            return "Benefit umum.";
        }
    }

    // GOLD
    public class GoldMembership : Membership
    {
        public override string GetName()
        {
            return "Gold";
        }

        public override int GetPrice()
        {
            return 349999;
        }

        public override string GetBenefit()
        {
            return "Berlangganan gym selama 6 bulan.";
        }
    }

    // SILVER
    public class SilverMembership : Membership
    {
        public override string GetName()
        {
            return "Silver";
        }

        public override int GetPrice()
        {
            return 59999;
        }

        public override string GetBenefit()
        {
            return "Berlangganan gym selama 1 bulan.";
        }
    }

    // BRONZE
    public class BronzeMembership : Membership
    {
        public override string GetName()
        {
            return "Bronze";
        }

        public override int GetPrice()
        {
            return 29999;
        }

        public override string GetBenefit()
        {
            return "Berlangganan gym selama 1 minggu.";
        }
    }