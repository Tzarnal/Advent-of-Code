using Serilog;
using System;

namespace Advent.AoCLib
{
    public class IntVector2
    {
        public int X;
        public int Y;

        public IntVector2()
        {
            X = 0;
            Y = 0;
        }

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IntVector2((int x, int y) input)
        {
            X = input.x;
            Y = input.y;
        }

        public IntVector2(IntVector2 input)
        {
            X = input.X;
            Y = input.Y;
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
            => new(a.X - b.X, a.Y - b.Y);
        public static IntVector2 operator -(IntVector2 a, (int X, int Y) b)
             => new(a.X - b.X, a.Y - b.Y);

        public static IntVector2 operator -(IntVector2 a, int i)
            => new(a.X - i, a.Y - i);

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
            => new(a.X + b.X, a.Y + b.Y);

        public static IntVector2 operator +(IntVector2 a, (int X, int Y) b)
            => new(a.X + b.X, a.Y + b.Y);

        public static IntVector2 operator +(IntVector2 a, int i)
            => new(a.X + i, a.Y + i);

        public static IntVector2 operator *(IntVector2 a, IntVector2 b)
            => new(a.X * b.X, a.Y * b.Y);

        public static IntVector2 operator *(IntVector2 a, (int X, int Y) b)
            => new(a.X * b.X, a.Y * b.Y);

        public static IntVector2 operator *(IntVector2 a, int i)
            => new(a.X * i, a.Y * i);

        public static IntVector2 operator /(IntVector2 a, IntVector2 b)
            => new(a.X / b.X, a.Y / b.Y);

        public static IntVector2 operator /(IntVector2 a, (int X, int Y) b)
            => new(a.X / b.X, a.Y / b.Y);

        public static IntVector2 operator /(IntVector2 a, int i)
            => new(a.X / i, a.Y / i);

        public static bool operator ==(IntVector2 a, IntVector2 b)
            => a.Equals(b);

        public static bool operator ==(IntVector2 a, (int X, int Y) b)
            => a.Equals(b);

        public static bool operator !=(IntVector2 a, IntVector2 b)
            => !a.Equals(b);

        public static bool operator !=(IntVector2 a, (int X, int Y) b)
            => !a.Equals(b);

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        public IntVector2 Copy => new(X, Y);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            {
                return false;
            }

            if (obj is not IntVector2 vector)
            {
                return false;
            }

            return X == vector.X && Y == vector.Y;
        }

        public bool Equals(IntVector2 vector)
        {
            return X == vector.X && Y == vector.Y;
        }

        public bool Equals((int X, int Y) tuple)
        {
            return X == tuple.X && Y == tuple.Y;
        }

        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }

        public override int GetHashCode()
        {
            return X ^ Y; //Classic example used by the .net7 documentation for a two int hash
        }
    }
}