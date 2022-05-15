using System.Collections.Generic;

namespace Ion.CodeAnalysis.Syntax;

internal sealed class Lexer
{
    private readonly List<string> _diagnostics = new List<string>();
    private readonly string _text;
    private int _position;
    private int _start;
    private SyntaxKind _kind;
    private object _value;

    public Lexer(string text)
    {
        _text = text;
        _diagnostics = new List<string>();
    }

    public IEnumerable<string> Diagnostics => _diagnostics;

    private char Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _text.Length)
        {
            return '\0';
        }

        return _text[index];
    }

    private char EatChar()
    {
        var c = Peek(0);
        _position++;
        return c;
    }

    public SyntaxToken Lex()
    {
        _start = _position;
        _kind = SyntaxKind.BadToken;
        _value = null;

        switch (Peek(0))
        {
            case '\0':
                _kind = SyntaxKind.EndOfFileToken;
                break;
            case '+':
                _kind = SyntaxKind.PlusToken;
                EatChar();
                break;
            case '-':
                _kind = SyntaxKind.MinusToken;
                EatChar();
                break;
            case '*':
                _kind = SyntaxKind.StarToken;
                EatChar();
                break;
            case '/':
                _kind = SyntaxKind.SlashToken;
                EatChar();
                break;
            case '%':
                _kind = SyntaxKind.PercentToken;
                EatChar();
                break;
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                ReadNumberToken();
                break;
            case ' ':
            case '\t':
            case '\n':
            case '\r':
                ReadWhitespace();
                break;
            default:
                _diagnostics.Add($"Unexpected character '{EatChar()}'");
                break;
        }

        var length = _position - _start;
        var text = _text.Substring(_start, length);
        return new SyntaxToken(_kind, text, _start, _value);
    }

    private void ReadNumberToken()
    {
        while (char.IsDigit(Peek(0)))
        {
            EatChar();
        }

        var length = _position - _start;
        var text = _text.Substring(_start, length);
        if (!int.TryParse(text, out var value))
        {
            _diagnostics.Add($"Invalid int32: '{text}'");
        }

        _value = value;
        _kind = SyntaxKind.NumericLiteralToken;
    }

    private void ReadWhitespace()
    {
        while (char.IsWhiteSpace(Peek(0)))
        {
            EatChar();
        }

        _kind = SyntaxKind.WhitespaceToken;
    }
}
