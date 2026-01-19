namespace Bingo;

public class BingoBoard
{
    private readonly Cell[,] cells;
    private readonly int width;
    private readonly int height;
    
    public BingoBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.cells = new Cell[width, height];
    }

    private void VerifyBoundaries(int x, int y)
    {
        if (x >= width || y >= height)
        {
            throw new IndexOutOfRangeException("Cell position out of board boundaries");
        }
    }

    public void DefineCell(int x, int y, string value)
    {
        this.VerifyBoundaries(x, y);
        if (!cells[x, y].IsInitialized)
        {
            for (var c = 0; c < width; c++)
            {
                for (var r = 0; r < height; r++)
                {
                    if (value.Equals(cells[c, r].Value))
                        throw new InvalidOperationException(value + " already present at " + c + "," + r);
                }
            }
            cells[x, y].Value = value;
        }
        else
        {
            throw new InvalidOperationException("cell already defined");
        }
    }

    public void MarkCell(int x, int y)
    {
        this.VerifyBoundaries(x, y);
        if (!IsInitialized())
        {
            throw new InvalidOperationException("board not initialized");
        }
        cells[x, y].IsMarked = true;
    }

    public bool IsMarked(int x, int y)
    {
        this.VerifyBoundaries(x, y);
        return cells[x, y].IsMarked;
    }

    public bool IsInitialized()
    {
        return cells.Cast<Cell>().All(cell => cell.IsInitialized);
    }
}

public struct Cell
{
    public string? Value { get; set; }

    public bool IsInitialized => this.Value is not null;

    public bool IsMarked { get; set; }
}