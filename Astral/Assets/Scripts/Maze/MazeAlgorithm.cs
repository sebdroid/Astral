public abstract class MazeAlgorithm
{

    protected MazeCell[,] mazeCells;
    protected int mRows, mColumns;

    protected MazeAlgorithm(MazeCell[,] mazeCells) : base()
    {
        this.mazeCells = mazeCells;
        mRows = mazeCells.GetLength(0);
        mColumns = mazeCells.GetLength(1);
    }

    public abstract void StartGen();
}
