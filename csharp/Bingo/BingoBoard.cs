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

    public void DefineCell(int x, int y, string value)
    {
        this.DefineCell(new Coordinate(x, y), value);
    }

    public void DefineCell(Coordinate coordinate, string value)
    {
        this.VerifyBoundaries(coordinate);
        if (cells[coordinate.X, coordinate.Y].IsInitialized)
        {
            throw new InvalidOperationException("cell already defined");
        }
       
        this.CheckExistingValue(value);

        cells[coordinate.X, coordinate.Y].Value = value;
    }

    public void MarkCell(int x, int y) => this.MarkCell(new Coordinate(x, y));

    public void MarkCell(Coordinate coordinate)
    {
        this.VerifyBoundaries(coordinate);
        if (!IsInitialized())
        {
            throw new InvalidOperationException("board not initialized");
        }

        cells[coordinate.X, coordinate.Y].IsMarked = true;
    }

    public bool IsMarked(int x, int y) => this.IsMarked(new Coordinate(x, y));

    public bool IsMarked(Coordinate coordinate)
    {
        this.VerifyBoundaries(coordinate);
        return cells[coordinate.X, coordinate.Y].IsMarked;
    }

    public bool IsInitialized()
    {
        return cells.Cast<Cell>().All(cell => cell.IsInitialized);
    }

    private void VerifyBoundaries(Coordinate coordinate)
    {
        if (coordinate.X >= this.width || coordinate.Y >= this.height)
        {
            throw new IndexOutOfRangeException("Cell position out of board boundaries");
        }
    }

    private void CheckExistingValue(string value)
    {
        for (var c = 0; c < width; c++)
        {
            for (var r = 0; r < height; r++)
            {
                if (value.Equals(cells[c, r].Value))
                    throw new InvalidOperationException(value + " already present at " + c + "," + r);
            }
        }
    }
}

public record Coordinate(int X, int Y);

public struct Cell
{
    public string? Value { get; set; }

    public bool IsInitialized => this.Value is not null;

    public bool IsMarked { get; set; }
}