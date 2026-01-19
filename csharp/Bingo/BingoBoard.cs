using System.Windows.Markup;

namespace Bingo;


public class Cell
{
    public string? Value { get; set; }

    public bool IsInitialized => this.Value is not null;

    public bool IsMarked { get; set; }
}

public class BingoBoard
{
    private readonly Cell[,] cells;
    
    public BingoBoard(int width, int height)
    {
        this.cells = new Cell[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                this.cells[x, y] = new Cell();
            }
        }
    }

    public void DefineCell(int x, int y, string value)
    {
        if (!cells[x, y].IsInitialized)
        {
            for (var c = 0; c < cells.GetLength(0); c++)
            {
                for (var r = 0; r < cells.GetLength(1); r++)
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
        if (!IsInitialized())
        {
            throw new InvalidOperationException("board not initialized");
        }
        cells[x, y].IsMarked = true;
    }

    public bool IsMarked(int x, int y)
    {
        return cells[x, y].IsMarked;
    }

    public bool IsInitialized()
    {
        return cells.Cast<Cell>().All(cell => cell.IsInitialized);
    }
}