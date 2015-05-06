# Solve Equation in C#

C#で方程式を解く。

## How To Use

**多項式**は、構造体`Polynomial`型で表現する。

変数は`Polynomial.X`、定数は`double`型を使用する。

`double -> Polynomial`の暗黙的型変換や演算子のオーバーロードにより、直感的に多項式を表現できる。

```cs
// 変数
Polynomial x = Polynomial.X;

// 多項式「3x^2 + 8x - 5」
Polynomial p = 3 * (x ^ 2) + 8 * x - 5;
```

**方程式**は、左辺と右辺の多項式を与えることで表現する。

`Polynomial`型の動的メソッド`SolveEquation`により、方程式の解を求める。

```cs
// 方程式「3x^2 + 8x - 5 = 2x^2 + 3x + 9」を解く
double[] results = (3 * (x ^ 2) + 8 * x - 5).SolveEquation(2 * (x ^ 2) + 3 * x + 9);

// results: [-4, -1]
```

## cf.

> C#で方程式を解く
> https://sakapon.wordpress.com/2014/12/07/equations-csharp/

