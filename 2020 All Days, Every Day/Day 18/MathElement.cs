using System;
using System.Linq;

namespace Day_18
{
    public class MathElement : IEquatable<MathElement>, IEquatable<string>, IEquatable<char>, IEquatable<double>
    {
        private static readonly string[] _operators = { "-", "+", "*", "/" };

        public bool IsSymbol;
        public bool IsOperator;
        public bool IsNumber => !IsSymbol && !IsOperator;

        public double Value;

        public string Operator;
        private readonly string OriginalInput;

        public MathElement(double input)
        {
            OriginalInput = input.ToString();
            Value = input;
        }

        public MathElement(char input) : this(input.ToString())
        {
        }

        public MathElement(string input)
        {
            input = input.Trim().ToLower();
            OriginalInput = input;
            if (!double.TryParse(input, out Value))
            {
                IsSymbol = true;
                Operator = OriginalInput;
            }

            if (_operators.Contains(input))
            {
                IsOperator = true;
                Operator = OriginalInput;
            }
        }

        public bool Equals(double other)
        {
            if (IsNumber)
            {
                Value.Equals(other);
            }

            return false;
        }

        public static bool operator ==(MathElement lhs, double rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(MathElement lhs, double rhs)
        {
            return !lhs.Equals(rhs);
        }

        public bool Equals(MathElement other)
        {
            return OriginalInput.Equals(other.OriginalInput.Trim().ToLower());
        }

        public static bool operator ==(MathElement lhs, MathElement rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(MathElement lhs, MathElement rhs)
        {
            return !lhs.Equals(rhs);
        }

        public bool Equals(string other)
        {
            return OriginalInput.Equals(other.Trim().ToLower());
        }

        public static bool operator ==(MathElement lhs, string rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(MathElement lhs, string rhs)
        {
            return !lhs.Equals(rhs);
        }

        public bool Equals(char other)
        {
            return OriginalInput.Equals(other.ToString().ToLower());
        }

        public static bool operator ==(MathElement lhs, char rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(MathElement lhs, char rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override string ToString()
        {
            return OriginalInput;
        }

        public override int GetHashCode()
        {
            return OriginalInput.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is MathElement otherM)
            {
                return Equals(otherM);
            }

            if (obj is string otherS)
            {
                return Equals(otherS);
            }

            if (obj is char otherC)
            {
                return Equals(otherC);
            }

            if (obj is double otherD)
            {
                return Equals(otherD);
            }

            if (obj is int otherI)
            {
                return Equals(otherI);
            }

            return false;
        }
    }
}