using System;
using System.Linq;

namespace Vatscy.Equation
{
    public class Program
    {
        // 2次方程式を解く
        public static void Main(string[] args)
        {
            var a = ReadConstant("a");
            var b = ReadConstant("b");
            var c = ReadConstant("c");

            var x = Polynomial.X;
            var p = a * (x ^ 2) + b * x + c;
            Console.WriteLine("y  = " + p);

            Console.WriteLine();
            Console.WriteLine(p + " = 0");
            try
            {
                var solutions = p.SolveEquation(0);
                Console.WriteLine("x = ["
                    + (solutions.Length == 0 ? "" : solutions.Select(s => s.ToString()).Aggregate((now, next) => now + ", " + next))
                    + "]");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("y' = " + p.Differentiate());
        }

        private static double ReadConstant(string name)
        {
            string constant = null;
            while (string.IsNullOrWhiteSpace(constant))
            {
                Console.Write(name + ": ");
                constant = Console.ReadLine();
            }
            return double.Parse(constant);
        }
    }
}