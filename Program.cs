using System;
using System.Linq;

namespace Vatscy.Equation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 2次方程式の解を求める
            Console.Write("a: ");
            var a = double.Parse(Console.ReadLine());
                        Console.Write("b: ");
            var b = double.Parse(Console.ReadLine());
                        Console.Write("b: ");
            var c = double.Parse(Console.ReadLine());
            
            var x = Polynomial.X;
            var p = a * (x ^ 2) + b * x + c;
            Console.WriteLine(p + " = 0");
            Console.WriteLine("x = [" + p.SolveEquation(0).Select(s => s.ToString()).Aggregate((now, next) => now + ", " + next) + "]");
        }
    }
}