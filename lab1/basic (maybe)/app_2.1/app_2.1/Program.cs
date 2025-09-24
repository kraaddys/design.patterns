using System;

class Program
{
        static void A()
        {
            B();
            C();

            Console.WriteLine("aga");
        }

        static void B()
        {
            Console.WriteLine("You Are the best.");
        }

        static void C()
        {
            Console.WriteLine("Hello, Buddy!!!");
        }

        static void Main()
        {
            A();
            A();
            A();   
        }
}