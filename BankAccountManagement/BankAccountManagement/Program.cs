namespace BankAccountManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = null;
            Menues Menues = new Menues();

            while (true)
            {
                if (user == null)
                {
                    user = Menues.MainMenue(user);
                }
                else
                {
                    user = Menues.UserMenu(user);
                }

            }
        }
    }
}
