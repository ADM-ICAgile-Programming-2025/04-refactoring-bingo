namespace Bingo;

public class BingoBoard
{
    private Cell[,] cells;

    public BingoBoard(int width, int height)
    {
        this.cells = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cell();
            }
        }
    }

    public void DefineCell(Cordination cordination, string value)
    {
        if (cells[cordination.x, cordination.y].HasValue)
        {
            throw new InvalidOperationException("cell already defined");
        }
        IsCellValueAlreadyPresent(value);
        cells[cordination.x, cordination.y].SetValue(value);
    }
    
    void IsCellValueAlreadyPresent(string value)
    {
        for (int c = 0; c < cells.GetLength(0); c++)
        {
            for (int r = 0; r < cells.GetLength(1); r++)
            {
                if (value.Equals(cells[c, r].Value))
                    throw new InvalidOperationException($"{value} already present at {c},{r}");
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
        
        cells[x, y].Mark();
    }

    public bool IsMarked(int x, int y)
    {
        if (!IsInitialized())
        {
            throw new InvalidOperationException("board not initialized");
        }
        return cells[x, y].IsMarked;
    }

    public bool IsInitialized()
    {
        return cells.Cast<Cell>().All(cell => cell.HasValue);
    }
}