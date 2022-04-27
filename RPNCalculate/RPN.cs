using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNCalculate
{
    public class RPN
    {
        static public double Calculate(string input)
        {
            string output = GetExpression(input);
            double result = Counting(output);
            return result;
        }

        static private string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operandsStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;

                if (Char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;

                        if (i == input.Length) break;
                    }

                    output += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operandsStack.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        char s = operandsStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operandsStack.Pop();
                        }
                    }
                    else
                    {
                        if(operandsStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operandsStack.Peek()))
                                output += operandsStack.Pop().ToString() + " ";

                        operandsStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }
            while (operandsStack.Count > 0)
                output += operandsStack.Pop() + " ";
            
            return output;
        }

        static private double Counting(string input)
        {
            double result = 0;
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
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i])
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                        case '%': result = b / 100 * a; break;
                        case '^': result = Math.Pow(b, a); break;
                    }
                    temp.Push(result);
                }
            }

            return temp.Peek();
        }

        static private bool IsDelimeter(char c)
        {
            if (" =".IndexOf(c) != -1)
                return true;
            return false;
        }

        static private bool IsOperator(char c)
        {
            if ("+-/*^%()".IndexOf(c) != -1)
                return true;
            return false;
        }

        static private byte GetPriority(char c)
        {
            switch (c)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 5;
                case '%': return 6;
                case '^': return 7;
                default: return 8;
            }
        }
    }
}
