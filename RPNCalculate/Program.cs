using System;
using System.Linq;

namespace RPNCalculate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите выражение: ");
                string s = Console.ReadLine().Replace('.', ',');
                
                if (s.Any(c => char.IsLetter(c)))
                    Console.WriteLine("Неверный формат строки!");
                else Console.WriteLine(RPN.Calculate(s));
            }
        }
    }
}
