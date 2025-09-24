using System;
using System.Threading;


class Program
{
    public static void Print()
    {
        Console.WriteLine("A");
        Console.WriteLine("B");
        Console.WriteLine("C");
    }

    public static void Main()
    {
        for(int i = 0; i < 3; i++)
        {
            Print();
            Thread.Sleep(500);
        }
    }
}