namespace Bingo;

public class Cell
{
    public string? Value { get; private set; }
    public bool IsMarked { get; private set; }

    public Cell()
    {
        Value = null;
        IsMarked = false;
    }

    public void SetValue(string value)
    {
        Value = value;
    }

    public void Mark()
    {
        IsMarked = true;
    }

    public bool HasValue => Value != null;
}