using System;
namespace ArithmeticParsing.Properties.Parsing
{
    /// <summary>
    /// This class is an advanced Type Int : It stores an integer and manages non valid integer
    /// </summary>
    public class NumberInt
    {
        #region Members
        /// <summary>
        /// Int value 
        /// </summary>
        int _val;

        /// <summary>
        /// Is the integer non valid or NaN?
        /// </summary>
        public bool IsNaN { get; }

        /// <summary>
        /// error message
        /// </summary>
        public string ErrorMessage { get; }  
        #endregion

        #region Constructors
        /// <summary>
        /// Empty Constrocutor
        /// </summary>
        public NumberInt()
        {
            _val = 0;
            IsNaN = true;
            ErrorMessage = "No Input";
        }

        /// <summary>
        /// Constructor from an Integer
        /// </summary>
        public NumberInt(int val)
        {
            _val = val;
            IsNaN = false;
            ErrorMessage = "";
        }

        /// <summary>
        /// Constructor from another NumberInt
        /// </summary>
        public NumberInt(NumberInt i1)
        {
            _val = i1._val;
            IsNaN = i1.IsNaN;
            ErrorMessage = i1.ErrorMessage;
        }

        /// <summary>
        /// Constructor with all the members 
        /// </summary>
        public NumberInt(int val, bool isNaN, string errorMessage)
        {
            _val = val;
            IsNaN = isNaN;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Constructor from an error message
        /// </summary>
        public NumberInt(string message)
        {
            _val = 0;
            IsNaN = true;
            ErrorMessage = message;
        }
        #endregion

        #region OverRideOperators
        /// <summary>
        /// Override Tostring()
        /// </summary>
        public override string ToString()
        {
            return IsNaN ? ErrorMessage : _val.ToString();
        }

        /// <summary>
        /// Override Equals()
        /// </summary>
        public override bool Equals(object o)
        {
            if (o is null)
            {
                return false;
            }

            NumberInt i2 = (NumberInt)o;

            if (IsNaN)
            {
                return i2.IsNaN && ErrorMessage == i2.ErrorMessage;
            }
            else if (i2.IsNaN)
            {
                return false;
            }
            return _val == i2._val;
        }

        /// <summary>
        /// Override GetHashCode()
        /// </summary>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion region

        #region Static Operators
        /// <summary>
        /// Explicit conversion
        /// </summary>
        public static explicit operator NumberInt(int b) => new NumberInt(b);

        /// <summary>
        /// Operattor ==
        /// </summary>
        public static bool operator ==(NumberInt i1, NumberInt i2)
        {
            return (i1 is null) ? false : i1.Equals(i2);
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        public static bool operator !=(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null || i1.IsNaN || i2.IsNaN)
            {
                return false;
            }
            return i1._val != i2._val;
        }

        /// <summary>
        /// Operator +
        /// </summary>
        public static NumberInt operator +(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null)
            {
                return null;
            }
            if (i1.IsNaN)
            {
                if (i2.IsNaN)
                {
                    if (i1.ErrorMessage == i2.ErrorMessage)
                    {
                        return new NumberInt(i1);
                    }
                    return new NumberInt(String.Format("({0} + {1})", i1.ErrorMessage, i2.ErrorMessage));
                }
                return new NumberInt(i1);
            }
            if (i2.IsNaN)
            {
                return new NumberInt(i2);
            }
            return new NumberInt(i1._val + i2._val);
        }

        /// <summary>
        /// Operator -
        /// </summary>
        public static NumberInt operator -(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null)
            {
                return null;
            }
            if (i1.IsNaN)
            {
                if (i2.IsNaN)
                {
                    if (i1.ErrorMessage == i2.ErrorMessage)
                    {
                        return new NumberInt(i1);
                    }
                    return new NumberInt(String.Format("({0} - {1})", i1.ErrorMessage, i2.ErrorMessage));
                }
                return new NumberInt(i1);
            }
            if (i2.IsNaN)
            {
                return new NumberInt(i2);
            }
            return new NumberInt(i1._val - i2._val);
        }

        /// <summary>
        /// Operator *
        /// </summary>
        public static NumberInt operator *(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null)
            {
                return null;
            }
            if (i1.IsNaN)
            {
                if (i2.IsNaN)
                {
                    if (i1.ErrorMessage == i2.ErrorMessage)
                    {
                        return new NumberInt(i1);
                    }
                    return new NumberInt(String.Format("({0} * {1})", i1.ErrorMessage, i2.ErrorMessage));
                }
                return new NumberInt(i1);
            }
            if (i2.IsNaN)
            {
                return new NumberInt(i2);
            }
            return new NumberInt(i1._val * i2._val);
        }

        /// <summary>
        /// Operator /
        /// </summary>
        public static NumberInt operator /(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null)
            {
                return null;
            }
            if (i1.IsNaN)
            {
                if (i2.IsNaN)
                {
                    if (i1.ErrorMessage == i2.ErrorMessage)
                    {
                        return new NumberInt(i1);
                    }
                    return new NumberInt(String.Format("({0} / {1})", i1.ErrorMessage, i2.ErrorMessage));
                }
                return new NumberInt(i1);
            }
            if (i2.IsNaN)
            {
                return new NumberInt(i2);
            }
            if (i2._val == 0)
            {
                return new NumberInt("Division by 0");
            }
            return new NumberInt(i1._val / i2._val);
        }

        /// <summary>
        /// Operator inferior
        /// </summary>
        public static bool operator <(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null || i1.IsNaN || i2.IsNaN)
            {
                return false;
            }
            return i1._val < i2._val;
        }

        /// <summary>
        /// Operator Superior
        /// </summary>
        public static bool operator >(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null || i1.IsNaN || i2.IsNaN)
            {
                return false;
            }
            return i1._val > i2._val;
        }

        /// <summary>
        /// Operator Inferior and equal
        /// </summary>
        public static bool operator <=(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null || i1.IsNaN || i2.IsNaN)
            {
                return false;
            }
            return i1._val <= i2._val;
        }

        /// <summary>
        /// Operator Superior and equal
        /// </summary>
        public static bool operator >=(NumberInt i1, NumberInt i2)
        {
            if (i1 is null || i2 is null || i1.IsNaN || i2.IsNaN)
            {
                return false;
            }
            return i1._val >= i2._val;
        }
        #endregion

        #region Non Static Functions
        /// <summary>
        /// Return the Opposite ()
        /// newValue = - value
        /// </summary>
        public NumberInt Opposite()
        {
            return new NumberInt(-_val, IsNaN, ErrorMessage);

        }
        #endregion
    }
}
