using System;
using System.IO;

namespace RecursiveDescent
{
    public class Program
    {
        public static void Main( string[] args )
        {
            string file = Console.ReadLine();
            StreamReader fileStream = new StreamReader( file );
            string code = fileStream.ReadLine();
            code = PreprocessCode( code );
            RecursiveDescent recursiveDescent = new RecursiveDescent( code );
            try
            {
                recursiveDescent.Check();
                Console.WriteLine( $"Parsing successful" );
            }
            catch ( ApplicationException e )
            {
                Console.WriteLine( $"Parsing error: {e.Message}" );
            }
        }

        private static string PreprocessCode( string code )
        {
            string preprocessedCode = string.Empty;
            foreach ( var symbol in code )
            {
                if ( symbol == '(' || symbol == ')' || symbol == '+' || symbol == '-' || symbol == '*' || symbol == ':' || symbol == ';' )
                {
                    preprocessedCode += $" {symbol} ";
                }
                else if ( symbol == ',' )
                {
                    preprocessedCode += $"{symbol} ";
                }
                else
                {
                    preprocessedCode += symbol;
                }
            }
            return preprocessedCode;
        }
    }
}
