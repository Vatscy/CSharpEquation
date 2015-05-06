using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vatscy.Equation
{
    // 多項式を表します。
    public struct Polynomial
    {
        // 多項式中の変数
        public static readonly Polynomial X = new Polynomial(new Dictionary<int, double> { { 1, 1 } });

        // 度数
        public int Degree
        {
            get { return Coefficients.Count == 0 ? 0 : Coefficients.Max(c => c.Key); }
        }

        // 変数に値を代入した値を取得します。
        public double this[double value]
        {
            get { return Coefficients.Sum(c => c.Value * Math.Pow(value, c.Key)); }
        }

        private static readonly IDictionary<int, double> _coefficients_empty = new Dictionary<int, double>();

        private IDictionary<int, double> _coefficients;

        private IDictionary<int, double> Coefficients
        {
            get { return _coefficients == null ? _coefficients_empty : _coefficients; }
        }

        private Polynomial(IDictionary<int, double> coefficients)
        {
            _coefficients = coefficients;
        }

        public static implicit operator Polynomial(double value)
        {
            return value == 0 ? default(Polynomial) : new Polynomial(new Dictionary<int, double> { { 0, value } });
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            var coefficients = new Dictionary<int, double>(p1.Coefficients);

            foreach (var item2 in p2.Coefficients)
            {
                AddMonomial(coefficients, item2.Key, item2.Value);
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            var coefficients = new Dictionary<int, double>(p1.Coefficients);

            foreach (var item2 in p2.Coefficients)
            {
                AddMonomial(coefficients, item2.Key, -item2.Value);
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            var coefficients = new Dictionary<int, double>();

            foreach (var item1 in p1.Coefficients)
            {
                foreach (var item2 in p2.Coefficients)
                {
                    AddMonomial(coefficients, item1.Key + item2.Key, item1.Value * item2.Value);
                }
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator /(Polynomial p, double value)
        {
            var coefficients = new Dictionary<int, double>();

            foreach (var item in p.Coefficients)
            {
                AddMonomial(coefficients, item.Key, item.Value / value);
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator ^(Polynomial p, int power)
        {
            if (power < 0)
            {
                throw new ArgumentOutOfRangeException("power", "The value must be non-negative.");
            }

            Polynomial result = 1;

            for (var i = 0; i < power; i++)
            {
                result *= p;
            }
            return result;
        }

        public static Polynomial operator +(Polynomial p)
        {
            return p;
        }

        public static Polynomial operator -(Polynomial p)
        {
            return -1 * p;
        }

        private static void AddMonomial(Dictionary<int, double> coefficients, int index, double coefficient)
        {
            var totalCoefficient = coefficients.ContainsKey(index) ? coefficient + coefficients[index] : coefficient;

            if (totalCoefficient != 0)
            {
                coefficients[index] = totalCoefficient;
            }
            else
            {
                coefficients.Remove(index);
            }
        }

        private double GetCoefficient(int index)
        {
            return Coefficients.ContainsKey(index) ? Coefficients[index] : 0;
        }

        // 左辺をこの多項式、右辺を0としたときの1次方程式の解を求めます。
        public double SolveLinearEquation()
        {
            if (Degree != 1)
            {
                throw new InvalidOperationException("The degree must be 1.");
            }

            // ax + b = 0 
            var a = GetCoefficient(1);
            var b = GetCoefficient(0);

            return -b / a;
        }

        // 左辺をこの多項式、右辺を0としたときの2次方程式の解を求めます。
        public double[] SolveQuadraticEquation()
        {
            if (Degree != 2)
            {
                throw new InvalidOperationException("The degree must be 2.");
            }

            // ax^2 + bx + c = 0 
            var a = GetCoefficient(2);
            var b = GetCoefficient(1);
            var c = GetCoefficient(0);
            var d = b * b - 4 * a * c;

            return d > 0 ? new[] { (-b - Math.Sqrt(d)) / (2 * a), (-b + Math.Sqrt(d)) / (2 * a) }
                : d == 0 ? new[] { -b / (2 * a) }
                : new double[0];
        }

        // この多項式を表す文字列に変換します。
        public override string ToString()
        {
            return ToString("X");
        }

        // 変数となる文字列を指定して、この多項式を表す文字列に変換します。
        public string ToString(string variable)
        {
            var builder = new StringBuilder();
            foreach (var c in Coefficients.OrderByDescending(x => x.Key))
            {
                var abs = Math.Abs(c.Value);

                builder.Append(c.Value > 0 ? '+' : '-').Append(' ');
                if (c.Key == 0 || abs != 1) builder.Append(abs);
                if (c.Key != 0) builder.Append(variable);
                if (c.Key > 1) builder.Append('^').Append(c.Key);
                builder.Append(' ');
            }
            builder.Remove(builder.Length - 1, 1);
            return builder[0] == '+' ? builder.ToString(2, builder.Length - 2) : builder.ToString();
        }
    }
}