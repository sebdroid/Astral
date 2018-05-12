using UnityEngine;

public class GenMaze : MazeAlgorithm
{

    private int cRow = 0;
    private int cColumn = 0;

    private bool courseComplete = false;

    public GenMaze(MazeCell[,] mazeCells) : base(mazeCells) { }

    // Use this for initialization
    public override void StartGen()
    {
        HAK();
    }

    void HAK()
    {
        mazeCells[cRow, cColumn].visited = true;

        while (!courseComplete)
        {
            Kill();
            Hunt();
        }
    }

    void Kill()
    {
        while (PathAvailable(cRow, cColumn))
        {
            int direction = Random.Range(1, 5);

            if ((direction == 1) && (CellAvailable(cRow - 1, cColumn)))
            {
                DestroyWall(mazeCells[cRow - 1, cColumn].southW);
                cRow--;
            }else if ((direction == 2) && (CellAvailable(cRow + 1, cColumn)))
            {
                DestroyWall(mazeCells[cRow, cColumn].southW);
                cRow++;
            }else if ((direction == 3) && (CellAvailable(cRow, cColumn + 1)))
            {
                DestroyWall(mazeCells[cRow, cColumn].eastW);
                cColumn++;
            }else if ((direction  == 4) && (CellAvailable(cRow, cColumn - 1)))
            {
                DestroyWall(mazeCells[cRow, cColumn - 1].eastW);
                cColumn--;
            }
        }

    }

    void Hunt()
    {

    }

    bool PathAvailable(int row, int column)
    {
        int paths = 0;

        if ((row > 0) && (!mazeCells[row-1, column].visited)) //if cell above? is unvisited
        {
            paths++;
        }

        if ((row < mRows - 1) && (!mazeCells[row+1, column].visited))
        {
            paths++;
        }

        if ((column > 0) && (!mazeCells[row, column - 1].visited)) //if cell to the right is unvisited
        {
            paths++;
        }

        if ((column < mColumns - 1) && (!mazeCells[row, column + 1].visited))
        {
            paths++;
        }

        return (paths > 0);
    }

    bool CellAvailable(int row, int column)
    {
        if ((((row >= 0) && (row < mRows)) && ((column >= 0) && (column < mColumns))) && !mazeCells[row, column].visited)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DestroyWall(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

    bool NextToCell(int row, int column)
    {
        int visited = 0;

        if ((row > 0) && (mazeCells[row - 1, column].visited)) //if cell above? is unvisited
        {
            visited++;
        }

        if ((row < mRows - 1) && (mazeCells[row + 1, column].visited))
        {
            visited++;
        }

        if ((column > 0) && (mazeCells[row, column - 1].visited)) //if cell to the right is unvisited
        {
            visited++;
        }

        if ((column < mColumns - 1) && (mazeCells[row, column + 1].visited))
        {
            visited++;
        }

        return (visited > 0);
    }

    void DestroyCellWall(int row, int column)
    {
        bool destroyed = false;

        while (!destroyed)
        {
            int direction = Random.Range(1, 5);

            if (((direction == 1) && (row > 0)) && (mazeCells[row - 1, column].visited))
            {
                DestroyWall(mazeCells[row - 1, column].southW);
                destroyed = true;
            }else if (((direction == 2) && (row < (mRows - 2))) && (mazeCells[row + 1, column].visited))
            {
                DestroyWall(mazeCells[row, column].southW);
                destroyed = true;
            }
            else if (((direction == 3) && (column > 0)) && (mazeCells[row, column - 1].visited))
            {
                DestroyWall(mazeCells[row - 1, column].eastW);
                destroyed = true;
            }
            else if (((direction == 4) && (column > (mColumns - 2))) && (mazeCells[row, column + 1].visited))
            {
                DestroyWall(mazeCells[row, column].eastW);
                destroyed = true;
            }
        }
    }

}