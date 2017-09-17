using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalcAdv
{
    class Rpn
    {
        //метод проверяющий корректрость расстановки скобок
        static private bool isCorrect(string input)
        {
            Stack<char> check = new Stack<char>();
            for (int i = 0; i < input.Length; i++)
            {

                //если есть буквы - выводим предупреждение
                if (isChar(input[i]))
                {
                    Console.WriteLine("неверное выражение");
                    return false;
                }
                if (input[i] == '(') check.Push(input[i]);
                if (input[i] == ')')
                {
                    if (check.Count <= 0)
                    {
                        Console.WriteLine("нехватает открывающих скобок");
                        return false;
                    }

                    check.Pop();
                }

            }
            if (check.Count > 0 || check.Count < 0)
            {
                Console.WriteLine("нехватает закрывающих скобок");
                return false;
            }
            if (String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Пустая строка, повторите ввод");
                return false;
            }
                
            return true;
        }

        //метод проверяет относится ли символ к буквам
        static private bool isChar(char c)
        {
            if (char.IsLetter(c))
                return true;
            return false;

        }
        //Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            else
                return false;
        }

        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(char с)
        {
            if (("+-/*^()".IndexOf(с) != -1))
                return true;
            return false;
        }
        
        //Метод возвращает приоритет оператора
        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 6;
            }
        }

        //"Входной" метод класса
        static public double Calculate(string input)
        {

            if (isCorrect(input) == false)
            {
                return 0;
            }
            string output = GetExpression(input); 
            double result = Counting(output); //Решаем полученное выражение
            return result; //Возвращаем результат
        }

        static private string GetExpression(string input)
        {
            string output = string.Empty; //Строка для хранения выражения
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов

            for (int i = 0; i < input.Length; i++) 
            {
                                  

                
                if (IsDelimeter(input[i]))
                    continue; 

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(input[i])) //Если цифра
                {
                    
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; 
                        i++; 

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; 
                    i--; 
                }

                //Если символ - оператор
                if (IsOperator(input[i])) 
                {
                    if (input[i] == '(') 
                        operStack.Push(input[i]); 
                    else if (input[i] == ')') 
                    {
                        
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) 
                                output += operStack.Pop().ToString() + " "; 

                        operStack.Push(char.Parse(input[i].ToString())); 

                    }
                }
            }

           
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; 
        }

        static private double Counting(string input)
        {
            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); 

            for (int i = 0; i < input.Length; i++) 
            {
                
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        a += input[i]; 
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); 
                    i--;
                }
                else if (IsOperator(input[i])) 
                {
                    //Берем два последних значения из стека
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i]) //И производим над ними действие, согласно оператору
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    }
                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
    }
}
