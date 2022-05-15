namespace Ion.CodeAnalysis.Syntax;

public sealed class SyntaxToken
    : SyntaxNode
{
    public SyntaxToken(SyntaxKind kind, string text, int position, object value)
    {
        Kind = kind;
        Text = text;
        Position = position;
        Value = value;
    }

    public string Text { get; }
    public int Position { get; }
    public object Value { get; }
    public override SyntaxKind Kind { get; }

    public override string ToString()
    {
        return $"Kind: {Kind}, Text: '{Text}', Position: {Position}, Value: {Value ?? "null"}";
    }
}
