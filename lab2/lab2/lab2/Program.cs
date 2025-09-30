namespace Lab
{
    class Program
    {
        struct Prices
        {
            public int drink;
            public int firstDish;
            public int secondDish;

        }

        struct Selection
        {
            public int drink;
            public int firstDish;
            public int secondDish;
        }

        class Calculate
        {
            public static int CalculateTotal(Prices prices, Selection selection)
            {
                return selection.drink * prices.drink +
                    selection.firstDish * prices.firstDish +
                    selection.secondDish * prices.secondDish;
            }
        }

        static void Main(string[] args)
        {
            Prices prices = new Prices { drink = 10, firstDish = 20, secondDish = 30 };

            Selection client1 = new Selection { drink = 1, firstDish = 2, secondDish = 0 };
            Selection client2 = new Selection { drink = 2, firstDish = 0, secondDish = 0 };

            Console.WriteLine($"Результаты заказов: \n");
            Console.WriteLine($"Клиент 1 заказал: {Calculate.CalculateTotal(prices, client1)} единиц стоимости.");
            Console.WriteLine($"Клиент 2 заказал: {Calculate.CalculateTotal(prices, client2)} единиц стоимости.");

            Console.WriteLine($"\nПрограмма завершена.");
        }
    }
}