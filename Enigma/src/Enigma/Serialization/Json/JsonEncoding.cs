using System;
using System.Globalization;
using System.Text;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{
    public class JsonEncoding
    {

        public static readonly JsonEncoding Unicode = new JsonEncoding(Encoding.Unicode);
        public static readonly JsonEncoding UTF8 = new JsonEncoding(Encoding.UTF8);

        public static readonly IFormatProvider NumberFormat =
            new NumberFormatInfo {
                NaNSymbol = "NaN",
                NegativeSign = "-",
                NumberDecimalDigits = 10,
                NumberDecimalSeparator = "."
            };

        public static readonly IFormatProvider DateTimeFormat =
            new DateTimeFormatInfo {
                
            };

        public readonly Encoding BaseEncoding;

        public readonly byte[] ObjectBegin;
        public readonly byte[] ObjectEnd;

        public readonly byte[] ArrayBegin;
        public readonly byte[] ArrayEnd;

        public readonly byte[] Quote;
        public readonly byte[] Assignment;
        public readonly byte[] Comma;
        public readonly byte[] ReverseSolidus;
        public readonly byte[] Solidus;
        public readonly byte[] Backspace;
        public readonly byte[] Formfeed;
        public readonly byte[] Newline;
        public readonly byte[] CarriageReturn;
        public readonly byte[] HorizontalTab;

        public readonly byte[] Null;
        public readonly byte[] True;
        public readonly byte[] False;
        public readonly byte[] Zero;

        public JsonEncoding(Encoding baseEncoding)
        {
            BaseEncoding = baseEncoding;
            ObjectBegin = baseEncoding.GetBytes("{");
            ObjectEnd = baseEncoding.GetBytes("}");
            ArrayBegin = baseEncoding.GetBytes("[");
            ArrayEnd = baseEncoding.GetBytes("]");
            Quote = baseEncoding.GetBytes("\"");
            Assignment = baseEncoding.GetBytes(":");
            Comma = baseEncoding.GetBytes(",");
            ReverseSolidus = baseEncoding.GetBytes("\\");
            Solidus = baseEncoding.GetBytes("/");
            Backspace = baseEncoding.GetBytes("\b");
            Formfeed = baseEncoding.GetBytes("\f");
            Newline = baseEncoding.GetBytes("\n");
            CarriageReturn = baseEncoding.GetBytes("\r");
            HorizontalTab = baseEncoding.GetBytes("\t");
            Null = baseEncoding.GetBytes("null");
            True = baseEncoding.GetBytes("true");
            False = baseEncoding.GetBytes("false");
            Zero = baseEncoding.GetBytes("0");
        }
        public bool RequiresEscape(char c, out byte[] charBytes)
        {
            switch (c) {
                case '\\':
                    charBytes = ReverseSolidus;
                    return true;
                case '\"':
                    charBytes = Quote;
                    return true;
                case '/':
                    charBytes = Solidus;
                    return true;
                case '\b':
                    charBytes = Backspace;
                    return true;
                case '\f':
                    charBytes = Formfeed;
                    return true;
                case '\n':
                    charBytes = Newline;
                    return true;
                case '\r':
                    charBytes = CarriageReturn;
                    return true;
                case '\t':
                    charBytes = HorizontalTab;
                    return true;
            }
            charBytes = null;
            return false;
        }

    }
}