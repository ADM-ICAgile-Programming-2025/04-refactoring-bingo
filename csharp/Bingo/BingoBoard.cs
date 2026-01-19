namespace Bingo;

public class BingoBoard
{
    private string[,] cells;
    private bool[,] marked;

    public BingoBoard(int width, int height)
    {
        this.cells = new string[width, height];
        this.marked = new bool[width, height];
    }

    public void DefineCell(int x, int y, string value)
    {
        if (cells[x, y] != null)
        {
            throw new InvalidOperationException("cell already defined");
        }
        IsCellValueAlreadyPresent(value);
        cells[x, y] = value;
    }
    
    void IsCellValueAlreadyPresent(string value)
    {
        for (int c = 0; c < cells.GetLength(0); c++)
        {
            for (int r = 0; r < cells.GetLength(1); r++)
            {
                if (value.Equals(cells[c, r]))
                    throw new InvalidOperationException($"{value} already present at ({c},{r})");
            }
        }
    }

    public void MarkCell(int x, int y)
    {
        if (!IsInitialized())
        {
            throw new InvalidOperationException("board not initialized");
        }
        
        if (x < 0 || x >= cells.GetLength(0) || y < 0 || y >= cells.GetLength(1))
        {
            throw new ArgumentOutOfRangeException($"Position ({x},{y}) is out of bounds");
        }
        
        marked[x, y] = true;
    }

    public bool IsMarked(int x, int y)
    {
        if (!IsInitialized())
        {
            throw new InvalidOperationException("board not initialized");
        }
        return marked[x, y];
    }

    public bool IsInitialized()
    {
        return cells.Cast<string>().All(cell => cell != null);
    }
}