using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalcAdv
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) //Бесконечный цикл
            {
                Console.Write("Введите выражение: "); //Предлагаем ввести выражение
                Console.WriteLine(Rpn.Calculate(Console.ReadLine())); //Считываем, и выводим результат
            }
        }
    }
}
