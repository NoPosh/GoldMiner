
namespace MyGame.Core
{
    public class Money
    {
        public int MoneyAmount { get; private set; }

        public Money(int amount = 0)
        {
            MoneyAmount = amount;
        }
        public bool SpendMoney(int amount)
        {
            if (MoneyAmount >= amount)
            {
                MoneyAmount -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddMoney(int amount)
        {
            MoneyAmount += amount;
        }
    }
}