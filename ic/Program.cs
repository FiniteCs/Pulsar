using Ion.CodeAnalysis.Syntax;
using System;

namespace Ion;

public class Program
{
    public static void Main()
    {
        while (true)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                break;
            }

            var lexer = new Lexer(input);
            SyntaxToken token;
            do
            {
                token = lexer.Lex();
                Console.WriteLine(token);
            } while(token.Kind != SyntaxKind.EndOfFileToken);
        }
    }
}
