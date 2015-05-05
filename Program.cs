using System;

namespace Vatscy.Equation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 2点の座標を入力
            var p1 = ReadPoint(1);
            var p2 = ReadPoint(2);
            Console.WriteLine("P1: " + p1);
            Console.WriteLine("P2: " + p2);

            // 2点を通る直線の方程式を求める
            var var_a = Polynomial.X;
            var a = ((var_a * p1.X - p1.Y) - (var_a * p2.X - p2.Y)).SolveLinearEquation();
            var b = (p1.Y - var_a * p1.X)[a];
            var f = a * Polynomial.X + b;
            Console.WriteLine("Linear Equation: y = " + f.ToString("x"));
            Console.WriteLine();

            // 与えられたx座標に対するy座標を求める
            while (true)
            {
                Console.Write("x = ");
                var x = double.Parse(Console.ReadLine());
                Console.WriteLine("y = " + f[x]);
                Console.WriteLine();
            }
        }

        private static Point2D ReadPoint(int index)
        {
            Console.Write("X" + index + ": ");
            var x = Console.ReadLine();
            Console.Write("Y" + index + ": ");
            var y = Console.ReadLine();
            return new Point2D(double.Parse(x), double.Parse(y));
        }

        private struct Point2D
        {
            public double X { get; private set; }
            public double Y { get; private set; }

            public Point2D(double x, double y) : this()
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return "(" + X + ", " + Y + ")";
            }
        }
    }
}