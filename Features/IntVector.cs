using System;
using System.Globalization;


namespace Illeana.Features;

/// <summary>
/// Integer vector2
/// </summary>
/// <param name="x">X</param>
/// <param name="y">Y</param>
public struct VecI(int x, int y) : IEquatable<VecI>
{
    public int x = x;
    public int y = y;
    public const int Count = 2;

    public VecI(int v) : this(v, v){}
    public VecI(double x, double y) : this((int)x, (int)y) {}
    public VecI(Vec v) : this(v.x, v.y) {}

    public static VecI Zero => default;
    public static VecI One => new(1);
    public static VecI UnitX => new(1, 0);
    public static VecI UnitY => new(0, 1);
    // public int this[int index]
    // {
    //     readonly get => this.GetElement(index);
    //     set => this = this.WithElement(index, value);
    // }

    public static VecI operator +(VecI a, VecI b)
    {
        return new(a.x + b.x, a.y + b.y);
    }

    public static VecI operator -(VecI a, VecI b)
    {
        return new(a.x - b.x, a.y - b.y);
    }

    public static VecI operator -(VecI v)
    {
        return Zero - v;
    }

    public static VecI operator /(VecI a, VecI b)
    {
        return new(a.x / b.x, a.y / b.y);
    }

    public static VecI operator /(VecI v, int a)
    {
        return v / new VecI(a);
    }

    public static VecI operator *(VecI a, VecI b)
    {
        return new(a.x * b.x, a.y * b.y);
    }

    public static VecI operator *(VecI v, int a)
    {
        return v * new VecI(a);
    }

    public static VecI operator *(int a, VecI v)
    {
        return v * a;
    }

    public static bool operator ==(VecI a, VecI b)
    {
        return a.x == b.x && a.y == b.y;
    }
    
    public static bool operator !=(VecI a, VecI b)
    {
        return a.x != b.x || a.y != b.y;
    }

    public static VecI Abs(VecI v)
    {
        return new(Math.Abs(v.x), Math.Abs(v.y));
    }

    public static VecI Clamp(VecI v, VecI min, VecI max)
    {
        return Min(Max(v, min), max);
    }

    public static VecI Min(VecI a, VecI b)
    {
        return new(a.x < b.x ? a.x : b.x, a.y < b.y ? a.y : b.y);
    }

    public static VecI Max(VecI a, VecI b)
    {
        return new(a.x > b.x ? a.x : b.x, a.y > b.y ? a.y : b.y);
    }

    public static int Dot(VecI a, VecI b)
    {
        return (a.x * b.x) + (a.y * b.y);
    }
    public static int Dot(VecI v)
    {
        return (v.x * v.x) + (v.y * v.y);
    }

    public static int DistanceSquared(VecI a, VecI b)
    {
        VecI diff = a - b;
        return Dot(diff);
    }

    public static double Distance(VecI a, VecI b)
    {
        double distanceSquared = (double)DistanceSquared(a, b);
        return Math.Sqrt(distanceSquared);
    }
    public static int DistanceInt(VecI a, VecI b)
    {
        int distanceSquared = DistanceSquared(a, b);
        return (int)Math.Sqrt(distanceSquared);
    }

    public static VecI Lerp(VecI a, VecI b, double n)
    {
        Vec v = (a.ToVec() * (1-n)) + (b.ToVec() * n);
        return new(v.x, v.y);
    }

    public static VecI Normalize(VecI v)
    {
        Vec vec = new Vec(v.x, v.y) / v.Length();
        return new(vec);
    }

    public static VecI Reflect(VecI v, VecI normal)
    {
        return v - (2 * (Dot(v, normal) * normal));
    }

    public readonly VecI Rotate(double r)
    {
        return new((x * Math.Cos(r)) - (y * Math.Sin(r)), (x * Math.Sin(r)) + (y * Math.Cos(r)));
    }

    public readonly VecI RotateSnap(int r)
    {
        return Rotate(r * Math.PI / 2);
    }

    public readonly Vec ToVec()
    {
        return new Vec(x, y);
    }

    public readonly bool Equals(VecI v)
    {
        return x == v.x && y == v.y;
    }
    public override readonly bool Equals(object? o)
    {
        if (o is VecI v)
        {
            return x == v.x && y == v.y;
        }
        if (o is Vec vec && vec.x % 1 == 0 && vec.y % 1 == 0)
        {
            return x == (int)vec.x && y == (int)vec.y;
        }
        return false;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public readonly int LengthSquared()
    {
        return Dot(this);
    }

    public readonly double Length()
    {
        double lengthSquared = LengthSquared();
        return Math.Sqrt(lengthSquared);
    }
    public readonly int LengthInt()
    {
        double lengthSquared = LengthSquared();
        return (int)Math.Sqrt(lengthSquared);
    }

    public override readonly string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    public readonly string ToString(string? format)
    {
        return ToString(format, CultureInfo.CurrentCulture);
    }

    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

        return $"<{x.ToString(format, formatProvider)}{separator} {y.ToString(format, formatProvider)}>";
    }
}
